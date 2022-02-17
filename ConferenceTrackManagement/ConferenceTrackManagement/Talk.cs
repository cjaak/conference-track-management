namespace ConferenceTrackManagement;

public class Talk
{
    public string Title { get; }
    public TimeSpan Duration { get; }

    public Talk(string title, int minutes)
    {
        Title = title;
        Duration = new TimeSpan(0,minutes,0);
    }

    public override string ToString()
    {
        return $"{nameof(Title)}: {Title}, {nameof(Duration)}: {Duration}";
    }
}