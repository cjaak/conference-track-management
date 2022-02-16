namespace ConferenceTrackManagement;

public class Talk
{
    public string Title { get; }
    public TimeSpan Duration { get; }

    public Talk(string title, TimeSpan duration)
    {
        Title = title;
        Duration = duration;
    }
}