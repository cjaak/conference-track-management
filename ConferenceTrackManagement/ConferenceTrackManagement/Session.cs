namespace ConferenceTrackManagement;

public class Session
{
    public TimeSpan MaxDuration { get; }
    public TimeSpan RestMinutes { get; set; }
    
    public List<Talk> TalksInSession { get; }

    public Session(int maxMinutes)
    {
        MaxDuration = new TimeSpan(0,maxMinutes ,0);
        RestMinutes = MaxDuration;
        TalksInSession = new List<Talk>();
    }

    public void ScheduleTalkAndUpdateRestMinutes(Talk talk)
    {
        TalksInSession.Add(talk);
        RestMinutes = CalculateRestMinutes(talk.Duration);
    }

    private TimeSpan CalculateRestMinutes(TimeSpan duration)
    {
        return MaxDuration - duration;
    }
}