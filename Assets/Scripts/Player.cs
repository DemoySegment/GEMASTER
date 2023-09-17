using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public LayerMask groundLayer;
    [SerializeField]
    private float speed = 0.2f;
    [SerializeField]
    private bool jumping = false;
    [SerializeField]
    private float rate = 1f;

    private Rigidbody2D rd;
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        jumping = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y+speed, transform.position.z);
        if(!jumping && Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(Jump());
        }
    }
    IEnumerator Jump()
    {
        rd.AddForce(Vector2.up*rate, ForceMode2D.Impulse);
        jumping = true;
        yield return new WaitUntil(IsOnGround);
        jumping = false;
        yield return null;
    }
    private bool IsOnGround()
    {

        bool grounded = Physics2D.OverlapCircle(transform.position, 1.5f+0.1f, groundLayer);
        Debug.Log(grounded);
        return grounded;
 
    }
}
