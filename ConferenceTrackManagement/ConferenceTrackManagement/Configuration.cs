namespace ConferenceTrackManagement;

public class Configuration
{
    public int MorningSessionMinutes { get; set; }
    public int AfternoonSessionMinutes { get; set; }
    public DateTime MorningSessionStart { get; set; }
    public DateTime AfternoonSessionStart { get; set; }
    public DateTime LunchStart { get; set; }
    public DateTime NetworkingEarlyStart { get; set; }
    public TimeSpan NetworkingMinimumWaitingPeriod { get; set; }

    public Configuration(int morningSessionMinutes, int afternoonSessionMinutes, DateTime morningSessionStart, DateTime afternoonSessionStart, DateTime lunchStart, DateTime networkingEarlyStart)
    {
        MorningSessionMinutes = morningSessionMinutes;
        AfternoonSessionMinutes = afternoonSessionMinutes;
        MorningSessionStart = morningSessionStart;
        AfternoonSessionStart = afternoonSessionStart;
        LunchStart = lunchStart;
        NetworkingEarlyStart = networkingEarlyStart;
        NetworkingMinimumWaitingPeriod = NetworkingEarlyStart - AfternoonSessionStart;
    }
}