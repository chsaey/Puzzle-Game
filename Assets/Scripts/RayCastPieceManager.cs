using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastPieceManager : MonoBehaviour
{
    public static RayCastPieceManager pm;
    public bool isMoving, hasPiece, left, right, up, down;
    public float velocity;
    public GameObject piece;
    public Vector3 startPos, endPos, rayStart;
    public LayerMask ignore;
    RaycastHit2D[] ray;

    //Resets all variables, stopping movement and allowing a new piece to move. Collision is detected in the piece script attached to a piece, which will call this method.
    public void DetectCollision()
    {
        isMoving = hasPiece = left = right = up = down = false;
        startPos = endPos = rayStart = Vector3.zero;
        piece.transform.position = new Vector3(Mathf.Round(piece.transform.position.x), Mathf.Round(piece.transform.position.y), 0f);
        piece.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        piece = null;
        ray = new RaycastHit2D[0];

    }
    public void Start()
    {
        Time.timeScale = 1f;
        isMoving = hasPiece = left = right = up = down = false;
        startPos = endPos = rayStart = Vector3.zero;
        piece = null;
        ray = new RaycastHit2D[0];
    }


    // Update is called once per frame
    void Update()
    {
        // Clicking on game object will put that into piece variable
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Cast a ray to detect an object. (starting position, Direction), Vector2.zero shoots ray down the Z axis
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            startPos = Input.mousePosition;
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Piece"))
                {

                    startPos = Input.mousePosition;
                    hasPiece = true;
                    piece = hit.collider.gameObject;
                    rayStart = piece.transform.position;
                    piece.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                }

            }
        }
        if (Input.GetMouseButtonUp(0) && !isMoving && hasPiece)
        {
            //endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPos = Input.mousePosition;
            // Get direction. By subtracting the the last mouse position from the starting mouse position, we can find out the direction with the highest magnitude.
            Vector3 direction = endPos - startPos;
            float x = Mathf.Abs(direction.x);
            float y = Mathf.Abs(direction.y);
            //If x > y, is this means we moved horizontally from our starting position more than vertically. We move the peice horizontally
            if (x > y)
            {
                if (direction.x > 0)
                {
                    right = true;
                    ray = Physics2D.RaycastAll(rayStart, Vector2.right,50f, ~ignore);

                }
                else
                {
                    left = true;
                    ray = Physics2D.RaycastAll(rayStart, Vector2.left, 50f, ~ignore);

                }
            }
            //Else vertically
            else
            {
                if (direction.y > 0)
                {
                    up = true;
                    ray = Physics2D.RaycastAll(rayStart, Vector2.up, 50f, ~ignore);
                }
                else
                {
                    down = true;
                    ray = Physics2D.RaycastAll(rayStart, Vector2.down, 50f, ~ignore);
                }
            }
            //chages the state to allow movement
            isMoving = true;
        }

        if (isMoving)
        {
            if (ray[1].distance > 1f)
            {
                if (right) { piece.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, 0); }
                else if (left) { piece.GetComponent<Rigidbody2D>().velocity = new Vector2(-velocity, 0); }
                else if (up) { piece.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocity); }
                else if (down) { piece.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -velocity); }
            }
            else { DetectCollision(); }
        }
    }

    public void ResetScene()
    {
       
    }

}



