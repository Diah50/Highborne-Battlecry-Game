/* UnitBase.cs - Highborne Universe
 * 
 * Creation Date: 18/09/2023
 * Authors: Archetype
 * Original: Archetype
 * 
 * Edited By: Archetype
 * 
 * Changes: 
 *      [18/09/2023] - Initial implementation (Archetype)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitBase : MonoBehaviour
{
    /// <summary>
    /// Unit sprite renderer
    /// </summary>
    public SpriteRenderer mySprite;

    /// <summary>
    /// Unit scriptable object
    /// </summary>
    public UnitBaseScOb scriptObj;

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

    public List<SpriteRenderer> colorSpritesSkin, colorSpritesOutfit;
    public SpriteRenderer iris, sclera, pupil, aura, direction;

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

    public void Initiate(UnitBaseScOb scriptableObject)
    {
        scriptObj = scriptableObject;
        mySprite.sprite = scriptableObject.sprites;

        healthBar = WorldCanvasManager.singleton.AskForHealthBar(gameObject);

        //Define variables
        healthBar.maxValue = scriptableObject.maxHealth;
        ChangeSlider();

        //Hide health bar
        healthBarCanvas.SetActive(false);
    }

    #region Health
    //Increase current health by amount
    public void AddToHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth > scriptObj.maxHealth) currentHealth = scriptObj.maxHealth;
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
