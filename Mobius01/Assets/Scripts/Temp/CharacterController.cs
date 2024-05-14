using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] int hitPoint = 1;

    // move
    private Vector2 moveDirection;
    [SerializeField] float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveFixedUpdate();
    }

    protected virtual void MoveFixedUpdate()
    {
        transform.Translate(moveDirection * Time.fixedDeltaTime * speed);
    }
}
