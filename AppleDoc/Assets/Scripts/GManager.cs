using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] GameObject DefeatScreen;
    [SerializeField] GameObject VictoryScreen;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] TimeManager timeManager;

    private WeponCycle cycle;

    private void Start()
    {
        cycle = FindObjectOfType<WeponCycle>();
        ResumeGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.SetActive(true);
            PauseGame();
        }
    }
    public Transform GetPlayerTransform()
    {
        return player;
    }

    public void Victory()
    {
        StartCoroutine(GameVictory());
    }
    public void Defeat()
    {
        StartCoroutine(GameDefeat());
    }

    public void Retry()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator GameVictory()
    {
        yield return new WaitForSeconds(5f);
        PauseGame();
        DefeatScreen.SetActive(false);
        VictoryScreen.SetActive(true);
    }
    IEnumerator GameDefeat()
    {
        yield return new WaitForSeconds(5f);
        PauseGame();
        VictoryScreen.SetActive(false);
        DefeatScreen.SetActive(true);
    }

    public void PauseGame()
    {
        cycle.enabled = false;
        timeManager.PauseGame();
        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        cycle.enabled = true;
        timeManager.StopSlowMotion();
        AudioListener.pause = false;
        PauseMenu.SetActive(false);
    }
}
