using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMissileLine : MonoBehaviour
{
    private void Update()
    {
        // Jam();
    }

    void Jam()
    {
        if (GameManager.instance.jammed)
        {
            gameObject.SetActive(false);
        }
    }
}
