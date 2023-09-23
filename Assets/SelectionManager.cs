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
 *      [23/09/2023] - Finally finished implementing pathfinding + custom unit avoidance, finished adding notation to script
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
    public List<GameObject> controlled = new List<GameObject>();

    /// <summary>
    /// temporary rigidbodies added and removed from the destination to keep them form overlapping
    /// </summary>
    List<Rigidbody2D> rigidbodies = new List<Rigidbody2D>();

    /// <summary>
    /// when a unit is clicked on directly
    /// </summary>
    public bool outsideClick;

    private void Start()
    {
        selectionImg.color = selectColor;
        selectionBox.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !outsideClick)
        {
            WhenMouseDown();
        }
        if (Input.GetMouseButton(0))
        {
            WhileMouseDown();
        }
        if (Input.GetMouseButtonUp(0))
        {
            WhenMouseUp();
        }
        if (Input.GetMouseButtonDown(1) && controlled.Count > 0)
        {
            WhenRightMouseDown();
        }
    }

    void WhileMouseDown()
    {
        endPos = Input.mousePosition;
        endPos = Camera.main.ScreenToWorldPoint(endPos);
        DrawSelection();
    }

    public void WhenMouseDown()
    {
        controlled = new List<GameObject>();
        startPos = Input.mousePosition;
        startPos = Camera.main.ScreenToWorldPoint(startPos);
        ClearSelection();
    }

    void WhenMouseUp()
    {
        outsideClick = false;

        foreach (GameObject obj in selected)
        {
            controlled.Add(obj);
        }

        selectionBox.gameObject.SetActive(false);
        selected.Clear();

        foreach (GameObject obj in controlled)
        {
            AddToSelection(obj);
        }
    }

    //issue move commands
    void WhenRightMouseDown()
    {
        Vector3 middlePos = new Vector3();

        foreach (GameObject obj in controlled)
        {
            middlePos += obj.transform.position;
        }

        middlePos = middlePos / controlled.Count;

        foreach (GameObject obj in controlled)
        {
            var x = obj.GetComponent<UnitBase>();
            x.gameObject.layer = 13;
            AstarPath.active.UpdateGraphs(x.colider.bounds);

            if (x.destination.GetComponent<Rigidbody2D>() != null)
            {
                CancelInvoke("KillRigidbodies");
            }
            else
            {
                Rigidbody2D r = x.destination.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
                r.gravityScale = 0;
                rigidbodies.Add(r);
            }
            
            x.OnStartMoving();
            x.OnSelectDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition)
                + (obj.transform.position - middlePos));

            Invoke("KillRigidbodies", 3);
        }
    }

    //cleanup
    void KillRigidbodies()
    {
        foreach(Rigidbody2D bod in rigidbodies)
        {
            Destroy(bod);
        }
    }

    public void AddToSelection(GameObject unit)
    {
        selected.Add(unit);
        unit.SendMessage("OnSelect");
    }

    public void RemoveFromSelection(GameObject unit)
    {
        selected.Remove(unit);
        unit.SendMessage("OnDeselect");
    }

    public void ClearSelection()
    {
        foreach (GameObject obj in selected)
        {
            obj.SendMessage("OnDeselect");
        }

        selected.Clear();
    }

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