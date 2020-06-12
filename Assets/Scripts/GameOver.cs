using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    List<GameObject> children = new List<GameObject>();
    List<GameObject> subChildren = new List<GameObject>();
    void Start()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            children.Add(transform.GetChild(i).gameObject);
        }

        for(int i=0;i<children.Count;i++)
        {
            for(int j=0;j<children[i].transform.childCount;j++)
            {
                subChildren.Add(children[i].transform.GetChild(j).gameObject);
            }
        }

        for(int i=0;i<subChildren.Count;i++)
        {
            subChildren[i].AddComponent<NewOscillator>();
            if (i % 2 != 0)
            {
                Vector3 position = new Vector3(0, 0, -10.1f);
                subChildren[i].transform.position += position;
                subChildren[i].GetComponent<NewOscillator>().movementVector = new Vector3(0, 0, 10.1f);
            }
            else
            {
                subChildren[i].GetComponent<NewOscillator>().movementVector = new Vector3(0f, 0f, -10.1f);
            }
        }
    }

    void Update()
    {
        
    }
}
