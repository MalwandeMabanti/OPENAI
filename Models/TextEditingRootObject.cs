namespace OPENAI.Models;

public class TextEditingRootobject
{
    public string _object { get; set; }
    public int created { get; set; }
    public Choice[] choices { get; set; }
    public Usage usage { get; set; }
}

public class Usage
{
    public int prompt_tokens { get; set; }
    public int completion_tokens { get; set; }
    public int total_tokens { get; set; }
}

public class Choice
{
    public string text { get; set; }
    public int index { get; set; }
}
