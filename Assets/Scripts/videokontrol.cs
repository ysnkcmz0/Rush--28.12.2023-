using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class videokontrol : MonoBehaviour
{
    
    float a;

    void Start()
    {
        a=0;
    }

    
    void Update()
    {
        a+=1*Time.deltaTime;
        if(a>5)
        {
            SceneManager.LoadScene(1);
        }
    }
}
