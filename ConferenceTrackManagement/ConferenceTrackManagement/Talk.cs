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

    public override string ToString()
    {
        return $"{nameof(Title)}: {Title}, {nameof(Duration)}: {Duration}";
    }
}