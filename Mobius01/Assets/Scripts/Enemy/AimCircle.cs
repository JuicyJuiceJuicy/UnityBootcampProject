using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCirle : MonoBehaviour
{
    Transform tF;
    SpriteRenderer sR;

    public Transform tFLC;

    public int isJam = 0;

    public float hitSize = 0.22f;

    private void Start()
    {
        tF = GetComponent<Transform>();
        sR = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        tF.position = new Vector2(GameManager.instance.playerX, tF.position.y);

        if (GameManager.instance.jammed && isJam == 0)
        {
            sR.color = new Color(0, 1, 0, 0);
        }
        else if (tF.position.x - tFLC.position.x < hitSize && -hitSize < tF.position.x - tFLC.position.x)
        {
            sR.color = new Color(0, 1, 0, 1);
        }
        else
        {
            sR.color = new Color(0, 1, 0, 50/255f);
        }
    }
}
