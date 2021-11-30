using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefs
{
    private const string CURRENT_LEVEL = "CURRENT_LEVEL";
    private const string MASTER_VOLUME = "MASTER_VOLUME";

    public static void SetCurrentLevel(string nextLevel)
    {
        PlayerPrefs.SetString(CURRENT_LEVEL, nextLevel);
    }

    public static bool HasCurrentLevel()
    {
        string currentLevel = PlayerPrefs.GetString(CURRENT_LEVEL);
        if (currentLevel != "")
        {
            return true;
        }
        return false;
    }

    public static string GetCurrentLevel()
    {
        string currentLevel = PlayerPrefs.GetString(CURRENT_LEVEL);
        return currentLevel;
    }

    public static int GetMasterVolume()
    {
        return PlayerPrefs.GetInt(MASTER_VOLUME, 50);
    }

    public static void SetMasterVolume(int newVolume)
    {
        PlayerPrefs.SetInt(MASTER_VOLUME, newVolume);
    }
}
