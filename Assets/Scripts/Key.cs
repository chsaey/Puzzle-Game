using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        GetComponentInParent<GameManager>().keys--;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        GetComponentInParent<GameManager>().keys++;
    }

}
