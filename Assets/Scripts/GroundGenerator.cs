using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


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
    [SerializeField] private float gemChance = 0.5f;
    [SerializeField] private int highSpawnPoint;
    [SerializeField] private int lowSpawnPoint;
    [SerializeField] private GameObject sign;

    [SerializeField] private List<GameObject> gemsPrefabs;
    private Color _cOrange = new(0.953f, 0.612f, 0.404f);
    private Color _cBlue = new(0.408f, 0.631f, 0.961f);
    private Color _cGreen = new(0.408f, 0.953f, 0.878f);

    private int distanceToGap = 7;
    private List<float> gemsAfterSign = new List<float>();
    public static GroundGenerator instance;
    private bool leaveSpace = false;

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

    // generate a batch of ground composed by the basicPiece, a slot will be involved in between
    public void generateStage()
    {
        float width = basicPiece.transform.localScale.x * 2;
        int trapStart = Random.Range(batchSize / 2, batchSize - clifWidth);
        int signIndex = trapStart + distanceToGap;
        bool generateCheck = true;
        float rand;
        generateGemsBeforeGap();
        Debug.Log(trapStart);
        for (int i = 0; i < batchSize; i++)
        {

            //Debug.Log(leaveSpace);
            //Debug.Log(i);
            if (i == signIndex)
            {
                Instantiate(sign, cursor.position, Quaternion.identity);
            }
            if (i > signIndex && i < trapStart)
            {
                spawnGem(gemsAfterSign[i - signIndex]);
            }
            if (i < trapStart || i >= trapStart + clifWidth)
            {
                rand = Random.Range(0f, 1f);
                spawnGem(rand);
                GameObject t = Instantiate(basicPiece, cursor.position, new Quaternion());
                TileColapse tc = t.AddComponent<TileColapse>();
                tc.TriggerTileGenerator = i % (batchSize / 2) == 0;

                t.transform.SetParent(ground, true);
            }

            cursor.transform.position = cursor.transform.position + new Vector3(width, 0);
        }
    }

    private void spawnGem(float rand)
    {
        if (leaveSpace)
        {
            leaveSpace = false;
            return;
        }
        if (rand < gemChance)
        {
            int randGem = Random.Range(0, gemsPrefabs.Count);
            int randShape = Random.Range(0, 3);
            GameObject gem;
            if (rand < gemChance / 2)
                gem = Instantiate(gemsPrefabs[randGem], new Vector3(0, highSpawnPoint, 0) + cursor.position, Quaternion.identity);
            else
            {
                gem = Instantiate(gemsPrefabs[randGem], new Vector3(0, lowSpawnPoint, 0) + cursor.position, Quaternion.identity);
            }
            switch (randShape)
            {
                case 0:
                    gem.GetComponent<SpriteRenderer>().color = _cOrange;
                    gem.GetComponent<GemData>().Color = GemColor.Orange;
                    break;
                case 1:
                    gem.GetComponent<SpriteRenderer>().color = _cBlue;
                    gem.GetComponent<GemData>().Color = GemColor.Blue;
                    break;
                case 2:
                    gem.GetComponent<SpriteRenderer>().color = _cGreen;
                    gem.GetComponent<GemData>().Color = GemColor.Green;
                    break;
            }
            gem.transform.SetParent(ground, true);
            leaveSpace = true;
        }
    }
    public void test()
    {
        Debug.Log("invoked");
    }
    private void generateGemsBeforeGap()
    {
        int gemRequirement = Random.Range(1, 4);
        for (int j = 0; j < gemRequirement; j++)
        {
            gemsAfterSign.Add(Random.Range(0f, 0.2f));
        }

        while (gemsAfterSign.Count < distanceToGap)
        {
            gemsAfterSign.Add(Random.Range(0f, 1f));
        }

        gemsAfterSign = Shuffle(gemsAfterSign);
    }

    List<float> Shuffle(List<float> list)
    {
        return list.OrderBy(x => Random.value).ToList();
    }

    GemColor GetRandomGemColor()
    {
        GemColor[] colors = (GemColor[])System.Enum.GetValues(typeof(GemColor));
        return colors[Random.Range(0, colors.Length)];
    }

    GemShape GetRandomGemShape()
    {
        GemShape[] shapes = (GemShape[])System.Enum.GetValues(typeof(GemShape));
        return shapes[Random.Range(0, shapes.Length)];
    }
}
