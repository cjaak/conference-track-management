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
        Configuration config = new Configuration(
            180,
             240,
            new DateTime(),
            new DateTime(), 
            new DateTime(),
            new DateTime()
            );
        
        ConferenceManager manager = new ConferenceManager(config);
        
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
        
        manager.ScheduleConference(input);

        Assert.That(manager.Tracks.Count, Is.EqualTo(2));

    }

    [Test]
    public void PrintConferenceTimeTable_ListOfTracks_FormattedList()
    {
        DateTime date = DateTime.Now;
        Configuration config = new Configuration(
            180,
            240,
            new DateTime(date.Year, date.Month, date.Day, 9,0,0),
            new DateTime(date.Year, date.Month, date.Day, 13,0,0),
            new DateTime(date.Year, date.Month, date.Day, 12,0,0),
            new DateTime(date.Year, date.Month, date.Day, 16,0,0)
        );
        
        ConferenceManager manager = new ConferenceManager(config);
        Track track1 = new Track(180, 240);
        Track track2 = new Track(180, 240);
        
        track1.MorningSession.TalksInSession.Add(new Talk("Sample A", 180));
        track1.AfternoonSession.TalksInSession.Add(new Talk("Sample B", 240));
        track1.AfternoonSession.RestMinutes = new TimeSpan(0);
        
        track2.MorningSession.TalksInSession.Add(new Talk("Sample C", 180));
        track2.AfternoonSession.TalksInSession.Add(new Talk("Sample D", 120));
        track2.AfternoonSession.RestMinutes = new TimeSpan(0,120,0);

        manager.Tracks.Add(track1);
        manager.Tracks.Add(track2);

        List<string> expected = new List<string>()
        {
            "Track 1:",
            "09:00 AM Sample A 180min",
            "12:00 PM Lunch",
            "01:00 PM Sample B 240min",
            "05:00 PM Networking Event",
            "Track 2:",
            "09:00 AM Sample C 180min",
            "12:00 PM Lunch",
            "01:00 PM Sample D 120min",
            "04:00 PM Networking Event",
        };

        List<string> output = manager.PrintConferenceTimeTable();

        Assert.That(output, Is.EqualTo(expected));
    }
}