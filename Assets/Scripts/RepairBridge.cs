using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairBridge : MonoBehaviour
{
    private GemData requiredGem;
    public int gemRequirement = 3;

    private string requirement;
    private GemColor color;
    private GemShape shape;
    void Start()
    {
        if (Random.Range(0f, 1f) < 0.5f)
        {
            requirement = "color";
            color = GetRandomGemColor();
        }
        else
        {
            requirement = "shape";
            shape = GetRandomGemShape();
        }
    }
    public void checkGemShape(GemShape shape)
    {

        if (!GemInventory.Instance.FindShapes(shape,gemRequirement))
        {
            UIManager.Instance.OnGameEnd();
        }
    }
    public void checkGemColor(GemColor color)
    {
        if (!GemInventory.Instance.FindColors(color,gemRequirement))
        {
            UIManager.Instance.OnGameEnd();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (requirement == "shape"){
                checkGemShape(shape);
            }
            else{
                checkGemColor(color);
            }
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
