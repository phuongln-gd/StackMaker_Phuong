using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBrick : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Finish level");
            other.GetComponent<Player>().currentLevel += 1;
            int levelIndex = other.GetComponent<Player>().currentLevel;
            LevelManager.instance.LoadLevel(levelIndex);
            UIManager.instance.SetTextLevel(levelIndex);
            other.GetComponent<Player>().PassLevel();
        }
    }
}
