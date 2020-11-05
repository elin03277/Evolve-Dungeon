using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string SAVE_FOLDER = Application.dataPath + "/SaveFolder";

    public static void Initialize()
    {
        if (!Directory.Exists(SAVE_FOLDER)) {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string saveString)
    {
        int saveNum = 1;

        while (File.Exists(SAVE_FOLDER + "/save_" + saveNum + ".txt"))
        {
            saveNum++;
        }

        File.WriteAllText(SAVE_FOLDER + "/save_" + saveNum + ".txt", saveString);
    }

    public static string Load()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.txt");
        FileInfo mostRecentFile = null;

        foreach (FileInfo fileInfo in saveFiles)
        {
            if (mostRecentFile == null)
            {
                mostRecentFile = fileInfo;
            } else if (fileInfo.LastWriteTime > mostRecentFile.LastWriteTime) {
                 //Debug.Log("Recent file: " + fileInfo.FullName + "Old file: " + mostRecentFile.Name);
                mostRecentFile = fileInfo;
            }
        }

        if (mostRecentFile != null)
        {
            string save = File.ReadAllText(mostRecentFile.FullName);
            return save;
        } else {
            return null;
        }
    }
}
