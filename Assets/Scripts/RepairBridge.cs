using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairBridge : MonoBehaviour
{
    private GemData requiredGem;
    public int gemRequirement = 3;

    private GemColor color;
    private GemShape shape;
    void Start()
    {
            color = GetRandomGemColor();
            shape = GetRandomGemShape();
    }
    public void checkGem()
    {

        if (!GemInventory.Instance.FindGem(color,shape,gemRequirement))
        {
            UIManager.Instance.OnGameEnd();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           checkGem();
        }
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
