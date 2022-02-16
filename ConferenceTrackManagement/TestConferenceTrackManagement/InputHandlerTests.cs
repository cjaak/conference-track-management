using ConferenceTrackManagement;
using NUnit.Framework;

namespace TestConferenceTrackManagement;

public class InputHandlerTests
{
    [Test]
    public void ValidateTalk_Title30min_ReturnsTrue()
    {
        InputHandler handler = new InputHandler("");
        string input = "dummy title 30min";
        bool output = handler.ValidateTalk(input);
        Assert.That(output, Is.True);
    }

    [Test]
    public void ValidateTalk_Title_With_Numbers_ReturnsFalse()
    {
        InputHandler handler = new InputHandler("");
        string input = "dummy 123 title 30min";
        bool output = handler.ValidateTalk(input);
        Assert.That(output, Is.False);
    }
}