using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIStartManager : MonoBehaviour
    {
        
        public void OnClickStart()
        {
            SceneManager.LoadScene("Gem");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnClickStart();
            }
        }
    }
}