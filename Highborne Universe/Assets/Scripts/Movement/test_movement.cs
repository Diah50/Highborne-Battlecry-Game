using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class test_movement : MonoBehaviour
{
    //Movement speed when traveling to a new destination
    [SerializeField] private float speed;
    //Size of the buffer between the player and an obstacle that it encounters
    [SerializeField] private float buffer;
    
    //The active destination coordinates that the player will constantly try to move to
    [NonSerialized] private Vector3 destination;

    //Layers that will be used in the Raycast to test for obstacles
    [SerializeField] private LayerMask obstacleLayers;

    // Start is called before the first frame update
    void Start()
    {
        //Set the destination to where the player starts out
        destination = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Constantly checks for player input
        getInput();

        //Constantly moves the player to the assigned "destination" coordinates
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, destination, speed);
        //Draws a line between the current player position and the destination coordinates
        Debug.DrawLine(gameObject.transform.position, destination, Color.blue);
    }

    //Checks for mouse input from the user
    void getInput()
    {
        //If the left mouse button is pressed down
        if (Input.GetMouseButtonDown(0))
        {
            changePosition();
        }
    }

    //Checks for any obstacles between the object's position and the new destination. Changes the destination coords accordingly
    void changePosition()
    {
        //Sets temporary destination coordinates to where the player clicked on the screen
        Vector3 tempDest = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, gameObject.transform.position.z);

        //Creates a raycasy (esentially a straight line that checks for collissions) between the player's current position and the temp destination
        RaycastHit2D hit = Physics2D.Linecast(gameObject.transform.position, tempDest, 1 << LayerMask.NameToLayer("Obstacle"));

        //If there is an obstacle in the way...
        if (hit.collider != null)
        {
            //Create a buffer between the hitpoint of the Raycast and the obstacle, so the player does not end up inside of the obstacle
            Vector3 vectorBuffer = -1.0f * buffer * (tempDest - gameObject.transform.position).normalized;
            //Set the active destination coordiantes to the Raycast hitpoint, taking into account the buffer created above
            destination = new Vector3(hit.point.x, hit.point.y, gameObject.transform.position.z) + vectorBuffer;
        }
        //If not...
        else
        {
            //Set the active destination coordinates to the temporary ones
            destination = tempDest;
        }
    }
}
