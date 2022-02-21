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
        List<Talk> restTalks = MorningSession.FillSession(talks);
        restTalks = AfternoonSession.FillSession(restTalks);
        return restTalks;
    }
    

   
    
}