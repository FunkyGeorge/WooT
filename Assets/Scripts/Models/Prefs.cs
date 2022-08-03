using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Prefs
{
    private const string CURRENT_LEVEL = "CURRENT_LEVEL";
    private const string MASTER_VOLUME = "MASTER_VOLUME";
    private const string DEATH_COUNT = "DEATH_COUNT";
    private const string SAVED_ECHOES = "SAVED_ECHOES";

    private Dictionary<string, EchoSave> echoSaves = new Dictionary<string, EchoSave>();

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

    public static int GetDeathCount()
    {
        return PlayerPrefs.GetInt(DEATH_COUNT, 0);
    }

    public static void SetDeathCount(int newDeathCount)
    {
        PlayerPrefs.SetInt(DEATH_COUNT, newDeathCount);
    }

    // Save echo collectibles
    public static void SaveEcho(int echoID)
    {
        string savedEchosString = PlayerPrefs.GetString(SAVED_ECHOES, "");
        string[] savedEchoIDStrings = savedEchosString.Split('_');

        List<int> savedEchos = Array.ConvertAll(savedEchoIDStrings, int.Parse).ToList<int>();
        if (!savedEchos.Contains(echoID))
        {
            savedEchos.Add(echoID);
        }
        
        string newSavedEchoString = String.Join("_", savedEchos);

        PlayerPrefs.SetString(SAVED_ECHOES, newSavedEchoString);
    }
}
