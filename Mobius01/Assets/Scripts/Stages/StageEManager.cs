using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageEManager : MonoBehaviour
{
    public Text startText;

    float startTime = 3;

    bool spawnEnemy = true;
    public float enemyFirstSpawnTime = 2.5f;

    bool spawnJammer = true;
    public float jammerFirstSpawnTime = 25f;

    bool spawnStalker = true;
    public float stalkerFirstSpawnTime = 20f;

    bool spawnAttacker = true;
    public float attackerFirstSpawnTime = 15f;

    bool spawnLaser = true;
    public float laserFirstSpawnTime = 30f;

    bool spawnNuke = true;
    public float nukeFirstSpawnTime = 40f;

    void Start()
    {
        StartCoroutine(StageStart());
    }

    IEnumerator StageStart()
    {
        string[] startText0 =
        {
            ".",
            "..",
            "...",
            "System Online",
            "Weapons Free",
            "Engage",
        };

        float[] startTime0 =
        {
            0.5f,
            0.5f,
            0.5f,
            1f,
            1f,
            1f,
        };

        for (int i = 0; i < startText0.Length; i++)
        {
            startText.text = startText0[i];

            if (i < 3)
            {
                SoundManager.instance.StartSound(i);
            }

            if (i == 3)
            {
                GameManager.instance.SystemOnline();
            }

            if (i == 5)
            {
                GameManager.instance.GunsOn();
            }

            yield return new WaitForSeconds(startTime0[i]);
        }
        startText.gameObject.SetActive(false);
    }

    void Update()
    {
        Restart();
        Stage();
    }

    void Stage()
    {
        if (spawnEnemy && GameManager.instance.time > enemyFirstSpawnTime + startTime)
        {
            spawnEnemy = false;
            SpawnManager.instance.spawnEnemy = true;
            SpawnManager.instance.SpawnEnemy();
        }

        if (spawnJammer && GameManager.instance.time > jammerFirstSpawnTime + startTime)
        {
            spawnJammer = false;
            SpawnManager.instance.spawnJammer = true;
            SpawnManager.instance.SpawnJammer();
        }

        if (spawnStalker && GameManager.instance.time > stalkerFirstSpawnTime + startTime)
        {
            spawnStalker = false;
            SpawnManager.instance.spawnStalker = true;
            SpawnManager.instance.SpawnStalker();
        }

        if (spawnAttacker && GameManager.instance.time > attackerFirstSpawnTime + startTime)
        {
            spawnAttacker = false;
            SpawnManager.instance.spawnAttacker = true;
            SpawnManager.instance.SpawnAttacker();
        }

        if (spawnLaser && GameManager.instance.time > laserFirstSpawnTime + startTime)
        {
            spawnLaser = false;
            SpawnManager.instance.spawnLaser = true;
            SpawnManager.instance.LaserAttack();
        }

        if (spawnNuke && GameManager.instance.time > nukeFirstSpawnTime + startTime)
        {
            spawnNuke = false;
            SpawnManager.instance.spawnNuke = true;
            SpawnManager.instance.NukeAttack();
        }
    }


    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("StageE");
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
