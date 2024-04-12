using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserSound : MonoBehaviour
{
    public float volume = 0.5f;
    public float pitch = 0.5f;

    void Start()
    {
        SoundManager.instance.LaserSound(volume, pitch);
    }
}