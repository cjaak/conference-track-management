using Castle.DynamicProxy.Generators;

namespace ConferenceTrackManagement;

public class Track
{
    public Session MorningSession { get; }
    public Session AfternoonSession { get; }

    public Track(int morningMinutes, int afternoonMinutes)
    {
        MorningSession = new Session(morningMinutes);
        AfternoonSession = new Session(afternoonMinutes);
    }

    public List<Talk> FillTrack(List<Talk> talks)
    {
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
    
}