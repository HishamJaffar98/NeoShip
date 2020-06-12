using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionReset : MonoBehaviour
{
    void Start()
    {
        Vector3 position = transform.position;
        position.z = FindObjectOfType<RocketShip>().gameObject.transform.position.z;
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
