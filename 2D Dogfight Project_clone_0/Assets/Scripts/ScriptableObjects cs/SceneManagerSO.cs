using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneManager", menuName = "Game /SceneManager")]
public class SceneManagerSO : ScriptableObject
{
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }
}
