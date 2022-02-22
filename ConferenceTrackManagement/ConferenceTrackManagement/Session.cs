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

    /// <summary>
    /// Iterates through available talks are tries to add the talk to the session. If the talk's duration is longer than
    /// the available rest minutes the largest fitting talk will be chosen instead.
    /// </summary>
    /// <param name="talks">List of talks, that are still available</param>
    /// <returns>List of talks that were not used</returns>
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
    
    /// <summary>
    /// Formats all talks into a list of strings. Each string follows the format "<hh:mm> <title> <minutes>min"
    /// </summary>
    /// <param name="start"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Iterates through the available talks and returns the talk with the biggest duration within the given limit.
    /// </summary>
    /// <param name="talks">List of talks, that are still available</param>
    /// <param name="limit">upper limit for duration</param>
    /// <returns>Better fitting talk or Talk with duration of 0, if none was found</returns>
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

    /// <summary>
    /// Wrapper Method for adding talk to session and setting the rest minutes
    /// </summary>
    /// <param name="talk">the talk to be added</param>
    /// <returns>true, if successful added or false if the talks duration is too big for the rest of the session</returns>
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

    /// <summary>
    /// Calculates remaining minutes for the session
    /// </summary>
    /// <param name="duration">duration of the just added talk</param>
    /// <returns>rest minutes</returns>
    private TimeSpan CalculateRestMinutes(TimeSpan duration)
    {
        return RestMinutes - duration;
    }

    public override string ToString()
    {
        return $"{nameof(MaxDuration)}: {MaxDuration}, {nameof(RestMinutes)}: {RestMinutes}, {nameof(TalksInSession)}: {string.Join(", ", TalksInSession)}";
    }
}