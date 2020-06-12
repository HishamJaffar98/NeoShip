using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingAnim : MonoBehaviour
{
    string loadingText;
    void Start()
    {
        loadingText = GetComponent<TextMeshProUGUI>().text;
        StartCoroutine(updateText());
    }

    void Update()
    {
        
    }

    IEnumerator updateText()
    {
        while(gameObject.activeSelf)
        {
            GetComponent<TextMeshProUGUI>().text = "";
            foreach (char letter in loadingText)
            {
                GetComponent<TextMeshProUGUI>().text += letter;
                yield return new WaitForSecondsRealtime(0.2f);
            }
        } 
    }
}
