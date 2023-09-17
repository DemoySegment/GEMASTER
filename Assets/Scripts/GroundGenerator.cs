using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject basicPiece;
    [SerializeField] 
    private Transform ground;

    [SerializeField]
    private Transform cursor;
    [SerializeField]
    private int batchSize = 20;
    [SerializeField]
    private int clifWidth = 5;
    [SerializeField]
    public int groundLayer = 7;


    // Start is called before the first frame update

    void Start()
    {
        cursor.transform.position = ground.transform.position;
        generateStage(20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // generate a batch of ground composed by the basicPiece, a slot will be involved in between
    private void generateStage(int length)
    {
        float width = basicPiece.transform.localScale.x*2;
        int trapStart = Random.Range(length/2, length - clifWidth);
        Debug.Log(trapStart);
        for (int i = 0; i < length; i++)
        {
            if(i < trapStart || i >= trapStart + clifWidth)
            {
                GameObject t = Instantiate(basicPiece, cursor.position, new Quaternion());
                t.transform.SetParent(ground, true);
            }

            cursor.transform.position = cursor.transform.position+new Vector3(width, 0);
        }

        


        


    }
}
