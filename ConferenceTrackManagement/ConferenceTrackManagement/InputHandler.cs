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
        Regex regex = new Regex("([a-zA-Z]+( [a-zA-Z]+)+) [0-9]+min", RegexOptions.IgnoreCase);
        if (regex.IsMatch(talk))
        {
            return true;
        }
        return false;
    }
}