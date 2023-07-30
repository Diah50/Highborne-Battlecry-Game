/*
 * CameraControler
 * 
 * Initial setup done by Hellhound. Confining camera to bounds, adding border push and double clicking to follow units
 * 
 * Authors: Hellhound, Archetype
 * 
 * Changes: 
 *  [07/29/2023] -Archetype- replaced "CameraMovement" script with cinemachine and added camera confines
 *  [07/30/2023] -Archetype- added edge scroling, also added unit follow (needs to be tested on real units)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Archetype;
using Cinemachine;

public class CameraCenterMovement : Singleton<CameraCenterMovement>
{
    public float moveSpeed;
    public Rigidbody2D rb2d;
    public Vector2 moveInput;
    public GameObject cam;
    public CinemachineVirtualCamera virtualCamera;

    bool pushedUp, pushedDown, pushedLeft, pushedRight;

    //controlls position of camera target (as seen in the virtual camera follow)
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        EdgeScroling();

        //any cam input should make the cam stop following a unit
        if ((moveInput.x != 0 || moveInput.y != 0 || pushedUp || pushedDown || pushedRight || pushedLeft) && virtualCamera.Follow != transform)
        {
            transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.z);
            virtualCamera.Follow = transform;
        }
    }

    private void FixedUpdate()
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

        //takes wich edge the mouse is pushing against, sums it up in case its a corner and then translates in that direction
        Vector3 finalDirection = new Vector3 (0,0,0);
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
        Debug.Log("doubleclick");
        virtualCamera.Follow = Ttransform;
        transform.position = new Vector3(0, 0, 0);
    }

    void EdgeScroling()
    {
        float mousePosX = Input.mousePosition.x;
        float mousePosY = Input.mousePosition.y;
        int scrollDistance = 30;

        if (mousePosX < scrollDistance)
        {
            pushedLeft = true;
        }
        else
        {
            pushedLeft = false;
        }

        if (mousePosX >= Screen.width - scrollDistance)
        {
            pushedRight = true;
        }
        else
        {
            pushedRight = false;
        }

        if (mousePosY < scrollDistance)
        {
            pushedDown = true;
        }
        else
        {
            pushedDown = false;
        }

        if (mousePosY >= Screen.height - scrollDistance)
        {
            pushedUp = true;
        }
        else
        {
            pushedUp = false;
        }
    }
}