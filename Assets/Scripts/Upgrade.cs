using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [Header("Display items")]
    [SerializeField] TextMeshProUGUI gemsDisplay;
    [SerializeField] Text costOfArmorUpgrade;
    [SerializeField] Text costOfFuelUpgrade;

    [Header("Attribute Sliders")]
    [SerializeField] Slider armorUpgradeSlider;
    [SerializeField] Slider fuelUpgradeSlider;

    [Header("Fireworks Parameters")]
    [SerializeField] GameObject spawnLocation;
    [SerializeField] GameObject fireworks;
    float fireworkDelayToDestroy =6f;

    [Header("Audioclips")]
    [SerializeField] AudioClip[] audioToPlay;

    int armorLevel;
    int fuelLevel;
    const int maxFuelLevel = 5;
    const int maxArmorLevel = 5;
    int armorCost;
    int fuelCost;
    void Start()
    {
        
    
    }

    public void UpgradeArmor()
    {
        int currentGems = PlayerPrefsController.GetMaxGems();
        if (currentGems>=armorCost && armorLevel<maxArmorLevel)
        {
            armorLevel++;
            PlayerPrefsController.SetMaxLives(armorLevel + 1);
            PlayerPrefsController.SetMaxGems(currentGems - armorCost);
            PlayerPrefsController.SetArmorLevel(armorLevel);
            AudioSource.PlayClipAtPoint(audioToPlay[0], Camera.main.transform.position);
            GameObject fireworkVFX = Instantiate<GameObject>(fireworks, spawnLocation.transform.position, Quaternion.identity);
            Destroy(fireworkVFX, fireworkDelayToDestroy);
        }
        else
        {
            AudioSource.PlayClipAtPoint(audioToPlay[1], Camera.main.transform.position);
        }
    }

    public void UpgradeFuel()
    {
        int currentGems = PlayerPrefsController.GetMaxGems();
        if (currentGems >= fuelCost && fuelLevel < maxFuelLevel)
        {
            fuelLevel++;
            PlayerPrefsController.SetMaxFuel(PlayerPrefsController.GetMaxFuel()+500);
            PlayerPrefsController.SetMaxGems(currentGems - fuelCost);
            PlayerPrefsController.SetFuelLevel(fuelLevel);
            AudioSource.PlayClipAtPoint(audioToPlay[0], Camera.main.transform.position);
            GameObject fireworkVFX = Instantiate<GameObject>(fireworks, spawnLocation.transform.position, Quaternion.identity);
            Destroy(fireworkVFX, fireworkDelayToDestroy);
        }
        else
        {
            AudioSource.PlayClipAtPoint(audioToPlay[1], Camera.main.transform.position);
        }
    }
    void Update()
    {
        fuelLevel = PlayerPrefsController.GetFuelLevel();
        armorLevel = PlayerPrefsController.GetArmorLevel();
        gemsDisplay.text = PlayerPrefsController.GetMaxGems().ToString();
        SetFuelCostValue();
        SetArmorCostValue();
    }

    private void SetFuelCostValue()
    {
        switch (fuelLevel)
        {
            case 1:
                fuelCost = 800;
                break;
            case 2:
                fuelCost = 1100;
                break;
            case 3:
                fuelCost = 1500;
                break;
            case 4:
                fuelCost = 2100;
                break;
            case 5:
                fuelCost = 2900;
                break;
        }
        costOfFuelUpgrade.text = fuelCost.ToString();
        fuelUpgradeSlider.value = fuelLevel % (maxFuelLevel + 1);
    }

    private void SetArmorCostValue()
    {
        switch (armorLevel)
        {
            case 1:
                armorCost = 1000;
                break;
            case 2:
                armorCost = 1400;
                break;
            case 3:
                armorCost = 2000;
                break;
            case 4:
                armorCost = 2800;
                break;
            case 5:
                armorCost = 3900;
                break;
        }
        costOfArmorUpgrade.text = armorCost.ToString();
        armorUpgradeSlider.value = armorLevel % (maxFuelLevel + 1);
    }

    public void OnHover()
    {
        AudioSource.PlayClipAtPoint(audioToPlay[2], Camera.main.transform.position);
    }
}
