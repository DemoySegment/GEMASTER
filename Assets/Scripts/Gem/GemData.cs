using UnityEngine;

public enum GemColor
{
    Blue,
    Green,
    Orange
}
public enum GemShape
{
    Square,
    Circle,
    Triangle
}
public class GemData:MonoBehaviour
{
    public Sprite gemSprite;
    public GemColor Color;
    public GemShape Shape;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GemInventory.Instance.AddGem(Color, Shape);
            Destroy(gameObject);
        }
    }
}
