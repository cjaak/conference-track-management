using ConferenceTrackManagement;
using NUnit.Framework;

namespace TestConferenceTrackManagement;

public class InputHandlerTests
{
    [Test]
    public void ValidateTalk_Title30min_ReturnsTrue()
    {
        string input = "dummy title 30min";
        bool output = InputHandler.ValidateTalk(input);
        Assert.That(output, Is.True);
    }
}