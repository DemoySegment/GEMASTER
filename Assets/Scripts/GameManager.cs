using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerData PlayerData;
    
    
    private void Awake()
    {
        Instance = this;
        PlayerData = new PlayerData();
    }
    

    void Start()
    {
    }
}