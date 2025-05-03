using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponentInChildren<TMP_Text>().text = Regex.Replace(gameObject.name, "[^0-9]", "");
    }
    public void LoadLevel()
    {
        Scene temp = SceneManager.GetActiveScene();
        
        SceneManager.LoadScene(gameObject.name);
        SceneManager.UnloadSceneAsync(temp);
    }
}
