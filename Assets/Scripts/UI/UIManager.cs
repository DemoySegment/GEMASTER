using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Image[] zumaImages;
    private Image[] _gemImages;
    private Color _cOrange = new(0.953f, 0.612f, 0.404f);
    private Color _cBlue = new(0.408f, 0.631f, 0.961f);
    private Color _cGreen = new(0.408f, 0.953f, 0.878f);

    public Image nextGem;
    
    public GameObject GameOverPanel;
    
    public static UIManager Instance;
    public bool test = false;

    private void Awake()
    {
        Instance = this;
        iTween.Init(gameObject);
    }


    void Start()
    {
        GameOverPanel.SetActive(false);
        
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
    }
    
    void Update()
    {
        if (test)
        {
            TestUI();
            test = false;
        }
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

        nextGem.sprite = sEmpty;
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


    public void SetGems(List<(GemColor, GemShape)> gems, bool anime = true)
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

        if (anime)
        {
            iTween.ScaleFrom(_gemImages[gems.Count - 1].gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
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

        iTween.ScaleFrom(nextGem.gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
    }


    /// <summary>
    /// Set Zuma Effects
    /// </summary>
    /// <param name="gems">Gems Collection before Erase the last three consecutive gems</param>
    public void SetZuma(List<(GemColor, GemShape)> gems)
    {
        SetZumaEffect(gems.Count);
        gems.RemoveRange(gems.Count - 3, 3);
        SetGems(gems, anime: false);
    }

    
    public void OnGameEnd()
    {
        GameOverPanel.SetActive(true);
    }

    public void OnClickRestartBtn()
    {
        SceneManager.LoadScene("Start");
    }


    /// <summary>
    /// Zuma eliminates the effect
    /// </summary>
    /// <param name="gemLengthBefore">the number of gems before Zuma elimination</param> 
    private void SetZumaEffect(int gemLengthBefore)
    {
        if (gemLengthBefore < 3)
        {
            return;
        }

        Image[] zumaGemImgs =
        {
            _gemImages[gemLengthBefore - 1], _gemImages[gemLengthBefore - 2], _gemImages[gemLengthBefore - 3]
        };

        StartCoroutine(EliminationEffect(zumaGemImgs));
    }

    IEnumerator EliminationEffect(Image[] zumaGemImgs)
    {
        for (int i = 0; i < zumaGemImgs.Length; i++)
        {
            zumaImages[i].sprite = zumaGemImgs[i].sprite;
            zumaImages[i].color = zumaGemImgs[i].color;
            zumaImages[i].gameObject.GetComponent<RectTransform>().anchoredPosition =
                zumaGemImgs[i].gameObject.GetComponent<RectTransform>().anchoredPosition;
            iTween.MoveTo(zumaImages[i].gameObject,
                new Vector2(zumaImages[i].gameObject.transform.position.x, -100), 0.5f);
        }

        yield break;
    }

    void TestUI()
    {
        SetScore(Random.Range(0, 100000));
        List<(GemColor, GemShape)> gems = new List<(GemColor, GemShape)>();
        for (int i = 0; i < Random.Range(3, 10); i++)
        {
            int c = Random.Range(0, 3); // color
            int s = Random.Range(0, 3); // shape
            GemColor color;
            GemShape shape;
            if (c == 0)
            {
                color = GemColor.Blue;
            }
            else if (c == 1)
            {
                color = GemColor.Green;
            }
            else
            {
                color = GemColor.Orange;
            }

            if (s == 0)
            {
                shape = GemShape.Circle;
            }
            else if (c == 1)
            {
                shape = GemShape.Square;
            }
            else
            {
                shape = GemShape.Triangle;
            }

            gems.Add((color, shape));
        }

        SetGems(gems);
        SetNextGem(gems[^1]);
        SetZuma(gems);
        OnGameEnd();
    }
}