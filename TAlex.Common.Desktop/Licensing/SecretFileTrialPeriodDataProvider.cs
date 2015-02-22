using System;
using System.IO;


namespace TAlex.Common.Licensing
{
    public class SecretFileTrialPeriodDataProvider : ITrialPeriodDataProvider
    {
        #region Fields

        private Object _syncObj = new Object();

        #endregion

        #region Properties

        public string FileName
        {
            get;
            set;
        }

        #endregion

        #region ITrialPeriodDataProvider Members

        public virtual int GetTrialDaysLeft(int trialPeriod)
        {
            int trialDaysLeft = trialPeriod;

            lock (_syncObj)
            {
                string fullPath = GetFilePath(FileName);

                if (!File.Exists(fullPath))
                {
                    using (File.Create(fullPath))
                    {
                        File.SetAttributes(fullPath, FileAttributes.Hidden | FileAttributes.System | FileAttributes.ReadOnly);
                    }
                }
                else
                {
                    trialDaysLeft = trialPeriod - DateTime.Now.Subtract(File.GetCreationTime(fullPath)).Days;

                    if (trialDaysLeft > trialPeriod)
                        trialDaysLeft = -1;

                    if (File.ReadAllText(fullPath) == "/%")
                    {
                        trialDaysLeft = -1;
                    }
                    else if (trialDaysLeft < 0)
                    {
                        File.SetAttributes(fullPath, FileAttributes.Normal);
                        File.WriteAllText(fullPath, "/%");
                        File.SetAttributes(fullPath, FileAttributes.Hidden | FileAttributes.System | FileAttributes.ReadOnly);
                    }
                }
            }

            return trialDaysLeft;
        }

        protected virtual string GetFilePath(string fileName)
        {
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData), FileName);
        }

        #endregion
    }
}
