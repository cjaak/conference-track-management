using System.Text.RegularExpressions;

namespace ConferenceTrackManagement;

public class InputHandler
{
    public string FilePath { get; }

    public InputHandler(string filePath)
    {
        FilePath = filePath;
    }

    internal bool ValidateTalk(string talk)
    {
        Regex regexDefault = new Regex("([a-zA-Z]+( [a-zA-Z]+)+) [0-9]+min", RegexOptions.IgnoreCase);
        Regex regexLightning = new Regex("([a-zA-Z]+( [a-zA-Z]+)+) lightning");
        return regexDefault.IsMatch(talk) || regexLightning.IsMatch(talk);
    }
}