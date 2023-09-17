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
            return true;
        }

        return false;
    }


    public void SetGemInventory(List<(GemColor,GemShape)> newGems){
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

        for (int i = gems.Count - 2; i < gems.Count; i++)
        {
            GemColor curColor = gems[i].Item1;
            GemShape curShape = gems[i].Item2;

            GemColor prevColor = gems[i - 1].Item1;
            GemShape prevShape = gems[i - 1].Item2;

            if (curColor == prevColor && (matchingAttribute == "color" ||
                                          matchingAttribute == ""))
            {
                count += 1;
                matchingAttribute = "color";
            }

            if (curShape == prevShape && (matchingAttribute == "shape" ||
                                          matchingAttribute == ""))
            {
                count += 1;
                matchingAttribute = "shape";
            }

            if (count >= 3)
            {
                for (int j = 0; j < 3; j++)
                {
                    gems.RemoveAt(gems.Count - 1);
                }

                count = 1;
            }

            UIManager.Instance.SetGems(gems);
        }
    }
}