using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GemUI : MonoBehaviour
{
    public Image[] gemSlots;

    public Dictionary<(GemColor,GemShape), Sprite> gemSprites; 
    public static GemUI instance;
    public void UpdateGemDisplay(List<(GemColor,GemShape)> gems)
    {
        for (int i = 0; i < gemSlots.Length; i++)
        {
            if (i < gems.Count)
            {
                gemSlots[i].sprite = gemSprites[gems[i]];
            }
            else
            {  
                gemSlots[i].sprite = null;
            }
        }
    }
}
