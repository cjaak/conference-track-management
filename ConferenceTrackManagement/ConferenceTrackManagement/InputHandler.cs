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
            string title = Regex.Replace(talkString, @"[0-9]+min$", "").Trim();
            TimeSpan duration = new TimeSpan(0, minutes, 0);
            return new Talk(title, duration);
        }
        else
        {
            string title = talkString.Substring(0, talkString.Length -10);
            TimeSpan duration = new TimeSpan(0, 5, 0);
            return new Talk(title, duration);
        }
    }
}