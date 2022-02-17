using System.Text.RegularExpressions;

namespace ConferenceTrackManagement;

public class InputHandler
{
    public string FilePath { get; }
    
    public List<Talk> Talks { get; }
    

    public InputHandler(string filePath)
    {
        FilePath = filePath;
        Talks = new List<Talk>();
    }

    public void CreateListOfTalks()
    {
        foreach (var talk in FetchTalksFromFile())
        {
            if (!ValidateTalk(talk))
            {
                throw new ArgumentException($"{talk} is NOT in a valid format");
            }
        }
    }

    internal virtual bool ValidateTalk(string talkString)
    {
        Regex regexDefault = new Regex("([a-zA-Z]+( [a-zA-Z]+)+) [0-9]+min"); //pattern for 'title <minutes>min' format
        Regex regexLightning = new Regex("([a-zA-Z]+( [a-zA-Z]+)+) lightning"); //pattern for 'title lightning' format
        return regexDefault.IsMatch(talkString) || regexLightning.IsMatch(talkString);
    }

    internal virtual Talk ConvertToTalk(string talkString)
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

    internal virtual string[] FetchTalksFromFile()
    {
        return System.IO.File.ReadAllLines(FilePath);
    }
}