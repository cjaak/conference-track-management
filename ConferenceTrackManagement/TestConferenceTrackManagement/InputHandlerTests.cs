using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        string path =  @"Data/test_input_invalid.txt";
        InputHandler handler = new InputHandler(path);
        
        
        var ex = Assert.Throws<ArgumentException>(
            ()=> handler.CreateListOfTalks());
        
        Assert.That(handler.Talks.Count, Is.EqualTo(0));
    }

    [Test]
    public void CreateListOfTalks_ValidTalks_ReturnsListOfTalks()
    {
        string path =  @"Data/test_input_valid.txt";
        InputHandler handler = new InputHandler(path);

        Talk expectedFirstElement = new Talk("Some Dummy Title", new TimeSpan(0, 45, 0));
        Talk expectedSecondElement = new Talk("Another Title", new TimeSpan(0, 5, 0));
        handler.CreateListOfTalks();
        
        
        Assert.That(handler.Talks.Count, Is.EqualTo(2));
        
        Assert.That(handler.Talks[0].Title, Is.EqualTo(expectedFirstElement.Title));
        Assert.That(handler.Talks[0].Duration, Is.EqualTo(expectedFirstElement.Duration));
        Assert.That(handler.Talks[1].Title, Is.EqualTo(expectedSecondElement.Title));
        Assert.That(handler.Talks[1].Duration, Is.EqualTo(expectedSecondElement.Duration));
    }
}