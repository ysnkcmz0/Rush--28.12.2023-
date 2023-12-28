using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skor : MonoBehaviour
{
    public static int skor;
    public Text skortext; 
    void Start()
    {
        skor = 0;
    }

    
    void Update()
    {
        skortext.text = skor.ToString();
    }
}
