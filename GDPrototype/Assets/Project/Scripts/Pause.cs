using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject levelSelect;
    private bool isFalse = false;
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isFalse == false)
        {
            isFalse = true;
            PauseGame();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isFalse == true)
        {
            isFalse = false;
            ResumeGame();
        }
    }

    public void setPanel(int p)
    {
        switch(p)
        {
            case 0:
                pausePanel.SetActive(true);
                levelSelect.SetActive(false);
                break;
            case 1:
                pausePanel.SetActive(false);
                levelSelect.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isFalse = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void restartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        SceneManager.LoadScene(scene.name);
    }

    public void loadGameLevelOne()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void loadGameLevelTwo()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public void loadGameLevelThree()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(3);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
