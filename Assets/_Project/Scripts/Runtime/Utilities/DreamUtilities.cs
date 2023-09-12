using System;

namespace DreamTeam.Runtime.Utilities
{
    public static class DreamUtilites
    {
        public static string ConvertToHoursMinutesSeconds(float totalSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(totalSeconds);

            return string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
        }
    }
}
