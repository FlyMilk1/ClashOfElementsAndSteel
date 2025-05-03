using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Text.RegularExpressions;

public class LoadEnemyUnits : MonoBehaviour
{
    public List<unitPlace> units = new List<unitPlace>();
    string pathlevels = Application.dataPath + "/levels.txt";
    string pathicons = Application.dataPath + "/icons.txt";
    string assetBundlePath = Application.streamingAssetsPath + "/AssetBundles";
    AssetBundle assetBundleLevels;
    AssetBundle assetBundleIcons;
    AssetBundle assetBundleText;
    int LevelNum;
    
    // Start is called before the first frame update
    void Start()
    {
        
        LevelNum = int.Parse(Regex.Replace(SceneManager.GetActiveScene().name, "[^0-9]", ""));
        //if (!File.Exists(pathlevels))
        //{
        //    File.Create(pathlevels);
        //}
        assetBundleLevels = AssetBundle.LoadFromFile(assetBundlePath + "/knights");
        assetBundleIcons = AssetBundle.LoadFromFile(assetBundlePath + "/icons");
        assetBundleText = AssetBundle.LoadFromFile(assetBundlePath + "/text");
        LoadLevel(LevelNum);
        
        //foreach (unitPlace place in GameObject.Find("KnightSpawnAnchorEnemy").GetComponentsInChildren<unitPlace>(true))
        //{
        //    units.Add(place);
        //    Debug.Log("Unit 0: "+units[0].name);
        //}
        
        
        

        
    }

    
    void LoadLevel(int index)
    {
        
        string[] levels = assetBundleText.LoadAsset<TextAsset>("levels").text.Split("\n");
        string[] enemyUnits = levels[index].Split(" ");
        GameObject[] knights = assetBundleLevels.LoadAllAssets<GameObject>();
        for (int i=0; i<enemyUnits.Length; i++)
        {
           foreach(GameObject go in knights)
            {
                if(go.name == "Knight"+enemyUnits[i])
                {
                    units[i].AssignUnit(go);
                }
            }
            
            
        }

        string[] icons = assetBundleText.LoadAsset<TextAsset>("icons").text.Split("\n");
        GameObject[] given = assetBundleIcons.LoadAllAssets<GameObject>();
        string[] unitIcons = icons[index].Split(" ");
        int[] nums = new int[10];
        for (int i=0; i<unitIcons.Length; i++)
        {
            
            
            nums[i] = int.Parse(Regex.Replace(unitIcons[i], "[^0-9]", ""));
            unitIcons[i] = Regex.Replace(unitIcons[i], "[0-9]", "");

        }
        for(int i=0; i<unitIcons.Length; i++)
        {
            foreach(GameObject go in given)
            {
                
                if(go.name == "UnitIcon" + unitIcons[i])
                {
                    Debug.Log("UnitIcon" + unitIcons[i]);
                    Debug.Log(go.name);
                    var instance = Instantiate(go, GameObject.Find("Canvas").transform.Find("UnitDrag").transform);
                    instance.GetComponent<IconDragging>().Count = nums[i];
                }
            }
            
        }
    }
}
