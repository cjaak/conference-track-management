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

    /// <summary>
    /// Wrapper method for reading talks from file, validating them and turning them into a list of `Talk` objects
    /// </summary>
    /// <exception cref="ArgumentException">Invalid format</exception>
    public void CreateListOfTalks()
    {
        foreach (string talk in FetchTalksFromFile())
        {
            if (!ValidateTalk(talk))
            {
                throw new ArgumentException($"{talk} is NOT in a valid format");
            }
            Talks.Add(ConvertToTalk(talk));
        }
    }

    /// <summary>
    /// Checks an string for a valid talk format
    /// valid formats: 'title <minutes>min' or 'title lightning'
    /// </summary>
    /// <param name="talkString">line from the read file</param>
    /// <returns>true if valid, false if invalid</returns>
    internal bool ValidateTalk(string talkString)
    {
        Regex regexDefault = new Regex(@"^([a-zA-Z\s:.()-]+) [0-9]+min$"); //pattern for 'title <minutes>min' format
        Regex regexLightning = new Regex(@"^([a-zA-Z\s:.()-]+) lightning$"); //pattern for 'title lightning' format
        return regexDefault.IsMatch(talkString) || regexLightning.IsMatch(talkString);
    }

    /// <summary>
    /// Converts line into a `Talk` object
    /// </summary>
    /// <param name="talkString">read line</param>
    /// <returns>`Talk` object</returns>
    internal Talk ConvertToTalk(string talkString)
    {
        if (talkString.EndsWith("min"))
        {
            string extractedNumber = Regex.Match(talkString, @"\d+").Value;
            int minutes = Convert.ToInt32(extractedNumber);
            string title = Regex.Replace(talkString, @"[0-9]+min$", "").Trim();
            return new Talk(title, minutes);
        }
        else
        {
            string title = talkString.Substring(0, talkString.Length -10);
            return new Talk(title, 5);
        }
    }
    
    /// <summary>
    /// Reads all lines from a file specified at `FilePath`
    /// </summary>
    /// <returns>string array of those lines</returns>
    private string[] FetchTalksFromFile()
    {
        return System.IO.File.ReadAllLines(FilePath);
    }
}