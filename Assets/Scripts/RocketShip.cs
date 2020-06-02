using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketShip : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;

    [Header("Movement Speeds")]
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float thrustSpeed = 100f;

    int currentBuildIndex;
    enum State {Alive, Dead};
    State state;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        state = State.Alive;
    }

    // Update is called once per frame
    void Update()
    {
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if(state==State.Alive)
        {
            Rotate();
            Thrust();
        }
    }

    void OnCollisionEnter(Collision otherCollision)
    {
        if(state==State.Dead) {return;}

        switch(otherCollision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                Invoke("LoadNextLevel", 1.5f) ;
                break;
            default:
                state = State.Dead;
                print("Dead");
                Invoke("RestartLevel",2f);
                break;
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(currentBuildIndex);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(currentBuildIndex + 1);
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true;
        float rotSpeed = rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward*rotSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward*rotSpeed);
        }
        rigidBody.freezeRotation = false;
    }
    private void Thrust()
    {
        float thrustFactor = thrustSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustFactor);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            audioSource.Stop();
        }
    }
}
