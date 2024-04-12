using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject EnemyMissile;
    public GameObject Explosion;

    // Move, Return
    public int direction = 1;
    public float speed = 2.5f;

    // Fire
    public float fireCool = 1.5f;
    public int burst = 1;
    public float burstCool = 0.1f;

    // Hit
    public int hitPoint = 1;
    int score = 1;

    void Start()
    {
        StartCoroutine("Fire");
    }

    void Update()
    {
        Move();
        Return();
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime, 0, 0);
    }


    private IEnumerator Fire()
    {
        
        yield return new WaitForSeconds(Random.Range(0, fireCool));
        while (GameManager.instance.alive)
        {
            for (int i = 0; i < burst; i++)
            {
                SoundManager.instance.EnemyFireSound();
                Instantiate(EnemyMissile, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(burstCool);
            }
            yield return new WaitForSeconds(fireCool);
        }
    }

    private void Return()
    {
        if (GameManager.instance.alive)
        {
            if (transform.position.x * direction > 3f)
                transform.position += new Vector3(-7 * direction, 0, 0);
        }
    }

    void OnBecameInvisible()
    {
        if (!GameManager.instance.alive)
        {
            Destroy(gameObject);
        }
    }


    public void Hit(int damage)
    {
        hitPoint -= damage;
        if (hitPoint <= 0)
        {
            Destroy(gameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            GameManager.instance.AddScore(score);
            GameManager.instance.JamOff();
        }
    }
}
