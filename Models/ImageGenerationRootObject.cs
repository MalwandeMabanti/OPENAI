namespace OPENAI;
public class ImageGenerationRootobject
{
    public int created { get; set; }
    public Datum[] data { get; set; }
}

public class Datum
{
    public string url { get; set; }
}

