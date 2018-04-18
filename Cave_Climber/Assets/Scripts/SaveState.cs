using Global;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using UnityEngine;
using System;

public class SaveState
{
    public float SavedScore;
    public void write()
    {
        XmlSerializer ser = new XmlSerializer(typeof(float));
        
        

        TextWriter writer = new StreamWriter(Application.persistentDataPath + "/save.txt");
        ser.Serialize(writer, SavedScore);
        writer.Close();
    }
    public void read()
    {
        try
        {
            TextReader reader = new StreamReader(Application.persistentDataPath + "/save.txt");
            XmlSerializer ser = new XmlSerializer(typeof(float));

            SavedScore = (float)ser.Deserialize(reader);
            reader.Close();
        }
        catch(Exception e)
        {

        }
    }
}
