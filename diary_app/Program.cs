using System;
using System.Security.Cryptography.X509Certificates;
using DiaryEntryNameSpace;
using System.IO;
using System.Reflection;


namespace DiaryApp
{
    class Program {

        static void Main()
        {

            Console.WriteLine("\nHello! What do you want to do?");
            Console.WriteLine("Please select the corresponding letter and press Enter to continue.\n");
            Console.WriteLine("'a' to Add an entry to the diary\n'r' to Read entries\n" +
                              "'m' to Modify entries\n'd' to Delete entries\n\n'q' to Quit the application\n");


            while (true)
            {


                string answer = Console.ReadLine();
                string aLow = answer.ToLower();

                switch (aLow)
                {
                    case "a":
                        Console.WriteLine("");
                        Add(); 
                        break;

                    case "r":
                        Console.WriteLine("\n");
                        Read();
                        break;

                    case "m":
                        Console.WriteLine("\n");
                        Modify();
                        break;

                    case "d":
                        Console.WriteLine("\n");
                        Delete();
                        break;

                    case "q":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("\nInput is not valid, please try again.");
                        break;
                }
                Console.WriteLine("\nWaiting for input:");
            }

            void Add()
            {
                StreamWriter writer = File.AppendText("diary.txt");
                Console.WriteLine("Please choose a title for your entry:");
                string title = Console.ReadLine();
                Console.WriteLine("\nEnter text your diary entry:");
                string text = Console.ReadLine();

                DiaryEntry entry = new DiaryEntry(title, text);
 
                writer.WriteLine(entry.Title + ";" + entry.Text + ";" + DateTime.Now);
                writer.Close();
                Console.WriteLine("\nEntry added!\n");
            }

            void Read()
            {
                Console.WriteLine("Please choose the title of the entry you would like to read:");
                Console.WriteLine("(Note: If there are multiple entries with the same title, each one will be printed.)");

                List();

                Console.WriteLine("");
                string title = Console.ReadLine();
                Search(title);
            }

            void Modify()
            {
                Console.WriteLine("Please choose the title of the entry you would like to modify:");
                Console.WriteLine("(Note: If there are multiple entries with the same title, you can choose which one to modify.)");


                List();
                Console.WriteLine("");
                string title = Console.ReadLine();
                int counter = 0;
                int index = 0;

                var lines = File.ReadAllLines("diary.txt");

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] splitted = lines[i].Split(";");
                    if (splitted[0].Equals(title))
                    {
                        Console.WriteLine("\n\n" + splitted[2] + "\n\n" + splitted[0] + "\n" + splitted[1]);
                        counter++;
                        index = i;
                    }
                }
                if (counter <= 1)
                {
                   
                }


            }

            void Delete()
            {
                Console.WriteLine("Please choose the title of the entry you would like to delete:");
                Console.WriteLine("(Note: If there are multiple entries with the same title, you can choose which one to delete.)");

                List();
                Console.WriteLine("");
                string title = Console.ReadLine();

                Search(title);

                if (Search(title) <= 1)
                {
                    Console.WriteLine("You're good!");
                } else
                {
                    Console.WriteLine("\nWarning! Multiple entries with the same title.");
                    Console.WriteLine("Choose the corresponding ID number you would like to delete.");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\nYou chose: " + id + ".");
                }
            }


            void List()
            {
                StreamReader reader = new StreamReader("diary.txt");
                string line = "";

                while ((line = reader.ReadLine()) != null)
                {
                    string[] splitted = line.Split(";");
                    Console.WriteLine(splitted[0]);
                }
                reader.Close();
            }


            int Search(string title)
            {
                int index = 0;
                int counter = 0;

                var lines = File.ReadAllLines("diary.txt");

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] splitted = lines[i].Split(";");
                    if (splitted[0].Equals(title))
                    {
                        Console.WriteLine("\n\n" + "ID number:" + i + "\n" + splitted[2] + "\n\n" + splitted[0] + "\n" + splitted[1]);
                        counter++;
                    }
                }
                return counter;
            }
        }
    }
}