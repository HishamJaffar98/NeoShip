using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string MAX_FUEL_KEY = "fuel";
    const string MAX_LIVES_KEY = "max_lives";
    const string MAX_GEMS_KEY = "max_gems";
    const string ARMOR_LEVEL_KEY = "armor_level";
    const string FUEL_LEVEL_KEY = "fuel_level";
    const int MIN_LEVEL = 1;
    const int MAX_LEVEL = 5;

    public static float GetMaxFuel()
    {
        return PlayerPrefs.GetFloat(MAX_FUEL_KEY);
    }

    public static void SetMaxFuel(float fuelValue)
    {
        PlayerPrefs.SetFloat(MAX_FUEL_KEY, fuelValue);
    }

    public static int GetMaxLives()
    {
        return PlayerPrefs.GetInt(MAX_LIVES_KEY);
    }

    public static void SetMaxLives(int lives)
    {
        PlayerPrefs.SetInt(MAX_LIVES_KEY, lives);
    }
    public static int GetMaxGems()
    {
        return PlayerPrefs.GetInt(MAX_GEMS_KEY);
    }

    public static void SetMaxGems(int gems)
    {
        PlayerPrefs.SetInt(MAX_GEMS_KEY, gems);
    }

    public static int GetArmorLevel()
    {
        return PlayerPrefs.GetInt(ARMOR_LEVEL_KEY);
    }

    public static void SetArmorLevel(int level)
    {
        if(level>=MIN_LEVEL || level <=MAX_LEVEL)
        {
            PlayerPrefs.SetInt(ARMOR_LEVEL_KEY, level);
        }
        else
        {
            Debug.Log("Cannot upgrade any further");
        }
    }

    public static int GetFuelLevel()
    {
        return PlayerPrefs.GetInt(FUEL_LEVEL_KEY);
    }

    public static void SetFuelLevel(int level)
    {
        if (level >= MIN_LEVEL || level <= MAX_LEVEL)
        {
            PlayerPrefs.SetInt(FUEL_LEVEL_KEY, level);
        }
        else
        {
            Debug.Log("Cannot upgrade any further");
        }
            
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
