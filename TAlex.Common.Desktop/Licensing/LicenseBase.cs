using System;
using System.Collections.Generic;
using System.Security.Cryptography;


namespace TAlex.Common.Licensing
{
    public abstract class LicenseBase
    {
        #region Fields
        
        protected readonly ILicenseDataManager LicenseDataManager;
        protected readonly ITrialPeriodDataProvider TrialPeriodDataProvider;

        private bool? _isLicensed;
        private int? _trialDaysLeft;
        private LicenseData _licenseData;

        private Object _syncObj = new Object();

        #endregion

        #region Properties

        public virtual int TrialPeriod { get { return 30; } }

        public virtual bool IsTrial
        {
            get
            {
                return !IsLicensed;
            }
        }

        public virtual bool IsLicensed
        {
            get
            {
                if (!_isLicensed.HasValue)
                {
                    _isLicensed = VerifyLicenseData(LicenseData);
                }
                return _isLicensed.Value;
            }
        }

        public virtual int TrialDaysLeft
        {
            get
            {
                if (!_trialDaysLeft.HasValue)
                {
                    _trialDaysLeft = TrialPeriodDataProvider.GetTrialDaysLeft(TrialPeriod);
                }
                return _trialDaysLeft.Value;
            }
        }

        public virtual bool TrialHasExpired
        {
            get
            {
                return (TrialDaysLeft < 0);
            }
        }

        public virtual string LicenseName
        {
            get
            {
                return LicenseData.LicenseName;
            }
        }


        protected LicenseData LicenseData
        {
            get
            {
                if (_licenseData == null)
                {
                    lock (_syncObj)
                    {
                        if (_licenseData == null)
                        {
                            _licenseData = LicenseDataManager.Load();
                        }
                    }
                }
                return _licenseData;
            }
        }

        protected abstract byte[] SK { get; }

        protected abstract byte[] IV { get; }

        protected abstract List<string> SKH { get; }

        #endregion

        #region Constructors

        public LicenseBase(ILicenseDataManager licenseDataManager, ITrialPeriodDataProvider trialPeriodDataProvider)
        {
            LicenseDataManager = licenseDataManager;
            TrialPeriodDataProvider = trialPeriodDataProvider;
        }

        #endregion

        #region Methods

        protected virtual bool VerifyLicenseData(LicenseData data)
        {
            if (String.IsNullOrEmpty(data.LicenseName) || String.IsNullOrEmpty(data.LicenseKey))
                return false;

            DESCryptoServiceProvider cipher = new DESCryptoServiceProvider();
            cipher.IV = IV;
            cipher.Key = SK;

            string decriptLik = String.Empty;

            try
            {
                byte[] likData = Convert.FromBase64String(data.LicenseKey);
                decriptLik = CryptoHelper.Decript(likData, cipher);
            }
            catch (FormatException)
            {
                return false;
            }
            catch (CryptographicException)
            {
                return false;
            }

            string linHash = CryptoHelper.SHA512Base64(data.LicenseName);

            const int linHashStartIndex = 10;
            const int secretKeyStartIndex = 108;
            const int secretKeyLength = 40;

            try
            {
                if (!String.Equals(decriptLik.Substring(linHashStartIndex, linHash.Length), linHash))
                    return false;

                string sekretKey = decriptLik.Substring(secretKeyStartIndex, secretKeyLength);
                string sekretKeyHash = CryptoHelper.SHA512Base64(sekretKey);

                if (!SKH.Contains(sekretKeyHash))
                    return false;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
