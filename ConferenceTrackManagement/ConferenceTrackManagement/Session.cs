namespace ConferenceTrackManagement;

public class Session
{
    public TimeSpan MaxDuration { get; }
    public TimeSpan RestMinutes { get; set; }
    
    public List<Talk> TalksInSession { get; }

    public Session(TimeSpan maxDuration)
    {
        MaxDuration = maxDuration;
        RestMinutes = maxDuration;
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