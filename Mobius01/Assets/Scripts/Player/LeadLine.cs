using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadLine : MonoBehaviour
{
    public GameObject Lead1;
    public GameObject Lead2;
    public GameObject Lead3;
    public GameObject Lead4;
    public GameObject Lead5;
    public GameObject Lead6;
    public GameObject Lead7;
    public GameObject Lead8;
    public GameObject Lead9;

    [SerializeField] LayerMask _leadCircleLayerMask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _leadCircleLayerMask) > 0)
        {
            if (!SoundManager.instance.lockedOn && SoundManager.instance.lockedOnSoundEnd)
            {
                SoundManager.instance.lockedOn = true;
                StartCoroutine(SoundManager.instance.LockedOnSound());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _leadCircleLayerMask) > 0)
        {
            Lead1.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1f);
            Lead2.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1f);
            Lead3.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1f);
            Lead4.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1f);
            Lead5.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1f);
            Lead6.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1f);
            Lead7.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1f);
            Lead8.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1f);
            Lead9.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1f);
            UIManager.instance.LockedOn();

            if (!SoundManager.instance.lockedOn && SoundManager.instance.lockedOnSoundEnd)
            {
                SoundManager.instance.lockedOn = true;
                StartCoroutine(SoundManager.instance.LockedOnSound());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _leadCircleLayerMask) > 0)
        {
            Lead1.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 30/255f);
            Lead2.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 30/255f);
            Lead3.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 30/255f);
            Lead4.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 30/255f);
            Lead5.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 30/255f);
            Lead6.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 30/255f);
            Lead7.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 30/255f);
            Lead8.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 30/255f);
            Lead9.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 30/255f);
            UIManager.instance.LockedOff();

            SoundManager.instance.lockedOn = false;
        }
    }
}
