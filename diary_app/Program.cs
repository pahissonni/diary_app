using DiaryEntryNameSpace;


namespace DiaryApp
{
    class Program
    {

        static void Main()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            StreamWriter writer = File.AppendText("diary.txt");
            writer.Close();
            Console.WriteLine("\nHello! What do you want to do?");
            Console.WriteLine("Please select the corresponding letter and press Enter to continue.\n");
            Console.WriteLine("'a' to Add an entry to the diary\n'r' to Read entries\n" +
                              "'m' to Modify entries\n'd' to Delete entries\n\n'q' to Quit the application\n");


            while (true)
            {


                string? answer = Console.ReadLine();
                string? aLow = answer?.ToLower();

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

                    case "l":
                        LightsOut();
                        break;

                    default:
                        Console.WriteLine("\nInput is not valid, please try again.");
                        break;
                }
                Console.WriteLine("\nWaiting for input (a, r, m, d or q):");
            }

            void Add()
            {
                StreamWriter writer = File.AppendText("diary.txt");
                Console.WriteLine("Please choose a title for your entry:");
                string? title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title) == true)
                {
                    title = "Default";
                }
                Console.WriteLine("\nEnter text your diary entry:");
                string? content = Console.ReadLine();

                DiaryEntry entry = new DiaryEntry(title, content);

                writer.WriteLine(entry.Title + ";" + entry.Content + ";" + DateTime.Now);
                writer.Close();
                Console.WriteLine("\nEntry added!\n");
            }

            void Read()
            {
                if (List() == true)
                {
                    Console.WriteLine("Please choose the title of the entry you would like to read:");
                    Console.WriteLine("(Note: If there are multiple entries with the same title, each title will be printed.)");
                    var lines = File.ReadAllLines("diary.txt");
                    Console.WriteLine("");
                    string? title = Console.ReadLine();
                    int id = Search(title);
                    if (id >= 0)
                    {
                        Console.WriteLine("\n\n" + lines[id].Split(";")[2] + "\n\n" + lines[id].Split(";")[0] + "\n" + lines[id].Split(";")[1] + "\n");
                    }
                }
            }

            void Modify()
            {
                if (List() == true)
                {
                    Console.WriteLine("Please choose the title of the entry you would like to modify:");
                    Console.WriteLine("(Note: If there are multiple entries with the same title, you can choose which one to modify.)");

                    Console.WriteLine("");
                    string? title = Console.ReadLine();
                    int id = Search(title);


                    Console.WriteLine("\nWarning: Modifying will overwrite existing title or content!");

                    if (Confirm() == true)
                    {
                        ModifyEntry(id);
                    }
                }
            }

            void ModifyEntry(int id)
            {

                var lines = File.ReadAllLines("diary.txt");
                var tempDiary = new StreamWriter("tempDiary.txt");
                while (true)
                {
                    Console.WriteLine("What would you like to modify 't' for the title and 'c' for the content?");
                    string? tOrC = Console.ReadLine();

                    if (tOrC == "t")
                    {
                        Console.WriteLine("Please choose a new title: ");
                        string? newTitle = Console.ReadLine();
                        lines[id] = newTitle + ";" + lines[id].Split(";")[1] + ";" + lines[id].Split(";")[2];
                        break;
                    }

                    if (tOrC == "c")
                    {
                        Console.WriteLine("Please write new content: ");
                        string? newContent = Console.ReadLine();
                        lines[id] = lines[id].Split(";")[0] + ";" + newContent + ";" + lines[id].Split(";")[2];
                        break;
                    }
                    Console.WriteLine("Not a valid input, please try again.");
                }

                for (int i = 0; i < lines.Length; i++)
                {
                    tempDiary.WriteLine(lines[i]);
                }
                tempDiary.Close();
                File.Delete("diary.txt");
                File.Move("tempDiary.txt", "diary.txt");
                Console.WriteLine("\nModified!");

            }

            void Delete()
            {

                if (List() == true)
                {
                    Console.WriteLine("Please choose the title of the entry you would like to delete:");
                    Console.WriteLine("(Note: If there are multiple entries with the same title, you can choose which one to delete.)");
                    Console.WriteLine("");
                    string? title = Console.ReadLine();

                    int id = Search(title);

                    if (Confirm() == true)
                    {
                        var lines = File.ReadAllLines("diary.txt");
                        var tempDiary = new StreamWriter("tempDiary.txt");

                        for (int i = 0; i < lines.Length; i++)
                        {
                            if (i != id)
                            {
                                tempDiary.WriteLine(lines[i]);
                            }
                            if (id == -1 && lines[i].Split(";")[0] != title)
                            {
                                tempDiary.WriteLine(lines[i]);
                            }
                        }
                        tempDiary.Close();
                        File.Delete("diary.txt");
                        File.Move("tempDiary.txt", "diary.txt");
                        Console.WriteLine("\nDeleted!");
                    }
                }
            }


            bool List()
            {
                StreamReader reader = new StreamReader("diary.txt");
                string? line = "";
                int counter = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] splitted = line.Split(";");
                    Console.WriteLine(splitted[0]);
                    counter++;
                }
                if (counter > 0)
                {
                    reader.Close();
                    return true;
                }
                Console.WriteLine("No entries found!");
                reader.Close();
                return false;
            }



            int Search(string title)
            {
                int id = -1;
                int counter = 0;
                var print = "";

                var lines = File.ReadAllLines("diary.txt");

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] splitted = lines[i].Split(";");
                    if (splitted[0].Equals(title))
                    {
                        print += "\n\n" + "ID number:" + i + "\n" + splitted[2] + "\n\n" + splitted[0] + "\n";
                        counter++;
                        id = i;
                    }
                }
                if (counter > 1)
                {
                    Console.Write(print);
                    Console.WriteLine("\nWarning! Multiple entries with the same title.");
                    Console.WriteLine("Choose the corresponding ID number you would like to interact with.");
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out int chosenId) && chosenId >= 0 && chosenId < lines.Length && title == lines[chosenId].Split(";")[0])
                        {
                            Console.WriteLine("\nYou've chosen: " + chosenId + ".");
                            id = chosenId;
                            break;
                        }
                        Console.WriteLine("\nID and title doesn't match!");
                        continue;
                    }
                }
                if (counter == 0)
                {
                    Console.WriteLine("\nEntry not found");
                }
                return id;
            }

            bool Confirm()
            {
                Console.WriteLine("\nAre you sure?");
                while (true)
                {
                    Console.WriteLine("'y' for YES and 'n' for NO");
                    string? yesOrNo = Console.ReadLine();

                    if (yesOrNo.ToLower() == "y")
                    {
                        return true;
                    }
                    else if (yesOrNo.ToLower() == "n")
                    {
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Input is not valid, please try again.");
                    }
                }
                return false;
            }



            void LightsOut()
            {

                Console.WriteLine("Choose 'e' to enable, 'd' to disable dark mode.");
                while (true)
                {
                    string input = Console.ReadLine();
                    if (input == "e")
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Clear();
                        break;
                    }
                    else if (input == "d")
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                }
            }


        }
    }
}