using System;
using System.Collections.Generic;
using ConferenceTrackManagement;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace TestConferenceTrackManagement;

[TestFixture]
public class InputHandlerTests
{
    Mock<InputHandler> _handlerMock;
    
    
    [Test]
    public void ValidateTalk_Title30min_ReturnsTrue()
    {
        InputHandler handler = new InputHandler("");
        string input = "dummy title 30min";
        
        bool output = handler.ValidateTalk(input);
        
        Assert.That(output, Is.True);
    }

    [Test]
    public void ValidateTalk_TitleWithNumbers_ReturnsFalse()
    {
        InputHandler handler = new InputHandler("");
        string input = "dummy 123 title 30min";
        
        bool output = handler.ValidateTalk(input);
        
        Assert.That(output, Is.False);
    }

    [Test]
    public void ValidateTalk_TitleLightning_ReturnsTrue()
    {
        InputHandler handler = new InputHandler("");
        string input = "dummy title lightning";
        
        bool output = handler.ValidateTalk(input);
        
        Assert.That(output, Is.True);
    }

    [Test]
    public void ConvertToTalk_Title30min_ReturnsTalk()
    {
        InputHandler handler = new InputHandler("");
        string input = "dummy title 30min";
        Talk expected = new Talk("dummy title", new TimeSpan(0, 30, 0));

        Talk output = handler.ConvertToTalk(input);

        Assert.That(output.Title, Is.EqualTo(expected.Title));
        Assert.That(output.Duration, Is.EqualTo(expected.Duration));
    }

    [Test]
    public void ConvertToTalk_TitleLightning_ReturnsTalk()
    {
        InputHandler handler = new InputHandler("");
        string input = "dummy title lightning";
        Talk expected = new Talk("dummy title", new TimeSpan(0, 5, 0));

        Talk output = handler.ConvertToTalk(input);
        
        Assert.That(output.Title, Is.EqualTo(expected.Title));
        Assert.That(output.Duration, Is.EqualTo(expected.Duration));
    }

    [Test]
    public void CreateListOfTalks_InvalidTalk_ThrowsException()
    {
        _handlerMock = new Mock<InputHandler>();
        _handlerMock.Setup(h => h.ValidateTalk("")).Returns(false);
        string[] dummyTalkStringArray = new string[] { "" };
        _handlerMock.Setup(h=> h.FetchTalksFromFile()).Returns(dummyTalkStringArray);

        _handlerMock.Object.CreateListOfTalks();
        _handlerMock.Verify(h => h.ValidateTalk, Times.Once);
        Assert.That(_handlerMock.Object.Talks.Count, Is.Zero);
        Assert.Throws<ArgumentException>(
            ()=>
            {
                throw new ArgumentException();
            });
    }
}