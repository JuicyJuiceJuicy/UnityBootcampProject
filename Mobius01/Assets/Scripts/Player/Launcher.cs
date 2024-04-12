using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject PlayerMissile;

    public float fireRate = 0.2f;
    public float fireRate0 = 0.2f;
    public float burstRate = 2f;

    float gunsHeat = 0;
    public float heatRate = 20;
    public float coolRate = 110;



    private void Update()
    {
        HeatRange();

        if (GameManager.instance.gunsOn)
        {
            Burst();

            if (GameManager.instance.gunsOff)
            {
                Fire();
                GameManager.instance.gunsOff = false;
            }
        }
    }

    void Fire()
    {
        if (GameManager.instance.gunsOn)
        {
            float randomX = Random.Range(-0.2f, 0.2f);
            Vector3 launchPoint = transform.position + new Vector3(randomX, 0, 0);
            Instantiate(PlayerMissile, launchPoint, Quaternion.identity);

            gunsHeat += heatRate;
            SoundManager.instance.FireSound(gunsHeat);

            UIManager.instance.GunsOn();
            UIManager.instance.GunsHeat(gunsHeat, heatRate);

            Invoke("Fire", fireRate0);
        }
    }

    void HeatRange()
    {
        if (gunsHeat <= -heatRate)
        {
            gunsHeat = -heatRate;
        }
        else if (gunsHeat > 100)
        {
            gunsHeat = 100;
        }
        else
        {
            gunsHeat -= coolRate * Time.deltaTime;
        }
    }
    void Burst()
    {
        if(Input.GetKeyDown(KeyCode.Space) && gunsHeat < 100 - heatRate)
        {
            UIManager.instance.BurstOn();
            fireRate0 /= burstRate;
        }
        if(Input.GetKeyUp(KeyCode.Space) || gunsHeat > 100 - heatRate)
        {
            UIManager.instance.BurstOff();
            // SoundManager.instance.OverheatSound();
            fireRate0 = fireRate;
        }
    }
}
