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
    void Update()
    {
        // when game ends, the button should be used to restart the game instead
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameManager.Instance.end)
            {
                UIManager.Instance.OnClickRestartBtn();
            }
            else if (!jumping)
            {
                jumping = true;
                rd.AddForce(Vector2.up * rate, ForceMode2D.Impulse);
            }
        }

        if (Physics2D.OverlapCircle(transform.position, 1.3f, groundLayer))
        {
            jumping = false;
        }
    }
    
}

