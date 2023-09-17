using System.Collections;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerData PlayerData;
    public bool end;
    public Coroutine timer;
    
    
    private void Awake()
    {
        Instance = this;
        PlayerData = new PlayerData();
    }
    

    void Start()
    {
        timer = StartCoroutine(TimeCount());
    }
    IEnumerator TimeCount()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            UIManager.Instance.SetScore(PlayerData.AddScore(1));

        }
    }
}