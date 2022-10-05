using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] Text textLevel;
    public void SetTextLevel(int level)
    {
        textLevel.text = "Level " + level;
    }
    
}
