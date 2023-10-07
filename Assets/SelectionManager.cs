/* UnitBase.cs - Highborne Universe
 * 
 * Creation Date: 19/09/2023
 * Authors: Archetype
 * Original: Archetype
 * 
 * Edited By: Archetype
 * 
 * Changes: 
 *      [19/09/2023] - Initial implementation (Archetype)
 *      [23/09/2023] - Finally finished implementing pathfinding + custom unit avoidance, finished adding notation to script (Archetype)
 *      [24/09/2023] - Added aditional documentation (Archetype)
 *      [06/10/2023] - Finished implementing formation rotation and visible pathing, added clump command mode that makes it that units clump toguether rahter than preserving formation (Archetype)
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : Singleton<SelectionManager>
{
    /// <summary>
    /// color of selection square
    /// </summary>
    [SerializeField] Color selectColor = new Color(0, 1, 0, 0.3f);

    /// <summary>
    /// details from selection box
    /// </summary>
    [SerializeField] RectTransform selectionBox;
    [SerializeField] BoxCollider2D selectionBoxCollider;
    [SerializeField] Image selectionImg;

    /// <summary>
    /// the two tips of the selection box
    /// </summary>
    public Vector2 startPos, endPos;

    /// <summary>
    /// list of units that are selected
    /// </summary>
    List<GameObject> selected = new List<GameObject>();

    /// <summary>
    /// list of units that are being commanded
    /// </summary>
    List<UnitBase> controlledScriptList = new List<UnitBase>();

    /// <summary>
    /// temporary rigidbodies added and removed from the destination to keep them form overlapping
    /// </summary>
    List<Rigidbody2D> rigidbodies = new List<Rigidbody2D>();

    /// <summary>
    /// when a unit is clicked on directly
    /// </summary>
    public bool outsideClick;

    /// <summary>
    /// formation rotation
    /// </summary>
    Vector3 dirDragPos;

    /// <summary>
    /// time till drag rotation for formations is registered
    /// </summary>
    float timeLeft = .5f;

    /// <summary>
    /// middle of a formation
    /// </summary>
    Vector3 middlePos = new Vector3();

    /// <summary>
    /// rotation paramaters for formation rotation
    /// </summary>
    Quaternion rotateDirection, startRotation;

    /// <summary>
    /// toggle clump command mode
    /// </summary>
    bool concentratePositions;

    private void Start()
    {
        startRotation = transform.rotation;

        //color of selection box
        selectionImg.color = selectColor;
        selectionBox.gameObject.SetActive(false);
    }

    private void Update()
    {
        KeyBoardShortcuts();

        if (Input.GetMouseButtonDown(0) && !outsideClick)
        {
            //when left mouse button down
            WhenMouseDown();
        }
        if (Input.GetMouseButton(0))
        {
            //while left mouse button held down
            WhileMouseDown();
        }
        if (Input.GetMouseButtonUp(0))
        {
            //when left mouse button up
            WhenMouseUp();
        }
        if (Input.GetMouseButtonDown(1))
        {
            WhenRightButtonDown();
        }
        if (Input.GetMouseButton(1))
        {
            //while right mouse button held down
            WhileRightMouse();
        }
        if (Input.GetMouseButtonUp(1) && controlledScriptList.Count > 0)
        {
            //when right mouse button up
            WhenRightMouseUp();
        }
    }

    void KeyBoardShortcuts()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            concentratePositions = !concentratePositions;
        }
    }

    void ResetShortcuts()
    {
        concentratePositions = false;
    }

    void WhileMouseDown()
    {
        //drag box selector end pos as mouse moves
        endPos = Input.mousePosition;
        endPos = Camera.main.ScreenToWorldPoint(endPos);

        //apply box size and positions
        DrawSelection();
    }

    public void WhenMouseDown()
    {
        //set box selector starting point
        controlledScriptList = new List<UnitBase>();
        startPos = Input.mousePosition;
        startPos = Camera.main.ScreenToWorldPoint(startPos);

        //clear anything that was previously selected from list unless leftShift is down
        if(!Input.GetKey(KeyCode.LeftShift)) ClearSelection();
    }

    void WhenMouseUp()
    {
        outsideClick = false;

        if (selected.Count <= 0) ResetShortcuts();

        //pass selected objects to controlled object list
        foreach (GameObject obj in selected)
        {
            controlledScriptList.Add(obj.GetComponent<UnitBase>());
        }

        selectionBox.gameObject.SetActive(false);
        selected.Clear();

        foreach (UnitBase obj in controlledScriptList)
        {
            AddToSelection(obj);
        }
    }

    void WhenRightButtonDown()
    {
        timeLeft = .5f;

        middlePos = new Vector3();
        //calculate middle of group of units
        foreach (UnitBase obj in controlledScriptList)
        {
            obj.destination.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, 0);

            middlePos += obj.destination.transform.position;
        }

        middlePos = middlePos / controlledScriptList.Count;
        transform.position = middlePos;

        float x = Mathf.Ceil(Mathf.Sqrt(controlledScriptList.Count));

        foreach (UnitBase obj in controlledScriptList)
        {
            obj.destination.transform.parent = transform;
        }

        //clump target positions if concentratePositions is enabled
        if (concentratePositions)
        {
            int y = 0;
            for (int i = 0; i < x; i++)
            {
                if (y >= controlledScriptList.Count -1) break;
                for (int ii = 0; ii < x; ii++)
                {
                    if (controlledScriptList[y] != null)
                    {
                        controlledScriptList[y].destination.transform.position =
                            new Vector3(transform.position.x - (x / 2) + ii, transform.position.y - (x / 2) + i, 0);
                    }
                    y++;
                    if (y >= controlledScriptList.Count) break;
                }
            }
        }

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        dirDragPos = transform.position;
    }

    void WhileRightMouse()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - dirDragPos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rotateDirection = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotateDirection;

            foreach (UnitBase obj in controlledScriptList)
            { 
                obj.destination.transform.rotation = new Quaternion(0,0,transform.rotation.z *-1,0);
            }
        }
    }

    //issue move commands
    void WhenRightMouseUp()
    {
        foreach (UnitBase obj in controlledScriptList)
        {
            //place units in passable layer and update A* map
            obj.gameObject.layer = 13;
            AstarPath.active.UpdateGraphs(obj.colider.bounds);

            //add rigidbody to destination to avoid overlapping end positions
            if (obj.destination.GetComponent<Rigidbody2D>() != null)
            {
                CancelInvoke("KillRigidbodies");
            }
            else
            {
                Rigidbody2D r = obj.destination.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
                r.gravityScale = 0;
                rigidbodies.Add(r);
            }

            obj.destination.transform.parent = null;
            obj.destination.transform.rotation = startRotation;

            //pass info to unit
            obj.OnStartMoving();
            obj.OnSelectDestination(obj.destination.transform.position);
        }

        transform.rotation = startRotation;
        transform.position = Vector3.zero;

        //give the end position rigidbodies some time to push themselves apart before deleting them
        Invoke("KillRigidbodies", 3);
    }

    //cleanup
    void KillRigidbodies()
    {
        foreach(Rigidbody2D bod in rigidbodies)
        {
            Destroy(bod);
        }

        rigidbodies.Clear();
    }

    //usually referenced from script attached to box selector
    public void AddToSelection(GameObject unit)
    {
        selected.Add(unit);
        unit.SendMessage("OnSelect");
    }
    
    public void AddToSelection(UnitBase unit)
    {
        selected.Add(unit.gameObject);
        unit.SendMessage("OnSelect");
    }

    public void RemoveFromSelection(GameObject unit)
    {
        if (!controlledScriptList.Contains(unit.GetComponent<UnitBase>()))
        {
            selected.Remove(unit);
            unit.SendMessage("OnDeselect");
        }
    }

    public void ClearSelection()
    {
        foreach (GameObject obj in selected)
        {
            obj.SendMessage("OnDeselect");
        }

        selected.Clear();
    }

    //apply size and position to selection box
    void DrawSelection()
    {
        if(!selectionBox.gameObject.activeSelf)
        {
            selectionBox.gameObject.SetActive(true);
        }

        float width = endPos.x - startPos.x;
        float height = endPos.y - startPos.y;

        Vector2 size = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.sizeDelta = size;
        selectionBoxCollider.size = size;

        Vector2 center = startPos + new Vector2(width / 2, height / 2);
        selectionBox.position = center;
    }
}