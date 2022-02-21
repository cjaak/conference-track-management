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
        Console.WriteLine("MINUTES LEFT" + RestMinutes);
    }

    public List<Talk> FillSession(List<Talk> talks)
    {
        foreach (var talk in talks.ToList())
        {
            if (ScheduleTalkAndUpdateRestMinutes(talk))
            {
                talks.Remove(talk);
            }
            else
            {
                Talk bestFittingTalk = FindTalkWithMaxDurationForLimit(talks, RestMinutes);
                if (bestFittingTalk.Duration > new TimeSpan(0))
                {
                    ScheduleTalkAndUpdateRestMinutes(bestFittingTalk);
                    talks.Remove(talk);
                }
            }
        }
        return talks;
    }
    
    
    
    internal Talk FindTalkWithMaxDurationForLimit(List<Talk> talks, TimeSpan limit)
    {
        Talk max = new Talk("", 0);
        foreach (var talk in talks)
        {
            if(talk.Duration <= limit && talk.Duration > max.Duration)
            {
                max = talk;
            }
        }
        return max;
    }

    internal bool ScheduleTalkAndUpdateRestMinutes(Talk talk)
    {
        if (talk.Duration > RestMinutes)
        {
            return false;
        }
        TalksInSession.Add(talk);
        RestMinutes = CalculateRestMinutes(talk.Duration);
        return true;
    }

    private TimeSpan CalculateRestMinutes(TimeSpan duration)
    {
        return RestMinutes - duration;
    }
}