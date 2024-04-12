using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    public float speed = 10f;
    public GameObject ExplosionMissile;
    public int damage = 1;

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += new Vector3
            (0,
            speed * Time.deltaTime,
            0);
    }

    [SerializeField] LayerMask _enemyLayerMask;
    [SerializeField] LayerMask _enemyGroundLayerMask;
    [SerializeField] LayerMask _enemyBossLayerMask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _enemyLayerMask) > 0)
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Enemy>().Hit(damage);
            Instantiate(ExplosionMissile, transform.position, Quaternion.identity);;
        }
        else if ((1 << collision.gameObject.layer & _enemyGroundLayerMask) > 0)
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<GroundEnemy>().Hit(damage);
            Instantiate(ExplosionMissile, transform.position, Quaternion.identity); ;
        }
        else if ((1 << collision.gameObject.layer & _enemyBossLayerMask) > 0)
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Boss>().Hit(damage);
            Instantiate(ExplosionMissile, transform.position, Quaternion.identity); ;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
