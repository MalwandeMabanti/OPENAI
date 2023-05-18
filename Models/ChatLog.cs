namespace OPENAI.Models
{
    public class ChatLog
    {
        public int Id { get; set; } // Primary key
        public string Input { get; set; }
        public string Content { get; set; }
    }
}
