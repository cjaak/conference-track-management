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
            new Talk("Foo", 100),
            new Talk("bar", 100),
            new Talk("foo foo", 70),
            new Talk("Foo Bar", 80),
            new Talk("bar bar", 30),
            new Talk("Not Used", 50),
            new Talk("bar foo", 40)
        };
        
        
    }
}