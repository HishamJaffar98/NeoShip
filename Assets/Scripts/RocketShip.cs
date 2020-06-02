using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;

    [Header("Movement Speeds")]
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float thrustSpeed = 100f;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Thrust();
    }

    void OnCollisionEnter(Collision otherCollision)
    {
        switch(otherCollision.gameObject.tag)
        {
            case "Friendly":
                print("Ok");
                break;
            default:
                print("Die");
                break;
        }
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
