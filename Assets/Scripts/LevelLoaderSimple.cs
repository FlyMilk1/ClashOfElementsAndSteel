using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderSimple : MonoBehaviour
{
    
    public void LoadLevel(string name)
    {
        
        Scene temp = SceneManager.GetActiveScene();

        SceneManager.LoadScene(name);
        SceneManager.UnloadSceneAsync(temp);
    }
}
