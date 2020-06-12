using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelController : MonoBehaviour
{
    MusicPlayer musicPlayer;
    [SerializeField] AudioClip firstSong;
    [SerializeField] AudioClip secondSong;
    int flag;
    void Awake()
    {
        flag = 0;
        if(FindObjectsOfType<LevelController>().Length>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
    }

    public void LoadNextLevel(int currentBuildIndex)
    {
        if (currentBuildIndex == SceneManager.sceneCountInBuildSettings - 3 && musicPlayer!=null)
        {
           musicPlayer.GetComponent<Animator>().SetBool("fadeOut",true);
           StartCoroutine(Wait(currentBuildIndex));
        }
        else
        {
            SceneManager.LoadScene((currentBuildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        }
    }

    public void RestartLevel(int currentBuildIndex)
    {
        SceneManager.LoadScene(currentBuildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadUpgradeScreen()
    {
        SceneManager.LoadScene("Upgrade Screen");
    }

    public void LoadStartScreen()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator Wait(int currentBuildIndex)
    {
        yield return new WaitForSeconds(4f);
        flag++;
        if (flag%2!=0)
        {
            FadeInMusic(secondSong, currentBuildIndex);
        }
        else if(flag%2==0)
        {
            FadeInMusic(firstSong, currentBuildIndex);
        }
    }

    void FadeInMusic(AudioClip clip, int currentBuildIndex)
    {
        musicPlayer.GetComponent<AudioSource>().clip = clip;
        musicPlayer.GetComponent<AudioSource>().Play();
        musicPlayer.GetComponent<Animator>().SetBool("fadeOut", false);
        SceneManager.LoadScene((currentBuildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
}
