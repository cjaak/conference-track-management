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
        string session = "morning";
        Talk? addedTalk;
        foreach (var talk in talks.ToList())
        {
            switch (session)
            {
                case "morning":
                    addedTalk = null;//ScheduleFittingTalk(talks, MorningSession, talk);
                    if (addedTalk is not null)
                    {
                        talks.Remove(addedTalk);
                    }
                    else
                    {
                        session = "afternoon";
                    }
                    break;
                case "afternoon":
                    addedTalk = null;//ScheduleFittingTalk(talks, AfternoonSession, talk);
                    if (addedTalk is not null)
                    {
                        talks.Remove(addedTalk);
                    }
                    else
                    {
                        session = "";
                    }
                    break;
                default:
                    break;
            }
        }
        return talks;
    }
    

   
    
}