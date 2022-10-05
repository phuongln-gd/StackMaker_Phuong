using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBrick : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameObject.SetActive(false);
            other.GetComponent<Player>().AddFirstBrick(); 
        }
    }
}
