using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> listLevel;
    public static LevelManager instance;

    private int currentLevel;
    private GameObject currentMap;
    private void Awake()
    {
        instance = this; 
    }
    public void LoadLevel(int levelIndex)
    {
        Destroy(currentMap);
        if (levelIndex % 5 == 0)
        {
            currentLevel = 5;
        }
        else
        {
            currentLevel = levelIndex % 5;
        }
        currentMap = Instantiate(listLevel[currentLevel-1], Vector3.zero, Quaternion.identity);
    }
    public void LoadFirstLevel()
    {
        currentLevel = 1;
        currentMap =  Instantiate(listLevel[currentLevel - 1], Vector3.zero, Quaternion.identity);
    }
}
