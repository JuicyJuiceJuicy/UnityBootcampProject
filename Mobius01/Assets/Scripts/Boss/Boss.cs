using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject Missile;
    public GameObject Explosion;
    public GameObject LeadCircle;
    Animator animator;

    // Move, Return
    public int direction = 1;
    public float speed = 2.5f;

    // Fire
    public float fireCool = 1.5f;
    public int burst = 1;
    public float burstCool = 0.1f;
    public float randomRange = 0;

    // Hit
    public int hitPoint = 1;
    int score = 1;
    public bool stageBoss;
    public int stage;
    public bool killed = false;

    public int phase2HP = 30;
    public int phase3HP = 10;


    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine("Fire");
    }

    void Update()
    {
        Move();
        Return();
    }

    private void Move()
    {
        if (transform.position.y > 4)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }    

        if (transform.position.x < GameManager.instance.playerX)
        {
            direction = 1;

            if (GameManager.instance.playerX - transform.position.x > 0.5f)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                LeadCircle.GetComponent<LeadCircle>().direction = 1;
                LeadCircle.GetComponent<LeadCircle>().LeadOn();
            }
            else if (GameManager.instance.playerX - transform.position.x > 0.1f)
            {
                LeadCircle.GetComponent<LeadCircle>().LeadOff();
            }
        }
        else
        {
            direction = -1;

            if (transform.position.x - GameManager.instance.playerX > 0.5f)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                LeadCircle.GetComponent<LeadCircle>().direction = -1;
                LeadCircle.GetComponent<LeadCircle>().LeadOn();
            }
            else if (transform.position.x - GameManager.instance.playerX > 0.1f)
            {
                LeadCircle.GetComponent<LeadCircle>().LeadOff();
            }
        }

        transform.Translate(direction * speed * Time.deltaTime, 0, 0);
    }


    private IEnumerator Fire()
    {
        
        yield return new WaitForSeconds(Random.Range(0, fireCool));
        while (GameManager.instance.alive)
        {
            for (int i = 0; i < burst; i++)
            {
                float randomX = Random.Range(-randomRange, randomRange);
                Instantiate(Missile, transform.position + new Vector3(randomX, 0, 0), Quaternion.identity);
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
            switch (stage)
            {
                case 1:
                    {
                        Stage1Manager.instance.boss1Killed = true;
                    }
                    break;
                default:
                    break;
            }
        }
        else if (hitPoint < phase3HP)
        {
            animator.SetInteger("phase", 3);
        }
        else if (hitPoint < phase2HP)
        {
            animator.SetInteger("phase", 2);
        }
    }
}
