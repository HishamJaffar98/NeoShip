using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] AudioClip[] pauseButtonSounds;
    MusicPlayer musicPlayer;
    LevelController levelController;
    AudioSource panelAudioSource;
    void Start()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
        levelController = FindObjectOfType<LevelController>();
        panelAudioSource = pausePanel.GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pausePanel.activeSelf)
        {
            musicPlayer.GetComponent<Animator>().cullingMode = AnimatorCullingMode.CullUpdateTransforms; 
            PauseActivity(true, 0, 0.5f);
            
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pausePanel.activeSelf)
        {
            musicPlayer.GetComponent<Animator>().cullingMode = AnimatorCullingMode.AlwaysAnimate;
            PauseActivity(false, 1, 1f);
        }
    }

    void PauseActivity(bool panelActive, float timeScaleValue, float volumeLevel)
    {
        pausePanel.SetActive(panelActive);
        Time.timeScale = timeScaleValue;
        musicPlayer.gameObject.GetComponent<AudioSource>().volume = volumeLevel;
    }

    public void Resume()
    {
        musicPlayer.GetComponent<Animator>().cullingMode = AnimatorCullingMode.AlwaysAnimate;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        musicPlayer.gameObject.GetComponent<AudioSource>().volume = 1f;
        
    }

    public void Restart()
    {
        musicPlayer.GetComponent<Animator>().cullingMode = AnimatorCullingMode.AlwaysAnimate;
        Time.timeScale = 1f;
        musicPlayer.gameObject.GetComponent<AudioSource>().volume = 1f;
        levelController.RestartLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        musicPlayer.GetComponent<Animator>().cullingMode = AnimatorCullingMode.AlwaysAnimate;
        Time.timeScale = 1f;
        musicPlayer.gameObject.GetComponent<AudioSource>().volume = 1f;
        levelController.LoadStartScreen();
    }

    public void OnHover()
    {
        panelAudioSource.PlayOneShot(pauseButtonSounds[0]);
    }

    public void OnClick()
    {
        panelAudioSource.PlayOneShot(pauseButtonSounds[1]);
    }
}
