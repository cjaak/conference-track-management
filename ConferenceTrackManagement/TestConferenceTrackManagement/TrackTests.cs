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
            new Talk(),
            new Talk(),
            new Talk(),
            new Talk(),
            new Talk(),
        };
    }
}