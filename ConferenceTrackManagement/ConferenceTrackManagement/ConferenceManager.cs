namespace ConferenceTrackManagement;

public class ConferenceManager
{
    public List<Track> Tracks {get;}

    public ConferenceManager()
    {
        Tracks = new List<Track>();
    }

    public void ScheduleConference(List<Talk> talks, int morningMinutes, int afternoonMinutes)
    {
        do
        {
            Track track = new Track(morningMinutes, afternoonMinutes);
            talks = track.FillTrack(talks);
            Tracks.Add(track);
        } while (talks.Count > 0);
    }

    public List<string> PrintConferenceTimeTable()
    {
        var dateNow = DateTime.Now;
        var morningStart = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 9, 0, 0);
        var afternoonStart = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 13, 0, 0);
        var lunchStart = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 12, 0, 0);
        TimeSpan networkingWaitTime = new TimeSpan(0, 180, 0);
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