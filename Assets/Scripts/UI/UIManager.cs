using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;


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

        for (int i = 0; i < zumaImages.Length; i++)
        {
            zumaImages[i].sprite = sEmpty;
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
            _gemImages[i].GetComponent<RectTransform>().localScale = Vector3.one;
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


    /// <summary>
    /// UI Effect for Gems used to fix the bridge
    /// </summary>
    /// <param name="gemIndexes">List of gem index in the gem collection list</param>
    /// <param name="gems">gem collection list before using gems to fix the bridge</param>
    public void SetGemUsedForFixBridge(List<int> gemIndexes, List<(GemColor, GemShape)> gems)
    {
        int maxIndex = -1;
        foreach (var index in gemIndexes)
        {
            maxIndex = Math.Max(maxIndex, index);
        }

        if (maxIndex >= gems.Count)
        {
            Debug.LogError("The max index of the gem index you offer exceeds the number of gems in the collection");
            return;
        }

        StartCoroutine(FixBridgeEffect(gemIndexes, gems));
    }

    private IEnumerator FixBridgeEffect(List<int> gemIndexes, List<(GemColor, GemShape)> gems)
    {
        List<(GemColor, GemShape)> gemsUsed = new List<(GemColor, GemShape)>();
        foreach (var index in gemIndexes)
        {
            iTween.ScaleFrom(_gemImages[index].gameObject, new Vector3(1.3f, 1.3f, 1.3f), 0.5f);
            gemsUsed.Add(gems[index]);
        }

        foreach (var gem in gemsUsed)
        {
            gems.Remove(gem);
        }

        yield return new WaitForSeconds(0.5f);
        SetGems(gems);
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

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < zumaImages.Length; i++)
        {
            zumaImages[i].sprite = sEmpty;
        }
    }

    void TestUI()
    {
        SetScore(Random.Range(0, 100000));
        List<(GemColor, GemShape)> gems = new List<(GemColor, GemShape)>();
        for (int i = 0; i < Random.Range(5, 11); i++)
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
        List<int> indexList = new List<int>();
        indexList.Add(1);
        indexList.Add(2);
        indexList.Add(3);
        SetGemUsedForFixBridge(indexList, gems);
        // SetZuma(gems);
        // OnGameEnd();
    }
}