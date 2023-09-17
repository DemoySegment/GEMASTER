using System;
using System.Collections.Generic;
using UnityEngine;

public class GemInventory : MonoBehaviour
{
    public static GemInventory Instance;
    private int maxSize = 10;
    public List<(GemColor, GemShape)> gems = new();

    private void Awake()
    {
        Instance = this;
    }

    public bool AddGem(GemColor color, GemShape shape)
    {
        if (gems.Count < maxSize)
        {
            gems.Add((color, shape));
            UIManager.Instance.SetGems(gems);
            CheckForMatches();
            return true;
        }

        GameManager.Instance.end = true;
        UIManager.Instance.OnGameEnd();
        StopAllCoroutines();
        return false;
    }


    public void SetGemInventory(List<(GemColor, GemShape)> newGems)
    {
        gems = newGems;
    }
    private void CheckForMatches()
    {
        int count = 1;
        string matchingAttribute = "";
        if (gems.Count < 3)
        {
            return;
        }
        (GemColor, GemShape) g1 = gems[^1];
        (GemColor, GemShape) g2 = gems[^2];
        (GemColor, GemShape) g3 = gems[^3];
        if((g1.Item1 == g2.Item1 && g2.Item1 == g3.Item1) || (g1.Item2 == g2.Item2 && g2.Item2 == g3.Item2))
        {
            if ((g1.Item1 == g2.Item1 && g2.Item1 == g3.Item1) && (g1.Item2 == g2.Item2 && g2.Item2 == g3.Item2))
            {
                
                UIManager.Instance.SetScore(GameManager.Instance.PlayerData.AddScore(10));
            }
            UIManager.Instance.SetZuma(gems);
            UIManager.Instance.SetScore(GameManager.Instance.PlayerData.AddScore(10));
        }




           
        
    }


    public bool FindGem(GemColor color, GemShape shape, int num)
    {

        for (int i = gems.Count - 1; i >= 0; i--)
        {
            if (num > 0 && gems[i].Item1 == color && gems[i].Item2 == shape)
            {
                gems.RemoveAt(i);
                num -= 1;
            }
        }
        if (num == 0) { return true; }
        return false;
    }
}
