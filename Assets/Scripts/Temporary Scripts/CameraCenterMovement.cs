/*
 * CameraCenterMovement.cs - Highborne Universe
 * 
 * Creation Date: 29/07/2023
 * Authors: Hellhound, Archetype, C137
 * Original: Hellhound
 * 
 * Changes: 
 *  [29/07/2023] - Replaced "CameraMovement" script with cinemachine and added camera confines (Archetype)
 *  [30/07/2023] - Added edge scrolling + unit follow (Archetype)
 *  [01/08/2023] - Improve meta info + added documentation + code review (C137)
 *  [01/08/2023] - Removed namespace for Singleton, small script update for organization, removed debug assets (Archetype)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraCenterMovement : Singleton<CameraCenterMovement>
{
    /// <summary>
    /// The movement speed of the camera
    /// </summary>
    public float moveSpeed;
    
    /// <summary>
    /// The rigidbody of the camera
    /// </summary>
    public Rigidbody2D rb2d;

    /// <summary>
    /// The input direction 
    /// </summary>
    public Vector2 moveInput;

    /// <summary>
    /// Reference to the actual camera
    /// </summary>
    public GameObject cam;

    /// <summary>
    /// Reference to the cinemachine camera
    /// </summary>
    public CinemachineVirtualCamera virtualCamera;

    /// <summary>
    /// Edge scrolling booleans
    /// </summary>
    bool pushedUp, pushedDown, pushedLeft, pushedRight;

    void Update()
    {
        //Raw axis inputs
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        //Edge scrolling inputs
        EdgeScrollingInput();

        //Any cam input should make the cam stop following a unit
        if ((moveInput.x != 0 || moveInput.y != 0 || pushedUp || pushedDown || pushedRight || pushedLeft) && virtualCamera.Follow != transform)
        {
            transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.z);
            virtualCamera.Follow = transform;
        }
    }

    private void FixedUpdate()
    {
        //Uses inputs from Update to move
        MoveCamera();
    }

    void MoveCamera()
    {
        rb2d.velocity = moveInput * moveSpeed;

        if (transform.position.x > cam.transform.position.x + 5 || transform.position.x < cam.transform.position.x - 5)
        {
            transform.position = new Vector3(cam.transform.position.x, transform.position.y, transform.position.z);
        }

        if (transform.position.y > cam.transform.position.y + 5 || transform.position.y < cam.transform.position.y - 5)
        {
            transform.position = new Vector3(transform.position.x, cam.transform.position.y, transform.position.z);
        }

        //Takes which edge the mouse is pushing against, sums it up in case its a corner and then translates in that direction
        Vector3 finalDirection = new Vector3(0, 0, 0);
        if (pushedUp) finalDirection += Vector3.up;
        if (pushedDown) finalDirection -= Vector3.up;
        if (pushedRight) finalDirection += Vector3.right;
        if (pushedLeft) finalDirection -= Vector3.right;
        finalDirection.Normalize();
        transform.Translate(finalDirection * moveSpeed * Time.deltaTime);
    }

    //add this to all units:
    /*
    int numOfClicks;
    private void OnMouseDown()
    {
        numOfClicks++;
        if (numOfClicks >= 2) CameraCenterMovement.Instance.TakeFocus(transform);
        Invoke("giveUpOnDoubleClick", .2f);
    }
    void giveUpOnDoubleClick()
    {
        numOfClicks = 0;
    }
    */
    public void TakeFocus(Transform Ttransform)
    {
        virtualCamera.Follow = Ttransform;
        transform.position = new Vector3(0, 0, 0);
    }

    void EdgeScrollingInput()
    {
        float mousePosX = Input.mousePosition.x;
        float mousePosY = Input.mousePosition.y;
        int scrollDistance = 30;


        pushedLeft = mousePosX < scrollDistance;
        pushedRight = mousePosX >= Screen.width - scrollDistance;
        pushedDown = mousePosY < scrollDistance;
        pushedUp = mousePosY >= Screen.height - scrollDistance;
    }
}