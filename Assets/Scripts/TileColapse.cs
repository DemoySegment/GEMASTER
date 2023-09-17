using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColapse : MonoBehaviour
{
    [SerializeField]
    private bool triggerTileGenerator = false;

    public bool TriggerTileGenerator { get => triggerTileGenerator; set => triggerTileGenerator = value; }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("BoarderTriggered");
        if (collision.CompareTag("Boarder"))
        {

            Destroy(this.gameObject);
            if (triggerTileGenerator)
            {
                GroundGenerator.instance.generateStage();
            }
           
            
        }
    }

}
