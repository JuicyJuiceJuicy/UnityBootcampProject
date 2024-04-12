using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuperWeaponExplosion : MonoBehaviour
{
    public float shake = 0.1f;
    public int damage = 1;
    public float time = 0.5f;

    void Start()
    {
        StartCoroutine(UIManager.instance.ShakeCam(shake));
        Invoke("Fade", time);
    }

    void Fade()
    {
        UIManager.instance.ResetCam();
        Destroy(gameObject);
    }

    [SerializeField] LayerMask _playerLayerMask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _playerLayerMask) > 0)
        {
            collision.gameObject.GetComponent<Player>().Damage(damage);
        }
    }
}
