using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderIngame : MonoBehaviour
{
    
    public void LoadSameLevel()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadNextLevel()
    {
        Scene temp = SceneManager.GetActiveScene();

        SceneManager.LoadScene("Level" + (int.Parse(Regex.Replace(SceneManager.GetActiveScene().name, "[^0-9]", "")) + 1).ToString());
        SceneManager.UnloadSceneAsync(temp);
        
    }
}
