namespace ConferenceTrackManagement;

public class Session
{
    public TimeSpan MaxDuration { get; }
    public TimeSpan RestMinutes { get; set; }
    
    public List<Talk> TalksInSession { get; set;}

    public Session(int maxMinutes)
    {
        MaxDuration = new TimeSpan(0,maxMinutes ,0);
        RestMinutes = MaxDuration;
        TalksInSession = new List<Talk>();
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
                    talks.Remove(bestFittingTalk);
                }
            }
        }
        return talks;
    }

    public List<string> CreatePrintableList(DateTime start)
    {
        List<string> pintableList = new List<string>();
        foreach (var talk in TalksInSession)
        {
            string formattedTalk = $"{start.ToString("hh:mm tt")} {talk.Title} {talk.Duration.TotalMinutes}min";
            start += talk.Duration;
            pintableList.Add(formattedTalk);
        }
        return pintableList;
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

    public override string ToString()
    {
        return $"{nameof(MaxDuration)}: {MaxDuration}, {nameof(RestMinutes)}: {RestMinutes}, {nameof(TalksInSession)}: {string.Join(", ", TalksInSession)}";
    }
}