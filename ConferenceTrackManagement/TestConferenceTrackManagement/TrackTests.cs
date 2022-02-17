using System;
using System.Collections.Generic;
using ConferenceTrackManagement;
using NUnit.Framework;

namespace TestConferenceTrackManagement;

[TestFixture]
public class TrackTests
{
    [Test]
    public void FillTrack_ListOfTalks_ReturnsTalksThatWereNotUsed()
    {
        Track track = new Track(180, 240);
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

        List<Talk> expectedReturn = new List<Talk>()
        {
            new Talk("Seventh", 50)
        };

        List<Talk> expectedMorningList = new List<Talk>()
        {
            new Talk("First", 100),
            new Talk("Second", 100),
            new Talk("Fourth", 80),
        };
        
        List<Talk> expectedAfternoonList = new List<Talk>()
        {
            new Talk("Third", 70),
            new Talk("Fifth", 30),
            new Talk("Sixth", 100),
            new Talk("Eighth", 40)
        };

        List<Talk> output = track.FillTracks(input);

        Assert.That(track.MorningSession.RestMinutes, Is.Zero);
        Assert.That(track.AfternoonSession.RestMinutes, Is.Zero);

        Assert.That(track.MorningSession.TalksInSession, Is.EquivalentTo(expectedMorningList));
        Assert.That(track.AfternoonSession.TalksInSession, Is.EquivalentTo(expectedAfternoonList));
        
        Assert.That(output, Is.EquivalentTo(expectedReturn));
    }
}