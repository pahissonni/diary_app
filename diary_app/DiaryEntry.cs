using System;

namespace DiaryEntryNameSpace
{
    public class DiaryEntry
    {
        public string Text 
        { get; set; }
        public string Title
        { get; set; }

        public DiaryEntry(string input_title, string input_text)
        {
            Title = input_title;
            Text = input_text;
        }
    }
}
