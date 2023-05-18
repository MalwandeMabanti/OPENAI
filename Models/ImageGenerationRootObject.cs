namespace OPENAI;
public class ImageGenerationRootobject
{
    public string Input { get; set; }
    public Datum[] data { get; set; }
}

public class Datum
{
    public string url { get; set; }
}

