using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BossAttack : MonoBehaviour
{
    public GameObject Missile;

    // Fire
    public float fireCool = 1.5f;
    public int burst = 1;
    public float burstCool = 0.1f;
    public float randomRange = 0;


    void Start()
    {
        StartCoroutine("Fire");
        UIManager.instance.IncommingOn();
    }

    private void Update()
    {
        UIManager.instance.IncommingOn();
    }

    private IEnumerator Fire()
    {

        yield return new WaitForSeconds(fireCool);
        if (GameManager.instance.alive)
        {
            for (int i = 0; i < burst; i++)
            {
                float randomX = Random.Range(-randomRange, randomRange);
                Instantiate(Missile, transform.position + new Vector3(randomX, 5, 0), Quaternion.identity);
                yield return new WaitForSeconds(burstCool);
            }
        }

        UIManager.instance.IncommingOff();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!SoundManager.instance.warning && SoundManager.instance.warningSoundEnd)
            {
                SoundManager.instance.warning = true;
                StartCoroutine(SoundManager.instance.WarningSound());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIManager.instance.WarningOn();
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1f);

            SoundManager.instance.warning = true;
            if (!SoundManager.instance.warning && SoundManager.instance.warningSoundEnd)
            {
                StartCoroutine(SoundManager.instance.WarningSound());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIManager.instance.WarningOff();
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 50 / 255f);
            SoundManager.instance.warning = false;
        }
    }
}
