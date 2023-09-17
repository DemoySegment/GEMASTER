using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GemSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> gems = new List<GameObject>();
    [SerializeField]
    private int platformLength = 20;
    [SerializeField]
    private float gemChance = 1.0f;
    [SerializeField]
    private Transform highSpawnPoint;
    [SerializeField]
    private Transform lowSpawnPoint;
    [SerializeField]
    
    private int distanceToGap = 5;
    [SerializeField]
    private Transform ground;
    public int signIndex = 10;
    private List<float> gemsAfterSign = new List<float>();
    
    public int gemRequirement = 3;
    public Transform cursor;
    public int width= 10;

    public static GemSpawner instance;

    private void Awake() {
        instance = this;
    }
    void Start()
    {
        generateGems(platformLength, signIndex, gemRequirement);
    }

    private void generateGems(int platformLength,int signIndex,int gemRequirement)
    {
        bool generateCheck = true;
        float rand;

        for (int i = 0; i < platformLength; i++)
        {
            Debug.Log(i);
            if (i > signIndex && i < signIndex + distanceToGap)
            {
                if (generateCheck)
                {
                    // generateGemsBeforeGap();
                    generateCheck = false;
                }
            }
            else
            {
                rand = Random.Range(0f, 1f);
                Debug.Log(rand);
                if (rand < gemChance)
                {
                    Debug.Log("here");
                    int randGem = Random.Range(0, gems.Count);
                    GameObject gem;
                    // Instantiate(gems[randGem], highSpawnPoint.position + cursor.position, Quaternion.identity);
                    if (rand < gemChance/2)
                        gem = Instantiate(gems[randGem], highSpawnPoint.position + cursor.position, Quaternion.identity);
                    else {
                        gem = Instantiate(gems[randGem], lowSpawnPoint.position + cursor.position, Quaternion.identity);}
                    gem.transform.SetParent(ground, true);       
                }
                cursor.position+= new Vector3(width,0);
            }
        }
    }

    private void generateGemsBeforeGap()
    {
        for (int j = 0; j < gemRequirement; j++)
        {
            gemsAfterSign.Add(Random.Range(0f, 0.2f));
        }

        while (gemsAfterSign.Count < distanceToGap)
        {
            gemsAfterSign.Add(Random.Range(0f, 1f));
        }

        gemsAfterSign = Shuffle(gemsAfterSign);

        foreach (var num in gemsAfterSign)
        {
            Debug.Log(num);
            int randGem = Random.Range(0, gems.Count);
            if (num < 0.1f)
                Instantiate(gems[randGem], highSpawnPoint.position + cursor.position, Quaternion.identity);
            else if (num < 0.2f)
                Instantiate(gems[randGem], lowSpawnPoint.position + cursor.position, Quaternion.identity);
            cursor.position+= new Vector3(width,0);
        }
    }

    List<float> Shuffle(List<float> list)
    {
        return list.OrderBy(x => Random.value).ToList();
    }
}
