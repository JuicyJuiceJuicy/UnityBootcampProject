using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNukeMissile : MonoBehaviour
{
    void Start()
    {
        Invoke("Fade", 1f);
    }

    void Update()
    {
        transform.Translate(0, -10 * Time.deltaTime, 0);
    }

    void Fade()
    {
        Destroy(gameObject);
    }
}
