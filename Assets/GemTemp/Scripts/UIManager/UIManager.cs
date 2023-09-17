using System.Collections.Generic;
using GemTemp.Scripts.Enum;
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
    public static UIManager instance;

    
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
        //setscore(123456789);
        //list<gem> gems = new list<gem>();
        //gems.add(new gem(gemshape.square, gemcolor.blue));
        //gems.add(new gem(gemshape.square, gemcolor.orange));
        //gems.add(new gem(gemshape.circle, gemcolor.green));
        //gems.add(new gem(gemshape.triangle, gemcolor.green));
        //gems.add(new gem(gemshape.square, gemcolor.green));
        //setgems(gems);
        //setnextgem(new gem(gemshape.square, gemcolor.blue));
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
    
    public void SetGems(List<Gem> gems)
    {
        for (int i = 0; i < _gemImages.Length; i++)
        {
            if (i < gems.Count)
            {
                Gem gem = gems[i];
                switch (gem.shape)
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

                switch (gem.color)
                {
                    case GemColor.Blue:
                        _gemImages[i].color  = _cBlue;
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


    public void SetNextGem(Gem gem)
    {
        switch (gem.shape)
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
        
        switch (gem.color)
        {
            case GemColor.Blue:
                nextGem.color  = _cBlue;
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