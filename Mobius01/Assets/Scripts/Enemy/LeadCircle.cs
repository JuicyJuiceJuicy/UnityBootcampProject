using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadCircle : MonoBehaviour
{
    public Transform EnemyT;
    public float leadRate = 0.25f; // EnemySpeed / PlayerMissileSpeed
    public float leadRate0 = 0.25f;
    float leadDistance;
    public sbyte direction = 1;
    public sbyte isJam = 0;
    SpriteRenderer sR;

    private void Start()
    {
        sR = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        GetLeadDistance();
    }

    void GetLeadDistance()
    {
        leadDistance = (transform.position.y - GameManager.instance.playerY) * leadRate;
        if (GameManager.instance.jammed && isJam == 0)
        {
            sR.color = new Color(0, 1, 0, 5 / 255f);
            leadDistance *= isJam;
        }
        else
        {
            sR.color = new Color(0, 1, 0, 1f);
        }
        transform.position = EnemyT.position + new Vector3(leadDistance * direction, 0, 0);
    }

    public void LeadOff()
    {
        leadRate = 0;
    }

    public void LeadOn()
    {
        leadRate = leadRate0;
    }
}
