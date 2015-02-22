using System;


namespace TAlex.Common.Licensing
{
    public interface ILicenseDataManager
    {
        LicenseData Load();

        void Save(LicenseData licenseData);
    }
}
