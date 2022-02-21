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

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }
        Talk talk = obj as Talk;
        if (talk.Duration == Duration && talk.Title == Title)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}