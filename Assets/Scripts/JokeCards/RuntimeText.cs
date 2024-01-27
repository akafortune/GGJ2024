using UnityEngine;
using System.IO;
public class RuntimeText : MonoBehaviour
{
    /*
    public static void WriteString()
    {
        string path = Application.persistentDataPath + "/test.txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Test");
        writer.Close();
        StreamReader reader = new StreamReader(path);
        //Print the text from the file
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }
    */

    public static string ReadString()
    {
        string path = "Assets/Scripts/JokeCards/PlayerDeck.txt";
        //Read the text from directly from the Filename file
        StreamReader reader = new StreamReader(path);
        string text = reader.ReadToEnd();
        Debug.Log(text);
        reader.Close();
        return text;
    }
}
