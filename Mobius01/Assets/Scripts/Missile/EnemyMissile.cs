using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 1;
    public GameObject Explosion;

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);
    }

    [SerializeField] LayerMask _playerLayerMask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _playerLayerMask) > 0)
        {
            Destroy(gameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<Player>().Damage(damage);
        }
    }
        private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
