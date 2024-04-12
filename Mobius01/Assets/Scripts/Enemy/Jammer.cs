using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jammer : MonoBehaviour
{

    void Start()
    {
        GameManager.instance.JamOn();
    }

    void Update()
    {
        GameManager.instance.JamOn();
    }
}
