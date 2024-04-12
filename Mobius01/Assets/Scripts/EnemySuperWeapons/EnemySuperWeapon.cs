using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuperWeapon : MonoBehaviour
{
    public GameObject LaunchWeapon;
    public float xRange = 2.5f;
    public float yRange = 4.5f;

    void Start()
    {
        Fence();
        UIManager.instance.IncommingOn();
        SoundManager.instance.IncommingSound();
        Invoke("Launch", 1);
    }

    void Update()
    {
        UIManager.instance.IncommingOn();
    }

    void Fence()
    {
        if (transform.position.x <= -xRange)
            transform.position = new Vector3(-xRange, transform.position.y, 0);
        if (transform.position.x >= xRange)
            transform.position = new Vector3(xRange, transform.position.y, 0);
        if (transform.position.y <= -yRange)
            transform.position = new Vector3(transform.position.x, -yRange, 0);
        if (transform.position.y >= 0f)
            transform.position = new Vector3(transform.position.x, 0f, 0);
    }

    void Launch()
    {
        Destroy(gameObject);
        Instantiate(LaunchWeapon, transform.position, Quaternion.identity);
        UIManager.instance.IncommingOff();
    }
}
