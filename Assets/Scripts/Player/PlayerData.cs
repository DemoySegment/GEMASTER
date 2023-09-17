using System.Collections.Generic;

public class PlayerData
{
    private int _score = 0;
    private List<GemData> _gemCollection = new();

    public void AddScore(int scoreNum)
    {
        _score += scoreNum;
    }

    public void ClearScore()
    {
        _score = 0;
    }

    public void AddGem(GemData data)
    {
        _gemCollection.Add(data);
    }

    public void CleanGemCollection()
    {
        _gemCollection.Clear();
    }


    /// <summary>
    /// Check the latest three gems are in consecutive [color|shape] 
    /// </summary>
    /// <returns></returns>
    public bool Zuma()
    {
        if (_gemCollection.Count < 3) return false;

        // the last three gem
        GemData g1 = _gemCollection[^1];
        GemData g2 = _gemCollection[^2];
        GemData g3 = _gemCollection[^3];

        return (g1.Color == g2.Color && g2.Color == g3.Color) || (g1.Shape == g2.Shape && g2.Shape == g3.Shape);
    }
}