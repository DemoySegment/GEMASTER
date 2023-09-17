using System.Collections.Generic;

public class PlayerData
{
    private int _score = 0;
    private List<GemData> _gemCollection = new();

    public int AddScore(int scoreNum)
    {
        _score += scoreNum;
        return _score;
    }

    public int GetScore() { return _score; }

    public void SetScore(int t) { _score = t; }


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

}