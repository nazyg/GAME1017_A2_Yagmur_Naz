using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void GoToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
