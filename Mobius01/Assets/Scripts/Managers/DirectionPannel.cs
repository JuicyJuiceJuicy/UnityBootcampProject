using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPannel : MonoBehaviour
{
    public SpriteRenderer upSr;
    public SpriteRenderer downSr;
    public SpriteRenderer leftSr;
    public SpriteRenderer rightSr;
    public SpriteRenderer stopSr;

    void Update()
    {
        if (GameManager.instance.systemOnline)
        {
            Pannel();
        }
    }

    void Pannel()
    {
        if (0 < Input.GetAxis("Vertical"))
        {
            upSr.color = new Color(0, 1, 0, 1);
            downSr.color = new Color(0, 1, 0, 5 / 255f);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            upSr.color = new Color(0, 1, 0, 5 / 255f);
            downSr.color = new Color(0, 1, 0, 1);
        }
        else
        {
            upSr.color = new Color(0, 1, 0, 5 / 255f);
            downSr.color = new Color(0, 1, 0, 5 / 255f);
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            leftSr.color = new Color(0, 1, 0, 1);
            rightSr.color = new Color(0, 1, 0, 5 / 255f);
        }
        else if (0 < Input.GetAxis("Horizontal"))
        {
            leftSr.color = new Color(0, 1, 0, 5 / 255f);
            rightSr.color = new Color(0, 1, 0, 1);
        }
        else
        {
            leftSr.color = new Color(0, 1, 0, 5 / 255f);
            rightSr.color = new Color(0, 1, 0, 5 / 255f);
        }

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            stopSr.color = new Color(0, 1, 0, 1);
        }
        else
        {
            stopSr.color = new Color(0, 1, 0, 5 / 255f);
        }
    }
}
