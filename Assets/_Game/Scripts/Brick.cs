using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private GameObject addBrickPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }

}
