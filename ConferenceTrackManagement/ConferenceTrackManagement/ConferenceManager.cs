namespace ConferenceTrackManagement;

public class ConferenceManager
{
    public List<Track> tracks {get;}

    public ConferenceManager()
    {
        tracks = new List<Track>();
    }

    public void ScheduleConference(List<Talk> talks, int morningMinutes, int afternoonMinutes)
    {
        do
        {
            Track track = new Track(morningMinutes, afternoonMinutes);
            talks = track.FillTrack(talks);
            tracks.Add(track);
        } while (talks.Count > 0);
    }
}