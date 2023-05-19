namespace OPENAI.Models;

public class TextEditingRootobject
{
    public string Input { get; set; }
    public Choice[] choices { get; set; }
}

public class Choice
{
    public string text { get; set; }
}
