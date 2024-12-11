//this is a journal entry program, which also has little prompts to spark the users memory of day.


//directives that allow access to certain things that the prog needs
using System;
using System.Collections.Generic;
using System.IO;


//makes new namespace
namespace JournalProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            //connects journal class to main function
            Journal myJournal = new Journal();
            
            while (true)
            {
                Console.WriteLine("\nJournal Menu:");
                Console.WriteLine("1. Write a new journal entry.");
                Console.WriteLine("2. Display the journal entry.");
                Console.WriteLine("3. Save the journal.");
                Console.WriteLine("4. Load a journal.");
                Console.WriteLine("5. Exit");
                Console.WriteLine("Choose an number: ");

                string choice = Console.ReadLine();


                //connects the options of user to actions
                switch (choice)
                {
                    case "1":
                        myJournal.AddEntry();
                        break;
                    case "2":
                        myJournal.DisplayEntries();
                        break;
                    case "3":
                        Console.Write("Enter file name: ");
                        string saveFilename = Console.ReadLine();
                        myJournal.SaveToFile(saveFilename);
                        break;
                    case "4":
                        Console.Write("Enter file name to load: ");
                        string loadFilename = Console.ReadLine();
                        myJournal.LoadFromFile(loadFilename);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid response... Please try a valid number.");
                        break;
                }
            }
        }
    }

    class Journal
    {
        private List<Entry> entries;
        private List<string> prompts;

        public Journal()
        {
            entries = new List<Entry>();
            prompts = new List<string>
            {
                "Who was the most interesting person you interacted with today?",
                "What was the best part of your day?",
                "How did you see the hand of the Lord in your day?",
                "Did you miss anyone or anything today?",
                "If you had one thing that you could do over today, what would it be?",
                "On a scale of 1-10, how happy were you today? Why do you think that is?"
            };
        }

        
        //function to add an entry to journal
        public void AddEntry()
        {
            Random random = new Random();
            string prompt = prompts[random.Next(prompts.Count)];

            Console.WriteLine("\nToday's prompt: " + prompt);
            Console.Write("Your response: ");
            string response = Console.ReadLine();

            string date = DateTime.Now.ToString("yyyy-MM-dd");
            entries.Add(new Entry(prompt, response, date));
            Console.WriteLine("Entry added.");
        }

        public void DisplayEntries()
        {
            Console.WriteLine("\nJournal Entries:");
            if (entries.Count == 0)
            {
                Console.WriteLine("No entries to display.");
                return;
            }

            foreach (var entry in entries)
            {
                Console.WriteLine($"Date: {entry.Date}\nPrompt: {entry.Prompt}\nResponse: {entry.Response}\n");
            }
        }

        public void SaveToFile(string filename)
        {
            //allows entry to be saved to a file.
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var entry in entries)
                {
                    writer.WriteLine($"{entry.Date}~|~{entry.Prompt}~|~{entry.Response}");
                }
            }
            Console.WriteLine("Journal saved con exito! Guao!");
        }

        public void LoadFromFile(string filename)
        {
            try
            {
                entries.Clear();
                using (StreamReader reader = new StreamReader(filename))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(new[] { "~|~" }, StringSplitOptions.None);
                        if (parts.Length == 3)
                        {
                            entries.Add(new Entry(parts[1], parts[2], parts[0]));
                        }
                    }
                }
                Console.WriteLine("Journal loaded successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading file: " + e.Message);
            }
        }
    }

    //this stores the users input
    class Entry
    {
        public string Prompt { get; }
        public string Response { get; }
        public string Date { get; }

        public Entry(string prompt, string response, string date)
        {
            Prompt = prompt;
            Response = response;
            Date = date;
        }
    }
}
