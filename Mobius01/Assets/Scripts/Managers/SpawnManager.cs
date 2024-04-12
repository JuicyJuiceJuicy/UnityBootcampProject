using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public float enemySpawnTime = 2.5f;
    public bool spawnEnemy;

    public float jammerSpawnTime = 15f;
    public bool spawnJammer;

    public float stalkerSpawnTime = 15f;
    public bool spawnStalker;

    public float attackerSpawnTime = 15f;
    public bool spawnAttacker;

    public float boss1AttackSpawnTime = 10f;
    public bool spawnBoss1Attack;

    public float laserSpawnTime = 10f;
    public bool spawnLaser;

    public float nukeSpawnTime = 10f;
    public bool spawnNuke;

    public GameObject EnemyLeft;
    public GameObject EnemyRight;
    public GameObject JammerLeft;
    public GameObject JammerRight;
    public GameObject StalkerLeft;
    public GameObject StalkerRight;
    public GameObject AttackerLeft;
    public GameObject AttackerRight;

    public GameObject boss1Attack;
    public GameObject Laser;
    public GameObject Nuke;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SpawnEnemy()
    {
        if (GameManager.instance.alive == true && spawnEnemy)
        {
            int randomS = Random.Range(0, 2);
            float randomY = Random.Range(-1.5f, 1.5f);
            if (randomS == 0)
            {
                Instantiate(EnemyLeft, new Vector3(3, 3 + randomY, 0), Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(EnemyRight, new Vector3(-3, 3 + randomY, 0), Quaternion.Euler(0, 0, 0));
            }
            float spawnTime = enemySpawnTime * GameManager.instance.difficulty;
            Invoke("SpawnEnemy", Random.Range(spawnTime, spawnTime * 2));
        }
    }

    public void SpawnJammer()
    {
        if (GameManager.instance.alive == true && spawnJammer)
        {
            int randomS = Random.Range(0, 2);
            float randomY = Random.Range(-1.5f, 1.5f);
            if (randomS == 0)
            {
                Instantiate(JammerLeft, new Vector3(3, 3 + randomY, 0), Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(JammerRight, new Vector3(-3, 3 + randomY, 0), Quaternion.Euler(0, 0, 0));
            }
            float spawnTime = jammerSpawnTime * GameManager.instance.difficulty;
            Invoke("SpawnJammer", Random.Range(spawnTime, spawnTime * 2));
        }
    }

    public void SpawnStalker()
    {
        if (GameManager.instance.alive == true && spawnStalker)
        {
            int randomS = Random.Range(0, 2);
            float randomY = Random.Range(-1.5f, 1.5f);
            if (randomS == 0)
            {
                Instantiate(StalkerLeft, new Vector3(3, 3 + randomY, 0), Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(StalkerRight, new Vector3(-3, 3 + randomY, 0), Quaternion.Euler(0, 0, 0));
            }
            float spawnTime = stalkerSpawnTime * GameManager.instance.difficulty;
            Invoke("SpawnStalker", Random.Range(spawnTime, spawnTime * 2));
        }
    }

    public void SpawnAttacker()
    {
        if (GameManager.instance.alive == true && spawnAttacker)
        {
            int randomS = Random.Range(0, 2);
            float randomY = Random.Range(-1.5f, 1.5f);
            if (randomS == 0)
            {
                Instantiate(AttackerLeft, new Vector3(3, 3 + randomY, 0), Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(AttackerRight, new Vector3(-3, 3 + randomY, 0), Quaternion.Euler(0, 0, 0));
            }
            float spawnTime = attackerSpawnTime * GameManager.instance.difficulty;
            Invoke("SpawnAttacker", Random.Range(spawnTime, spawnTime * 2));
        }
    }
    public void Boss1Attack()
    {
        if (GameManager.instance.alive == true && spawnBoss1Attack)
        {
            Instantiate(boss1Attack, new Vector3(GameManager.instance.playerX, 0, 0), Quaternion.Euler(0, 0, 0));
            float spawnTime = boss1AttackSpawnTime * GameManager.instance.difficulty;
            Invoke("Boss1Attack", Random.Range(spawnTime, spawnTime * 2));
        }
    }

    public void LaserAttack()
    {
        if (GameManager.instance.alive == true && spawnLaser)
        {
            Instantiate(Laser, new Vector3(GameManager.instance.playerX, 0, 0), Quaternion.Euler(0, 0, 0));
            float spawnTime = laserSpawnTime * GameManager.instance.difficulty;
            Invoke("LaserAttack", Random.Range(spawnTime, spawnTime * 2));
        }
    }
    public void NukeAttack()
    {
        if (GameManager.instance.alive == true && spawnNuke)
        {
            Instantiate(Nuke, GameManager.instance.playerT.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.Euler(0, 0, 0));
            float spawnTime = nukeSpawnTime * GameManager.instance.difficulty;
            Invoke("NukeAttack", Random.Range(spawnTime, spawnTime * 2));
        }
    }
}
