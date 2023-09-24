/* UnitBase.cs - Highborne Universe
 * 
 * Creation Date: 18/09/2023
 * Authors: Archetype, C137
 * Original: Archetype
 * 
 * Edited By: Archetype, C137
 * 
 * Changes: 
 *      [18/09/2023] - Initial implementation (Archetype)
 *      [23/09/2023] - Finally finished implementing pathfinding + custom unit avoidance, finished adding notation to script (Archetype)
 *      [24/09/2023] - Code review (C137)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pathfinding;

public class UnitBase : MonoBehaviour
{
    /// <summary>
    /// The unit scriptable object containg all of its information
    /// </summary>
    public UnitInfo information;

    /// <summary>
    /// Unit current experience points
    /// </summary>
    public int currentEXP;

    /// <summary>
    /// Unit current level
    /// </summary>
    public int currentLevel;

    /// <summary>
    /// Unit current health
    /// </summary>
    public float currentHealth;

    /// <summary>
    /// Sprite Renderers that can be recoulored for team colors and character body colors
    /// </summary>
    public List<SpriteRenderer> colorSpritesSkin, colorSpritesOutfit;
    public SpriteRenderer iris, sclera, pupil, aura, direction;

    /// <summary>
    /// A* destination setter script
    /// </summary>
    public AIDestinationSetter aiDestinationSetter;

    /// <summary>
    /// A* path script
    /// </summary>
    public AIPath aiPath;

    /// <summary>
    /// game object that links to aiDestinationSetter, move this to change target destination
    /// </summary>
    public GameObject destination;

    /// <summary>
    /// circle under unit that is enabled when it is selected
    /// </summary>
    public GameObject basicSelector;

    /// <summary>
    /// unit circle colider
    /// </summary>
    public CircleCollider2D colider;

    /// <summary>
    /// previous pos to mark speed
    /// </summary>
    Vector3 previosPos;

    /// <summary>
    /// weather the unit is moving or not
    /// </summary>
    bool moving;

    /// <summary>
    /// unit velocity using the above two variables
    /// </summary>
    float velocity;

    /// <summary>
    /// unit rigidbody2D
    /// </summary>
    public Rigidbody2D rb2D;

    #region Health
    /// <summary>
    /// Health bar canvas that can be enabled or disabled
    /// </summary>
    public GameObject healthBarCanvas;

    /// <summary>
    /// Health bar slider
    /// </summary>
    public Slider healthBar;

    /// <summary>
    /// Healthbar image that represents amount of health points
    /// </summary>
    public Image healthBarFillImage;
    #endregion

    private void Start()
    {
        //the unit prefab comes in with its own destination setter when its instantiated but it is unparented so it doesn't move with the unit
        destination.transform.parent = null;

        //initiate the previous pos for velocity detection
        previosPos = transform.position;

        //disable pathfinding while the unit isn't moving
        aiPath.canSearch = false;
        moving = false;

        //set unit to unit unpassable layer so it can be considered a pathfinding obstacle while idle
        gameObject.layer = 12;
        AstarPath.active.UpdateGraphs(colider.bounds);
    }

    //when this is instantiated it is then ran with the Initiate function with the appropriate scriptable object
    public void Initiate(UnitInfo scriptableObject)
    {
        information = scriptableObject;

        //Define variables
        aiPath.maxSpeed = scriptableObject.movmentSpeed;

        healthBar.maxValue = scriptableObject.maxHealth;
        ChangeSlider();

        //Hide health bar
        healthBarCanvas.SetActive(false);
    }

    //Accessed from unit selection manager to signal when a unit is under selection
    public void OnDeselect()
    {
        basicSelector.SetActive(false);
    }

    public void OnSelect()
    {
        basicSelector.SetActive(true);
    }

    //Accessed from unit selection manager to receive new target destination
    public void OnSelectDestination(Vector3 position)
    {
        aiDestinationSetter.target.transform.position = new Vector3(position.x, position.y, 0);
    }

    //things to do when the unit starts moving
    public void OnStartMoving()
    {
        //activate pathfinding
        aiPath.canSearch = true;
    }

    private void OnDestroy()
    {
        //avoid orphaned game objects
        Destroy(destination);
    }

    private void FixedUpdate()
    {
        //calculate current velocity
        velocity = Vector3.Distance(transform.position, previosPos);
        previosPos = transform.position;
    }

    private void Update()
    {
        //transform.GetChild(0).gameObject.layer = 2;

        //do something when the unit stops moving
        if (velocity > 0f) moving = true;
        if (velocity == 0f && moving)
        {
            aiPath.canSearch = false;
            moving = false;
            gameObject.layer = 12;
            AstarPath.active.UpdateGraphs(colider.bounds);
        }
    }

    private void OnMouseDown()
    {
        //so something when this unit is clicked on directly
        SelectionManager.singleton.WhenMouseDown();
        SelectionManager.singleton.AddToSelection(gameObject);
        SelectionManager.singleton.outsideClick = true;
    }

    #region Health
    //Increase current health by amount
    public void AddToHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth > information.maxHealth) currentHealth = information.maxHealth;
        ChangeSlider();
    }

    //Decrease current health by amount
    public void ReduceHealth(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0) Destroy(gameObject);
        ChangeSlider();
    }

    //Update health bar fill amount, color and text
    void ChangeSlider()
    {
        if (healthBar.IsActive())
        {
            healthBar.value = currentHealth;
            healthBarFillImage.color = Color.Lerp(Color.red, Color.green, healthBar.value / 100);
        }
    }

    private void OnMouseEnter()
    {
        //Display health bar
        healthBarCanvas.SetActive(true);
        ChangeSlider();
    }

    private void OnMouseExit()
    {
        //Hide health bar
        healthBarCanvas.SetActive(false);
    }
    #endregion
}
