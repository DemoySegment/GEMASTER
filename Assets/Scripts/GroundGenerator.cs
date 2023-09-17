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
    private int groundLayer = 7;

    public static GroundGenerator instance;

    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update

    void Start()
    {
        cursor.transform.position = ground.transform.position;
        generateStage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // generate a batch of ground composed by the basicPiece, a slot will be involved in between
    public void generateStage()
    {
        float width = basicPiece.transform.localScale.x*2;
        int trapStart = Random.Range(batchSize / 2, batchSize - clifWidth);
        Debug.Log(trapStart);
        for (int i = 0; i < batchSize; i++)
        {
            if(i < trapStart || i >= trapStart + clifWidth)
            {
                GameObject t = Instantiate(basicPiece, cursor.position, new Quaternion());
                TileColapse tc =  t.AddComponent<TileColapse>();
                tc.TriggerTileGenerator = i % (batchSize/2) == 0;

                t.transform.SetParent(ground, true);
            }

            cursor.transform.position = cursor.transform.position+new Vector3(width, 0);
        }

        


        


    }
    public void test()
    {
        Debug.Log("invoked");
    }
}
