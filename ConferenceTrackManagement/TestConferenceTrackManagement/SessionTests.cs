using System;
using System.Collections.Generic;
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
    
    [Test]
    public void FindTalkWithMaxDurationForLimit_ListOfTalksAndLimit_ReturnsMaxTalkWithinLimit()
    {
        Session session = new Session(0);
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

        Talk output = session.FindTalkWithMaxDurationForLimit(input, new TimeSpan(0, 60, 0));

        Assert.That(output, Is.EqualTo(input[6]));
    }

    [Test]
    public void FillSession_ListOfTalks_ReturnsTalksThatWereNotUsed()
    {
        Session session = new Session(180);
        
        List<Talk> input = new List<Talk>()
        {
            new Talk("First", 100),
            new Talk("Second", 100),
            new Talk("Third", 70),
            new Talk("Fourth", 80),
        };
        
        List<Talk> expectedInSession = new List<Talk>()
        {
            new Talk("First", 100),
            new Talk("Fourth", 80),
        };
        
        List<Talk> expectedOutput = new List<Talk>()
        {
            new Talk("Second", 100),
            new Talk("Third", 70),
        };

        List<Talk> output = session.FillSession(input);
        
        Assert.That(session.TalksInSession, Is.EquivalentTo(expectedInSession));
        Assert.That(output, Is.EquivalentTo(expectedOutput));
        Assert.That(session.TalksInSession.Count, Is.EqualTo(2));
    }
    
}