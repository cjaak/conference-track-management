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

    public List<string> CreatePrintableTrackTimeTable(DateTime morningStart, DateTime afternoonStart, DateTime lunchStart, TimeSpan minNetworkingWaiting)
    {
        List<string> track = new List<string>();
        track.AddRange(MorningSession.CreatePrintableList(morningStart));
        track.Add($"{lunchStart:hh:mm tt} Lunch");
        track.AddRange(AfternoonSession.CreatePrintableList(afternoonStart));
        TimeSpan timeBeforeNetworkingCanStart = AfternoonSession.MaxDuration - minNetworkingWaiting;
        DateTime networking;
        //Assumption: Networking Event starts right after all afternoon talks are finished.
        //              Exception: Afternoon activities finish early
        if (AfternoonSession.RestMinutes > timeBeforeNetworkingCanStart)
        {
            networking = afternoonStart.Add(minNetworkingWaiting);
        }
        else
        {
            TimeSpan actualAfternoonDuration = AfternoonSession.MaxDuration - AfternoonSession.RestMinutes; 
            networking = afternoonStart.Add(actualAfternoonDuration);
        }
        track.Add($"{networking:hh:mm tt} Networking Event");
        return track;
    }
    

    public override string ToString()
    {
        return $"{nameof(MorningSession)}: {MorningSession}, {nameof(AfternoonSession)}: {AfternoonSession}";
    }
}