using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [Tooltip("Number Sprites and Gem Sprites")]
    public List<Sprite> sNums;

    public List<Sprite> sGems;
    public Sprite sEmpty;

    public GameObject scoreBar;
    private Image[] _scoreImages;

    public GameObject gemBar;
    private Image[] _gemImages;
    private Color _cOrange = new(0.953f, 0.612f, 0.404f);
    private Color _cBlue = new(0.408f, 0.631f, 0.961f);
    private Color _cGreen = new(0.408f, 0.953f, 0.878f);

    public Image nextGem;
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        _scoreImages = new Image[scoreBar.transform.childCount];
        for (int i = 0; i < _scoreImages.Length; i++)
        {
            _scoreImages[i] = scoreBar.transform.GetChild(i).GetComponent<Image>();
        }

        _gemImages = new Image[gemBar.transform.childCount];
        for (int i = 0; i < _gemImages.Length; i++)
        {
            _gemImages[i] = gemBar.transform.GetChild(i).GetComponent<Image>();
        }

        OnResetUI();
        TestUI();
    }


    void TestUI()
    {
        SetScore(123456789);
        List<(GemColor, GemShape)> gems = new List<(GemColor, GemShape)>();
        gems.Add((GemColor.Blue, GemShape.Circle));
        gems.Add((GemColor.Orange, GemShape.Square));
        gems.Add((GemColor.Blue, GemShape.Square));
        gems.Add((GemColor.Green, GemShape.Triangle));
        gems.Add((GemColor.Orange, GemShape.Triangle));
        gems.Add((GemColor.Orange, GemShape.Square));
        gems.Add((GemColor.Blue, GemShape.Triangle));
        SetGems(gems);
    }

    /// <summary>
    /// Clean the UI before start/restart the game
    /// </summary>
    public void OnResetUI()
    {
        foreach (var numImg in _scoreImages)
        {
            numImg.sprite = sEmpty;
        }

        foreach (var gemImg in _gemImages)
        {
            gemImg.sprite = sEmpty;
            gemImg.color = Color.white;
        }
    }


    public void SetScore(int score)
    {
        string strScore = score.ToString();
        int scoreLen = strScore.Length;
        for (int i = 0; i < _scoreImages.Length; i++)
        {
            if (i < scoreLen)
            {
                _scoreImages[i].sprite = sNums[int.Parse(strScore[scoreLen - i - 1].ToString())];
            }
            else
            {
                _scoreImages[i].sprite = sEmpty;
            }
        }
    }
    

    public void SetGems(List<(GemColor, GemShape)> gems)
    {
        for (int i = 0; i < _gemImages.Length; i++)
        {
            if (i < gems.Count)
            {
                (GemColor, GemShape) gem = gems[i];
                switch (gem.Item2)
                {
                    case GemShape.Circle:
                        _gemImages[i].sprite = sGems[0];
                        break;
                    case GemShape.Square:
                        _gemImages[i].sprite = sGems[1];
                        break;
                    case GemShape.Triangle:
                        _gemImages[i].sprite = sGems[2];
                        break;
                    default:
                        _gemImages[i].sprite = sEmpty;
                        break;
                }

                switch (gem.Item1)
                {
                    case GemColor.Blue:
                        _gemImages[i].color = _cBlue;
                        break;
                    case GemColor.Green:
                        _gemImages[i].color = _cGreen;
                        break;
                    case GemColor.Orange:
                        _gemImages[i].color = _cOrange;
                        break;
                }
            }
            else
            {
                _gemImages[i].sprite = sEmpty;
                _gemImages[i].color = Color.white;
            }
        }
    }


    public void SetNextGem((GemColor, GemShape) gem)
    {
        switch (gem.Item2)
        {
            case GemShape.Circle:
                nextGem.sprite = sGems[0];
                break;
            case GemShape.Square:
                nextGem.sprite = sGems[1];
                break;
            case GemShape.Triangle:
                nextGem.sprite = sGems[2];
                break;
            default:
                nextGem.sprite = sEmpty;
                break;
        }

        switch (gem.Item1)
        {
            case GemColor.Blue:
                nextGem.color = _cBlue;
                break;
            case GemColor.Green:
                nextGem.color = _cGreen;
                break;
            case GemColor.Orange:
                nextGem.color = _cOrange;
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}