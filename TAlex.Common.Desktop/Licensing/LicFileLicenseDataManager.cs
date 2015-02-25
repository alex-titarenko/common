using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using TAlex.Common.Extensions;


namespace TAlex.Common.Licensing
{
    public abstract class LicFileLicenseDataManager : ILicenseDataManager
    {
        #region Fields

        private const string DefaultLicenseFilePath = "License.dat";

        private SymmetricAlgorithm _cipher;

        #endregion

        #region Properties

        protected abstract byte[] SK { get; }

        protected abstract byte[] IV { get; }

        protected virtual string LicenseFilePath
        {
            get
            {
                string fullPath = Path.GetDirectoryName(Path.GetFullPath(System.Environment.GetCommandLineArgs()[0]));
                return Path.Combine(fullPath, DefaultLicenseFilePath);
            }
        }

        protected virtual string AlternativeLicenseFilePath
        {
            get
            {
                var assemblyInfo = Assembly.GetEntryAssembly().GetAssemblyInfo();

                return Path.Combine(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData),
                    assemblyInfo.Company,
                    assemblyInfo.Product,
                    String.Format("v{0}", assemblyInfo.Version.Major),
                    DefaultLicenseFilePath);
            }
        }


        protected virtual string LicenseNameSeparator { get { return "\n"; } }
        protected virtual string LicenseKeySeparator { get { return "$"; } }

        protected virtual int TextLength { get { return 2000; } }
        protected virtual int LicenseNameStartIndex { get { return 135; } }
        protected virtual int LicenseNameMaxLength { get { return 70; } }
        protected virtual int LicenseKeyStartIndex { get { return 1146; } }
        protected virtual int LicenseKeyMaxLength { get { return 250; } }

        #endregion

        #region Constructors

        public LicFileLicenseDataManager()
        {
            _cipher = new DESCryptoServiceProvider();
            _cipher.IV = IV;
            _cipher.Key = SK;
        }

        #endregion

        #region ILicenseDataManager Members

        public LicenseData Load()
        {
            LicenseData licData = new LicenseData();
            byte[] data = null;

            if (!LoadData(new[] { LicenseFilePath, AlternativeLicenseFilePath }, out data))
            {
                return licData;
            }
            
            string text = String.Empty;
            try
            {
                text = CryptoHelper.Decript(data, _cipher);
            }
            catch (CryptographicException)
            {
                return licData;
            }

            if (text.Length != TextLength + 2)
                return licData;

            // Load license data
            try
            {
                string lin = text.Substring(LicenseNameStartIndex, LicenseNameMaxLength + 1);
                licData.LicenseName = lin.Substring(0, lin.IndexOf(LicenseNameSeparator));

                string lik = text.Substring(LicenseKeyStartIndex, LicenseKeyMaxLength + 1);
                licData.LicenseKey = lik.Substring(0, lik.IndexOf(LicenseKeySeparator));
            }
            catch (ArgumentOutOfRangeException)
            {
                licData.LicenseName = String.Empty;
                licData.LicenseKey = String.Empty;
            }

            return licData;
        }

        public void Save(LicenseData licenseData)
        {
            Save(new[] { LicenseFilePath, AlternativeLicenseFilePath }, licenseData);
        }


        private bool LoadData(IEnumerable<string> paths, out byte[] data)
        {
            foreach (string path in paths)
            {
                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        data = new byte[fs.Length];
                        fs.Read(data, 0, (int)fs.Length);
                        return true;
                    }
                }
                catch (IOException)
                {
                }
            }

            data = null;
            return false;
        }

        private bool Save(IEnumerable<string> paths, LicenseData licenseData)
        {
            foreach (string path in paths)
            {
                try
                {
                    string dirName = Path.GetDirectoryName(Path.GetFullPath(path));
                    if (!Directory.Exists(dirName)) Directory.CreateDirectory(dirName);

                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        byte[] data = CryptoHelper.Encrypt(GenerateText(licenseData.LicenseName, licenseData.LicenseKey), _cipher);
                        fs.Write(data, 0, data.Length);
                        return true;
                    }
                }
                catch (UnauthorizedAccessException)
                {
                }
            }
            throw new UnauthorizedAccessException();
        }

        #endregion

        private string GenerateText(string lin, string lik)
        {
            StringBuilder sb = new StringBuilder(TextLength);

            sb.Append(CryptoHelper.GenerateRandomString(LicenseNameStartIndex));
            sb.Append(lin.Substring(0, Math.Min(lin.Length, LicenseNameMaxLength)));
            sb.Append(LicenseNameSeparator);
            sb.Append(CryptoHelper.GenerateRandomString(LicenseKeyStartIndex - sb.Length));
            sb.Append(lik.Substring(0, Math.Min(lik.Length, LicenseKeyMaxLength)));
            sb.Append(LicenseKeySeparator);
            sb.Append(CryptoHelper.GenerateRandomString(TextLength - sb.Length));
            return sb.ToString();
        }
    }
}
