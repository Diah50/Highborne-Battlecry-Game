/*
 * CameraControler
 * 
 * Initial setup done by Hellhound. Confining camera to bounds, adding border push and double clicking to follow units
 * 
 * Authors: Hellhound, Archetype
 * 
 * Changes: 
 *  [07/29/2023] replaced "CameraMovement" script with cinemachine
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenterMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb2d;
    public Vector2 moveInput;
    public GameObject cam;

    //controlls position of camera target (as seen in the virtual camera follow)
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
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
    }
}
