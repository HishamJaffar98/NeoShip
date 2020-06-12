using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : MonoBehaviour
{
    RocketShip player;
    bool collected = false;
    [SerializeField] AudioClip gemPickupSound;
    Vector3 initialCameraPosition;
    void Start()
    {
        initialCameraPosition = Camera.main.transform.position;
        player = FindObjectOfType<RocketShip>();
    }

    // Update is called once per frame
    void Update()
    {
        collected = false;
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if(otherCollider.GetComponentInParent<RocketShip>() && collected == false)
        {
            switch(gameObject.tag)
            {
                case "Amber":
                    player.currentGems += 100;
                    break;
                case "Saphire":
                    player.currentGems += 125;
                    break;
                case "Emerald":
                    player.currentGems += 150;
                    break;
                case "Ruby":
                    player.currentGems += 200;
                    break;
            }
            gameObject.GetComponent<AudioSource>().PlayOneShot(gemPickupSound);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, 2f);
            collected = true;
        }
    }
}
