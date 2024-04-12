using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float volume = 0.2f;
    public float pitch = 1.5f;

    public float shake = 0.05f;
    public float expTime = 0.1f;

    void Start()
    {
        StartCoroutine(UIManager.instance.ShakeCam(shake));
        StartCoroutine(Explosive());
        SoundManager.instance.ExposionSound(volume, pitch);
    }

    IEnumerator Explosive()
    {
        yield return new WaitForSeconds(expTime);
        UIManager.instance.ResetCam();
        Destroy(gameObject);
    }
}
