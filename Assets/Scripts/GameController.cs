using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  public GameObject gameOver;
  public GameObject PauseMenu;
  public static GameController instance;

  void Start()
  {
    instance = this;
  }

  public void ShowGameOver()
  {
    gameOver.SetActive(true);
  }

  public void RestartGame(string lvlName)
  {
    SceneManager.LoadScene(lvlName);
  }

  public void PauseGame()
  {
    if (Time.timeScale == 0)
    {
      PauseMenu.SetActive(false);
      Time.timeScale = 1;
      return;
    }
    PauseMenu.SetActive(true);
    Time.timeScale = 0;
  }
}