using System;


namespace TAlex.Common.Licensing
{
    public interface ITrialPeriodDataProvider
    {
        int GetTrialDaysLeft(int trialPeriod);
    }
}
