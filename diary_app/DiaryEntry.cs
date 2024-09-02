namespace DiaryEntryNameSpace
{
    public class DiaryEntry
    {
        public string Content { get; set; } = "Default Title (no title entered by the user)";
        public string Title { get; set; } = "Default Content (no text entered by the user)";

        public DiaryEntry(string input_title, string input_content)
        {
            Title = input_title;
            Content = input_content;
        }
    }
}
