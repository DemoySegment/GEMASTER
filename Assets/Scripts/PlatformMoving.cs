using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    public int speed = 1;
    private bool moving = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PauseAtBegin());
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("killBar")){
            Destroy(gameObject);
        }
    }
    IEnumerator PauseAtBegin()
    {
        yield return new WaitForSeconds(1);
        moving = true;
    }
}
