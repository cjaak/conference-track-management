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
            new Talk("Sixth", 100),
            new Talk("Seventh", 50)
        };

        List<Talk> expectedMorningList = new List<Talk>()
        {
            new Talk("First", 100),
            new Talk("Fourth", 80),
        };
        
        List<Talk> expectedAfternoonList = new List<Talk>()
        {
            new Talk("Second", 100),
            new Talk("Third", 70),
            new Talk("Fifth", 30),
            new Talk("Eighth", 40)
        };

        List<Talk> output = track.FillTrack(input);

        Assert.That(track.MorningSession.RestMinutes, Is.EqualTo(new TimeSpan(0)));
        Assert.That(track.AfternoonSession.RestMinutes, Is.EqualTo(new TimeSpan(0)));

        Assert.That(track.MorningSession.TalksInSession, Is.EquivalentTo(expectedMorningList));
        Assert.That(track.AfternoonSession.TalksInSession, Is.EquivalentTo(expectedAfternoonList));
        
        Assert.That(output, Is.EquivalentTo(expectedReturn));
    }

    [Test]
    public void CreatePrintableTrackTimeTable_EarlyFinish_ReturnsListOfAllFormattedEvents()
    {
        Track track = new Track(180, 240);
        
        var dateNow = DateTime.Now;
        var morningStart = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 9, 0, 0);
        var afternoonStart = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 13, 0, 0);
        var lunchStart = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 12, 0, 0);
        TimeSpan networkingWaitTime = new TimeSpan(0, 60, 0);

        track.MorningSession.TalksInSession = new List<Talk>()
        {
            new Talk("Sample A", 90)
        };

        track.AfternoonSession.RestMinutes = new TimeSpan(0, 80, 0);

        track.AfternoonSession.TalksInSession = new List<Talk>()
        {
            new Talk("Sample B", 120)
        };

        List<string> expectedOutput = new List<string>()
        {
            "09:00 AM Sample A 90min",
            "12:00 PM Lunch",
            "01:00 PM Sample B 120min",
            "04:00 PM Networking Event"
        };

        List<string> output =
            track.CreatePrintableTrackTimeTable(morningStart, afternoonStart, lunchStart, networkingWaitTime);

        Assert.That(output, Is.EqualTo(expectedOutput));
    }

    
}