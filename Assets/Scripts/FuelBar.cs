using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    RocketShip player;
    Slider fuelSlider;
    void Start()
    {
        player = FindObjectOfType<RocketShip>();
        fuelSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        fuelSlider.value = player.fuelLevel / player.maxFuel;
    }
}
