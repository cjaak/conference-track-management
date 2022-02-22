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

    /// <summary>
    /// Fill morning and afternoon session with talks
    /// </summary>
    /// <param name="talks">available talks</param>
    /// <returns>list of remaining talks</returns>
    public List<Talk> FillTrack(List<Talk> talks)
    {
        List<Talk> restTalks = MorningSession.FillSession(talks);
        restTalks = AfternoonSession.FillSession(restTalks);
        return restTalks;
    }

    /// <summary>
    /// Creates an easy to read Timetable for the session. In addition to the filled sessions, social events like lunch
    /// and the networking event will also be listed in their timeslot.
    /// </summary>
    /// <param name="morningStart">start time of the morning session</param>
    /// <param name="afternoonStart">start time of the afternoon session</param>
    /// <param name="lunchStart">start time of lunch</param>
    /// <param name="minNetworkingWaiting">Minimum time that has to past after the start of the afternoon session in order to schedule the networking event</param>
    /// <returns>List of formatted strings containing each activity with their start time</returns>
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