using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnBrick : MonoBehaviour
{
    [SerializeField] private GameObject cube1, cube2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (cube1.activeSelf == true)
            {
                cube1.SetActive(false);
                cube2.SetActive(true);
                other.GetComponent<Player>().DeleteBrick();
            }
        }
    }
}
