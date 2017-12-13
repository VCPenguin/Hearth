using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Variables
    //Singlton Instance
    public static PlayerController instance;

    //Variables
    public float strengthBoostTimer;
    public float baseStrength;
    public float strength;
    public float glueBerries;
    public float strengthBoost;

    public bool inCave;

    public float maxHealth;
    public float health;

    public Image PlayerDamageDisplay;

    // -80 -2.2

    //Module Controller Objects
    public InputController inputController;
    public InteractionController interactionController;
    public FirstPersonController firstPersonController;
    public DayAndNightControl dayNightControl;
    public SceneController sceneController;

    public Campfire campFire;

    public float sleepDuration;

    public Fader BlackFader;

    #endregion

    private void Awake()
    {
        //Singlton Initialization
        instance = this;

        //Setting up control moduel references
        inputController = GetComponent<InputController>();
        interactionController = GetComponent<InteractionController>();
        firstPersonController = GetComponent<FirstPersonController>();
    }

    void Start()
    {
        

        health = maxHealth;

        //Setting strength to base default
        strength = baseStrength;
    }

    // Update is called once per frame
    void Update()
    {
        //If you have a strength boost, run the update code
        if (strengthBoostTimer > 0)
            UpdateStrengthBoost();

        //Adjust player health visual
        Color tempColor = PlayerDamageDisplay.color;
        tempColor.a = Mathf.Lerp(1, 0, health / maxHealth);
        PlayerDamageDisplay.color = tempColor;
    }

    
    public void TakeDamage(float _damage)
    {
        health -= _damage;

        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        sceneController.ActivateDeadMenu();
    }

    public void BoostStrength(float _boostAmount, float _boostTime)
    {
        strength += _boostAmount;
        strengthBoostTimer = _boostTime;
    }

    void UpdateStrengthBoost()
    {
        strengthBoostTimer -= Time.deltaTime;

        if (strengthBoostTimer <= 0)
        {
            strength = baseStrength;
            strengthBoostTimer = 0;
        }
    }

    public IEnumerator Sleep()
    {
        
        yield return null;
    }



}
