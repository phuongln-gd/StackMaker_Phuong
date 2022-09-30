using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnBrick : MonoBehaviour
{
    [SerializeField] GameObject cube1, cube2;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            cube1.SetActive(false);
            cube2.SetActive(true);
        }
    }
}
