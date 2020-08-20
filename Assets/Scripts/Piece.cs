using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameObject.ReferenceEquals(GetComponentInParent<RayCastPieceManager>().piece, gameObject)){
            transform.parent.GetComponent<RayCastPieceManager>().DetectCollision();
        }
            
    }
}
