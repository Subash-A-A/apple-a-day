using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    [SerializeField] GameObject Story;
    [SerializeField] GameObject Controls;
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowStory()
    {
        Story.SetActive(true);
    }

    public void HideStory()
    {
        Story.SetActive(false);
    }
    public void ShowControls()
    {
        Controls.SetActive(true);
    }

    public void HideControls()
    {
        Controls.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
