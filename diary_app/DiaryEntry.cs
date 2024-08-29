using System;

namespace DiaryEntryNameSpace
{
    public class DiaryEntry
    {
        public string Content 
        { get; set; }
        public string Title
        { get; set; }

        public DiaryEntry(string input_title, string input_content)
        {
            Title = input_title;
            Content = input_content;
        }
    }
}
