using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RocketShip : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    LevelController levelController;

    [Header("Movement Speeds")]
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float thrustSpeed = 100f;

    [Header("Audio Clips")]
    [SerializeField] AudioClip[] audioClipsToPlay;

    [Header("Particle Systems")]
    [SerializeField] ParticleSystem[] particleSystems;

    [Header("Delay times")]
    [SerializeField] float levelLoadDelay = 1.5f;
    [SerializeField] float restartLevelDelay = 2f;

    [Header("Fuel Resources")]
    public float fuelLevel;
    public float maxFuel;

    [Header("Gem Resources")]
    public int currentGems;
    [SerializeField] GameObject gemsCounter;

    [Header("Armour Resources")]
    public int lives;
    [SerializeField] GameObject livesCounter;

    Animator rocketAnimator;
    ParticleSystem smoke;
    bool collisionEnabled = true;
    enum State {Alive, Dead, Recuperating};
    State state;

    void Awake()
    {
        maxFuel = PlayerPrefsController.GetMaxFuel();
        fuelLevel = PlayerPrefsController.GetMaxFuel();
        lives = PlayerPrefsController.GetMaxLives();
    }
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        rocketAnimator = GetComponent<Animator>();
        currentGems = PlayerPrefsController.GetMaxGems();
        state = State.Alive;
    }

    // Update is called once per frame
    void Update()
    {
        levelController = FindObjectOfType<LevelController>();
        if (smoke != null)
        {
            smoke.transform.position = gameObject.transform.position;
            smoke.transform.rotation = transform.rotation;
        }
        if(state!=State.Dead && fuelLevel > 0 && Time.timeScale!=0)
        {
            Rotate();
            Thrust();
        }
        if(Debug.isDebugBuild)
        {
            ActivateDebug();
        }
        livesCounter.GetComponent<TextMeshProUGUI>().text = "Armor = " + lives.ToString();
        gemsCounter.GetComponent<TextMeshProUGUI>().text = "Gems = " + currentGems.ToString();
        
    }

    void ActivateDebug()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            levelController.LoadNextLevel(SceneManager.GetActiveScene().buildIndex);
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionEnabled = !collisionEnabled;
        }
    }

    void OnCollisionEnter(Collision otherCollision)
    {
        if(state==State.Dead || !collisionEnabled) {return;}
        switch(otherCollision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                collisionEnabled = false;
                audioSource.PlayOneShot(audioClipsToPlay[1]);
                particleSystems[1].Play();
                PlayerPrefsController.SetMaxGems(currentGems);
                Invoke("CallLoadNextLevel", levelLoadDelay) ;
                break;      
            default:
                if(state==State.Recuperating)
                {
                    break;
                }
                lives--;
                state = State.Recuperating;
                if(lives==0)
                {
                    state = State.Dead;
                    AudioSource.PlayClipAtPoint(audioClipsToPlay[2],Camera.main.transform.position);
                    particleSystems[2].Play();
                    Invoke("CallRestartLevel", restartLevelDelay);
                    break;
                }
                audioSource.PlayOneShot(audioClipsToPlay[3]);
                smoke = Instantiate<ParticleSystem>(particleSystems[3], transform.position, Quaternion.identity);
                StartCoroutine(Recuperate(smoke));
                break;
        }
    }

    IEnumerator Recuperate(ParticleSystem particles)
    {
        rocketAnimator.SetBool("isRecuperating",true);
        yield return new WaitForSecondsRealtime(5f);
        rocketAnimator.SetBool("isRecuperating",false);
        Destroy(particles.gameObject);
        state = State.Alive;
    }

    private void CallRestartLevel()
    {
        levelController.RestartLevel(SceneManager.GetActiveScene().buildIndex);
    }

    private void CallLoadNextLevel()
    {
        if(SceneManager.GetActiveScene()  == SceneManager.GetSceneByName("Game Over Screen") )
        {;
            levelController.LoadStartScreen();
        }
        else
        {
            levelController.LoadNextLevel(SceneManager.GetActiveScene().buildIndex);
        }
        
    }

    private void Rotate()
    {
        rigidBody.angularVelocity = Vector3.zero;
        float rotSpeed = rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward*rotSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward*rotSpeed);
        }
    }
    private void Thrust()
    {
        float thrustFactor = thrustSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustFactor);
            fuelLevel--;
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClipsToPlay[0]);
                particleSystems[0].Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            audioSource.Stop();
            particleSystems[0].Stop();
        }
    }
}
