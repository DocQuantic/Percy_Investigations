using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;

    public void Update()
    {
        if (!pauseMenu.activeSelf)
        {
            if (Input.GetKeyDown("escape"))
            {
                pauseMenu.SetActive(true);
            }
        }
        else
        {
            if (Input.GetKeyDown("escape"))
            {
                pauseMenu.SetActive(false);
            }
        }
    }

    public void LoadMap(int mapInteger)
    {
        SceneManager.LoadScene(mapInteger);
    }

    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
