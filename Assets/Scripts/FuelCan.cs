using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCan : MonoBehaviour
{
    [SerializeField] ParticleSystem fuelPickupParticles;
    [SerializeField] AudioClip pickupSound;
    bool collected = false;


    RocketShip player;
    void Start()
    {
        player = FindObjectOfType<RocketShip>();
    }

    // Update is called once per frame
    void Update()
    {
        collected = false;
    }

    void OnTriggerEnter(Collider otherCollision)
    {
        if (otherCollision.GetComponentInParent<RocketShip>())
        {
            float fuelGain = 200f;
            if (player.maxFuel - player.fuelLevel < fuelGain)
            {
                player.fuelLevel = player.maxFuel;
            }
            else
            {
                player.fuelLevel += fuelGain;
            }
            Instantiate<ParticleSystem>(fuelPickupParticles,gameObject.transform.position, Quaternion.identity);
            gameObject.GetComponent<AudioSource>().PlayOneShot(pickupSound);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, 2f);
            collected = true;
        }
    }
}
