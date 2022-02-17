using System;
using ConferenceTrackManagement;
using NUnit.Framework;

namespace TestConferenceTrackManagement;

[TestFixture]
public class SessionTests
{
    [Test]
    public void ScheduleTalkAndUpdateRestMinutes_TalkObject_SetsTalkAndRestMinutesForSession()
    {
        Session session = new Session(180);
        Talk input = new Talk("Dummy Title", 30);
        TimeSpan expectedDuaration = new TimeSpan(0, 150, 0);
        
        session.ScheduleTalkAndUpdateRestMinutes(input);

        Assert.That(session.RestMinutes, Is.EqualTo(expectedDuaration));
        Assert.That(session.TalksInSession[0].Title, Is.EqualTo(input.Title));
        Assert.That(session.TalksInSession[0].Duration, Is.EqualTo(input.Duration));
    }
}