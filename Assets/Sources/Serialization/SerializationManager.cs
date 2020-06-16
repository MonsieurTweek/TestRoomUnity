using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializationManager
{
    public static readonly string SAVE_DIRECTORY = Application.persistentDataPath + "/saves/";
    public static readonly string SAVE_EXTENSION = ".save";

    public static bool Save(string saveName, object saveData)
    {
        BinaryFormatter formatter = GetBinaryFormatter();

        if (Directory.Exists(SAVE_DIRECTORY) == false)
        {
            Directory.CreateDirectory(SAVE_DIRECTORY);
        }

        string path = SAVE_DIRECTORY + saveName + SAVE_EXTENSION;
        FileStream file = File.Create(path);

        formatter.Serialize(file, saveData);

        file.Close();

        return true;
    }

    public static object Load(string saveName)
    {
        string path = SAVE_DIRECTORY + saveName + SAVE_EXTENSION;

        if (File.Exists(path) == false)
        {
            return null;
        }

        BinaryFormatter formatter = GetBinaryFormatter();

        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);

            file.Close();

            return save;
        }
        catch
        {
            Debug.LogErrorFormat("Failed to load file at {0}", path);

            file.Close();

            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        return formatter;
    }
}
