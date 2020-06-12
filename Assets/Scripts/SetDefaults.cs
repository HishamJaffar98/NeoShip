using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetDefaults : MonoBehaviour
{
    [SerializeField] float loadTime = 5f;
    void Start()
    {
        PlayerPrefsController.SetMaxFuel(1000f);
        PlayerPrefsController.SetMaxLives(2);
        PlayerPrefsController.SetMaxGems(0);
        PlayerPrefsController.SetArmorLevel(1);
        PlayerPrefsController.SetFuelLevel(1);
        StartCoroutine(LoadStartMenu());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadStartMenu()
    {
        yield return new WaitForSecondsRealtime(loadTime);
        FindObjectOfType<LevelController>().LoadNextLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
