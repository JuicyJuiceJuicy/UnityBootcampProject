using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    Transform tF;
    Animator aN;
    public Transform tFGUI;
    public GameObject Explosion;

    // GroundMove
    public float scrollSpeed = 0.5f;
    public float controlSpeed = 0.25f;

    public float xy = 0.58f;
    public float mapSizeY = 10f;
    public float mapSizeX = 2.9f;

    Vector3 moveVec;

    bool front = false;

    public int direction = 1;
    public float speed = 2.5f;

    // Spotted
    bool spotted;

    // Hit
    public int hitPoint = 3;
    int score = 1;
    public bool armored;

    // ShackCame
    public float shake = 0.5f;

    void Awake()
    {
        tF = GetComponent<Transform>();
        aN = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundMove();
        Return();
        Spotted();

        if (front)
        {
            aN.SetBool("front", true);
        }
        else
        {
            Move();
            aN.SetBool("front", false);
        }
    }

    void GroundMove()
    {
        moveVec = new(-(Input.GetAxisRaw("Horizontal") * controlSpeed * xy), - (Input.GetAxisRaw("Vertical") * controlSpeed) - scrollSpeed, 0);
        tF.Translate(moveVec * Time.deltaTime);
    }

    private void Move()
    {
        tF.Translate(direction * speed * Time.deltaTime, 0, 0);
    }

    private void Spotted()
    {
        if (tF.position.y < 5)
        {

            if (!spotted)
            {
                spotted = true;
                StartCoroutine(Turn());
            }

            tFGUI.position = new Vector3(tF.position.x, tF.position.y - 0.5f, 0);
        }
        else if (tF.position.y < 10)
        {
            tFGUI.position = new Vector3(tF.position.x, 4.5f, 0);
        }
    }

    IEnumerator Turn()
    {
        yield return new WaitForSeconds(0.5f);
        front = true;
        yield return new WaitForSeconds(0.5f);
    }

    void Return()
    {
        if (tF.position.y < -5)
        {
            tF.position += new Vector3(0, mapSizeY, 0);
            spotted = false;
            front = false;
        }    

        if (tF.position.x < -mapSizeX)
        {
            tF.position += new Vector3(2 * mapSizeX, 0, 0);
        }
        else if (tF.position.x > mapSizeX)
        {
            tF.position -= new Vector3(2 * mapSizeX, 0, 0);
        }
    }

    public void Hit(int damage)
    {
        if (!(front && armored))
        {
            hitPoint -= damage;
            if (hitPoint <= 0)
            {
                Destroy(gameObject);
                Instantiate(Explosion, transform.position, Quaternion.identity);
                GameManager.instance.AddScore(score);
                GameManager.instance.JamOff();
            }
        }
    }
}
