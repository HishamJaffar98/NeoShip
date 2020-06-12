using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewOscillator : MonoBehaviour
{
    public Vector3 movementVector;
    float movementFactor;
    public float period = 3f;
    Vector3 startingPos;
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        CreateAutomatedOscillation();
    }

    private void CreateAutomatedOscillation()
    {
        float cycle = Time.time / period;
        const float tau = Mathf.PI * 2;

        float rawSin = Mathf.Sin(tau * cycle); // Used to get a smooth oscillation between -1 and 1 
        movementFactor = (rawSin / 2f) + 0.5f; //Makes the oscillation between 0 and 1
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
