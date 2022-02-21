using System;

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
        }
    }
}