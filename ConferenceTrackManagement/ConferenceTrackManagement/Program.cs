using System;
using Castle.Components.DictionaryAdapter;

namespace ConferenceTrackManagement
{
    internal class Program
    {
        private const string path = @"Data/input_file.txt";
        static void Main(string[] args)
        {
            InputHandler handler = new InputHandler(path);
            handler.CreateListOfTalks();
            Console.WriteLine(string.Join("\n", handler.Talks));
            
            ConferenceManager manager = new ConferenceManager();
            manager.ScheduleConference(handler.Talks, 180, 240);

            List<string> formattedTable = manager.PrintConferenceTimeTable();
            Console.WriteLine(string.Join("\n", formattedTable));
        }
    }
}