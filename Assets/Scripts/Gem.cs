using System.Collections;
using System.Collections.Generic;
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
public class Gem : MonoBehaviour
{

    GameManager _gameManager;
    public Sprite gemSprite;
    public GemColor color;
    public GemShape shape;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _gameManager.addScore();
            Destroy(gameObject);
        }
    }
}