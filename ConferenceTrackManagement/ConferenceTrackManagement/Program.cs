using System;
using Castle.Components.DictionaryAdapter;

namespace ConferenceTrackManagement
{
    internal class Program
    {
        private const string path = @"Data/input_file.txt";
        static void Main(string[] args)
        {
            #region Configure
            DateTime date = DateTime.Now;
            Configuration config = new Configuration(
                180,
                240,
                new DateTime(date.Year, date.Month, date.Day, 9,0,0),
                new DateTime(date.Year, date.Month, date.Day, 13,0,0),
                new DateTime(date.Year, date.Month, date.Day, 12,0,0),
                new DateTime(date.Year, date.Month, date.Day, 16,0,0)
            );
            #endregion
            
            InputHandler handler = new InputHandler(path);
            handler.CreateListOfTalks();
            Console.WriteLine("CONVERTED INPUT: ");
            Console.WriteLine(string.Join("\n", handler.Talks));


            ConferenceManager manager = new ConferenceManager(config);
            manager.ScheduleConference(handler.Talks);

            List<string> formattedTable = manager.PrintConferenceTimeTable();
            Console.WriteLine("CONFERENCE TIMETABLE: ");
            Console.WriteLine(string.Join("\n", formattedTable));
        }
    }
}