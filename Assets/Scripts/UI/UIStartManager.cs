using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIStartManager : MonoBehaviour
    {
        
        public void OnClickStart()
        {
            Debug.Log("start!!");
            SceneManager.LoadScene("Gem");
        }
    }
}