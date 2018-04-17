using Global;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

public class SaveState
{
    public float SavedScore;
    public void write()
    {
        XmlSerializer ser = new XmlSerializer(typeof(float));
        
        TextWriter writer = new StreamWriter("save.txt");
        ser.Serialize(writer, SavedScore);
        writer.Close();
    }
    public void read()
    {
        TextReader reader = new StreamReader("save.txt");
        XmlSerializer ser = new XmlSerializer(typeof(float));

        SavedScore = (float)ser.Deserialize(reader);
        reader.Close();
    }
}
