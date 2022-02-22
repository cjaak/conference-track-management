using System;
using System.Collections.Generic;
using System.Threading.Channels;
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

        Assert.That(manager.Tracks.Count, Is.EqualTo(2));

    }

    [Test]
    public void PrintConferenceTimeTable_ListOfTracks_FormattedList()
    {
        ConferenceManager manager = new ConferenceManager();
        Track track1 = new Track(180, 240);
        Track track2 = new Track(180, 240);
        
        track1.MorningSession.TalksInSession.Add(new Talk("Sample A", 180));
        track1.AfternoonSession.TalksInSession.Add(new Talk("Sample B", 240));
        track1.AfternoonSession.RestMinutes = new TimeSpan(0);
        
        track2.MorningSession.TalksInSession.Add(new Talk("Sample C", 180));
        track2.AfternoonSession.TalksInSession.Add(new Talk("Sample B", 120));
        track2.AfternoonSession.RestMinutes = new TimeSpan(0,120,0);

        List<string> expected = new List<string>()
        {
            "Track 1:",
            "09:00 AM Sample A 180min",
            "12:00 PM Lunch",
            "01:00 PM Sample B 240min",
            "05:00 PM Networking Event",
            "Track 2:",
            "09:00 AM Sample A 180min",
            "12:00 PM Lunch",
            "01:00 PM Sample B 120min",
            "04:00 PM Networking Event",
        };

        List<string> output = manager.PrintConferenceTimeTable();

        Assert.That(output, Is.EqualTo(expected));
    }
}