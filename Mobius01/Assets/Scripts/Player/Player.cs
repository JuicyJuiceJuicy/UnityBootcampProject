using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int hitPoint = 1;
    [SerializeField] float speed = 5f;
    [SerializeField] float speed0 = 5f;
    [SerializeField] GameObject Explosion;

    [SerializeField] float fenceX = 2.65f;
    [SerializeField] float fenceY = -1f;

    void Start()
    {
        GameManager.instance.alive = true;
    }

    void Update()
    {
        Fence();

        if (GameManager.instance.systemOnline)
        {
            Move();
            AirBreak();
            UIManager.instance.DashBoard();
        }
    }

    void Move()
    {
        Vector3 movedir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
        transform.Translate(movedir * Time.deltaTime * speed0);
    }

    void AirBreak()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed0 *= 0.5f;
            UIManager.instance.AirBreakOn();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed0 = speed;
            UIManager.instance.AirBreakOff();
        }
    }

    void Fence()
    {
        if (transform.position.x <= -fenceX)
            transform.position = new Vector3(-fenceX, transform.position.y, 0);
        if (transform.position.x >= fenceX)
            transform.position = new Vector3(fenceX, transform.position.y, 0);
        if (transform.position.y <= -4.5f)
            transform.position = new Vector3(transform.position.x, -4.5f, 0);
        if (transform.position.y >= fenceY)
            transform.position = new Vector3(transform.position.x, fenceY, 0);
    }

    public void Damage(int damage)
    {
        hitPoint -= damage;

        if (hitPoint <= 0)
        {
            Destroy(gameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            UIManager.instance.SignalLost();
            UIManager.instance.DashOff();
            UIManager.instance.AirBreakOff();
            UIManager.instance.LockedOff();
            UIManager.instance.GunsOff();
            UIManager.instance.BurstOff();
            UIManager.instance.GunsHeatOff();

            GameManager.instance.alive = false;

            SoundManager.instance.warning = true;
            SoundManager.instance.SignalLostSoundStart();
        }
    }

    [SerializeField] LayerMask _warningLayerMask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _warningLayerMask) > 0)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1f);
            UIManager.instance.WarningOn();

            if (!SoundManager.instance.warning && SoundManager.instance.warningSoundEnd)
            {
                SoundManager.instance.warning = true;
                StartCoroutine(SoundManager.instance.WarningSound());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _warningLayerMask) > 0)
        {
            UIManager.instance.WarningOn();
            SoundManager.instance.warning = true;
            if (!SoundManager.instance.warning && SoundManager.instance.warningSoundEnd)
            {
                StartCoroutine(SoundManager.instance.WarningSound());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _warningLayerMask) > 0)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 50 / 255f);
            UIManager.instance.WarningOff();
            SoundManager.instance.warning = false;
        }
    }
}