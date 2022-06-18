using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool pause;
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("pause");
            if (pause)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        pause = false;
        menu.SetActive(false);
    }

    void pauseGame()
    {
        Time.timeScale = 0f;
        pause = true;
        menu.SetActive(true);
    }

    public void returnMain()
    {
        Time.timeScale = 1f;
        pause = false;
        SceneManager.LoadScene("InitialScene");
    }
}
