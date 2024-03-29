namespace ConferenceTrackManagement;

public class ConferenceManager
{
    public List<Track> Tracks {get;}

    private Configuration Config { get; set; }

    public ConferenceManager(Configuration config)
    {
        Tracks = new List<Track>();
        Config = config;
    }

    /// <summary>
    /// Creates Tracks and fills them, until all talks are scheduled
    /// </summary>
    /// <param name="talks">Talks that are to be scheduled</param>
    public void ScheduleConference(List<Talk> talks)
    {
        do
        {
            Track track = new Track(Config.MorningSessionMinutes, Config.AfternoonSessionMinutes);
            talks = track.FillTrack(talks);
            Tracks.Add(track);
        } while (talks.Count > 0);
    }

   
    /// <summary>
    /// Formats all tracks into an easy to read list
    /// </summary>
    /// <returns>Events for each track</returns>
    public List<string> PrintConferenceTimeTable()
    {
        DateTime morningStart = Config.MorningSessionStart;
        DateTime afternoonStart = Config.AfternoonSessionStart;
        DateTime lunchStart = Config.LunchStart;
        TimeSpan networkingWaitTime = Config.NetworkingMinimumWaitingPeriod;
        List<string> formattedTable = new List<string>();
        for (int i = 0; i < Tracks.Count; i++)
        {
            string title = $"Track {i + 1}:";
            formattedTable.Add(title);
            List<string> formattedTrack = Tracks[i].CreatePrintableTrackTimeTable(morningStart, afternoonStart, lunchStart, networkingWaitTime);
            formattedTable.AddRange(formattedTrack);
        }
        return formattedTable;
    }
}