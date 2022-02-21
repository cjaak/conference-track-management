using System.Collections.Generic;
using ConferenceTrackManagement;
using NUnit.Framework;

namespace TestConferenceTrackManagement;

[TestFixture]
public class ConferenceManagerTests
{
    [Test]
    public void ScheduleConference_ListOfTalks_ListHasLength2()
    {
        ConferenceManager manager = new ConferenceManager();
        
        List<Talk> input = new List<Talk>()
        {
            new Talk("First", 100),
            new Talk("Second", 100),
            new Talk("Third", 70),
            new Talk("Fourth", 80),
            new Talk("Fifth", 30),
            new Talk("Sixth", 100),
            new Talk("Seventh", 50),
            new Talk("Eighth", 40)
        };
        
        manager.ScheduleConference(input, 100, 200);

        Assert.That(manager.tracks.Count, Is.EqualTo(2));

    }
}