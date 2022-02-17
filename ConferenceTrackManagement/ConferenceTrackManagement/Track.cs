using Castle.DynamicProxy.Generators;

namespace ConferenceTrackManagement;

public class Track
{
    public Session MorningSession { get; }
    public Session AfternoonSession { get; }

    public Track(int morningMinutes, int afernoonMinutes)
    {
        MorningSession = new Session(new TimeSpan(0,morningMinutes, 0));
        AfternoonSession = new Session(new TimeSpan(0, afernoonMinutes, 0));
    }
    
}