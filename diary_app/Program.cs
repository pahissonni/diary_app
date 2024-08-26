using System;
using System.Security.Cryptography.X509Certificates;
using DiaryEntryNameSpace;
using System.IO;



/*            {
    File.Create("diary.txt");
}

StreamWriter writer = new StreamWriter("diary.txt");*/



while (true)
{
    Console.WriteLine("\nHello! What do you want to do?");
    Console.WriteLine("Please select the corresponding letter and press Enter to continue.\n");
    Console.WriteLine("'a' to Add an entry to the diary\n'r' to Read entries\n" +
                      "'m' to Modify entries\n'd' to Delete entries\n\n'q' to Quit the application\n");

    string answer = Console.ReadLine();
    string aLow = answer.ToLower();

    switch (aLow)
    {
        case "a":
            Add();
            break;

        case "r":
            Read();
            break;

        case "m":
            Console.WriteLine("Selected M");
            break;

        case "d":
            Console.WriteLine("Selected D");
            break;

        case "q":
            Environment.Exit(0);
            break;

        default:
            Console.WriteLine("\nInput is not valid, please try again.");
            break;
    }
}

void Add()
{
    Console.WriteLine("Please choose a title for your enty:");
    string title = Console.ReadLine();
    Console.WriteLine("\nEnter text your diary entry:");
    string text = Console.ReadLine();

    DiaryEntry entry = new DiaryEntry(title, text);
    {
        entry.Title = title;
        entry.Text = text;
    };

    Console.WriteLine("\nEntry added!");
}

void Read()
{
    Console.WriteLine("Please choose the title of the entry you would like to read:");
    //Listaa kaikki merkinnät.
    string title = Console.ReadLine();
    //tulosta titlen perusteella (lue tiedosto, tiedosto[0] on avain
}
