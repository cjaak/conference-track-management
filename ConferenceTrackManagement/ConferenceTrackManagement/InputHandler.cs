using System.Text.RegularExpressions;

namespace ConferenceTrackManagement;

public class InputHandler
{
    public string FilePath { get; }
    

    public InputHandler(string filePath)
    {
        FilePath = filePath;
    }

    internal bool ValidateTalk(string talkString)
    {
        Regex regexDefault = new Regex("([a-zA-Z]+( [a-zA-Z]+)+) [0-9]+min");
        Regex regexLightning = new Regex("([a-zA-Z]+( [a-zA-Z]+)+) lightning");
        return regexDefault.IsMatch(talkString) || regexLightning.IsMatch(talkString);
    }

    internal Talk ConvertToTalk(string talkString)
    {
        if (talkString.EndsWith("min"))
        {
            string extractedNumber = Regex.Match(talkString, @"\d+").Value;
            int minutes = Convert.ToInt32(extractedNumber);
            string title = Regex.Match(talkString, @"^[0-9]+").Value.Trim();
            TimeSpan duration = new TimeSpan(0, minutes, 0);
            return new Talk(title, duration);
        }
        else
        {
            return new Talk("", new TimeSpan(0, 0, 0));
        }
    }
}