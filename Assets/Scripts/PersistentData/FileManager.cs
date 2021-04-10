using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager
{
    public static bool WriteToFile(string a_FileName, string a_FileContents)
    {
        var fullpath = Path.Combine(Application.persistentDataPath, a_FileName);

        try
        {
            File.WriteAllText(fullpath, a_FileContents);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to {fullpath} with exeption{e}");
        }

        return false;
    }

    public static bool LoadFromFile(string a_filename, out string result)
    {
        var fullpath = Path.Combine(Application.persistentDataPath, a_filename);

        try
        {
            result = File.ReadAllText(fullpath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from {fullpath} with execption {e}");
            result = "";
            return false;
        }
    }
}
