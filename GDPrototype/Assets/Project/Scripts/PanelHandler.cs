using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelHandler : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPanel(int p)
    {
        switch(p)
        {
            case 0:
                mainMenu.SetActive(true);
                levelSelect.SetActive(false);
                break;
            case 1:
                mainMenu.SetActive(false);
                levelSelect.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void loadGameLevelOne()
    {
        SceneManager.LoadScene(1);
    }

    public void loadGameLevelTwo()
    {
        SceneManager.LoadScene(2);
    }

    public void loadGameLevelThree()
    {
        SceneManager.LoadScene(3);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
