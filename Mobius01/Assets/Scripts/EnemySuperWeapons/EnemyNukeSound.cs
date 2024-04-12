using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNukeSound : MonoBehaviour
{
    public float volume = 0.5f;
    public float pitch = 0.5f;
    
    void Start()
    {
        SoundManager.instance.ExposionBigSound(volume, pitch);
    }
}
