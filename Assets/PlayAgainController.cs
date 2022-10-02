using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainController : MonoBehaviour
{

    public void PlayAgain()
    {
        SceneManager.LoadScene("StartMenu");
    }

}
