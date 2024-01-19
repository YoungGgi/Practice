using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    
    [SerializeField]
    private Vector2 inputVec;
    
    [SerializeField]
    private float speed;
    Rigidbody2D rigid;
    
    private void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();

    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }


    private void FixedUpdate() 
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        
        rigid.MovePosition(rigid.position + nextVec);
    }
}
