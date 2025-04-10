using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime
{
    public class ReloadScene : MonoBehaviour
    {
        public void ReloadClick()
        {
            SceneManager.LoadScene(0);
            Time.timeScale=1;
        }
    }
}