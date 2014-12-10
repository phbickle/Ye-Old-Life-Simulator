﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Canvas))]
public class HUDScript : MonoBehaviour 
{
    public Canvas m_PlayerHUD; //Player's HUD canvas
    public GameObject m_Stats; //The stat's panel
    public GameObject m_Goals; //The goals panel
    public PlayerData m_PlayerData; //The player's data

    private bool statsActive_ = false; //This bool determines whether or not the the stats window is open
    private float timer_; //Turn timer
    private Slider[] sliderArray_; //Array containing the HUD sliders
    private Slider[] objSliderArray_; //Array of objective sliders
    private Slider timeSlider_; //Slider for the time left in your turn
    private Slider hungerSlider_; //Slider for the amount of player's hunger
    private Slider repObjSlider_; //Slider for the reputation objective
    private Slider currObjSlider_; //Slider for the currency/shillings objective
    private Slider happyObjSlider_; //Slider for the happiness objective
    private Text[] textArray_; //Array of text for the stats screen
    private List<float> playerStats_ = new List<float>(); //List of the player stats

    //All values in the following temp code should be set from the player rather than in here
    //This will be all plugged in once the player is in a state where this can happen
    //-------------TEMP CODE-------------
public float m_ReputationObjective;
public float m_CurrencyObjective;
public float m_HappinessObjective;
    //-------------END TEMP CODE------------- 

	// Use this for initialization
	void Start () 
    {
        //Initialize and set up the slider arrays
        sliderArray_ = m_PlayerHUD.GetComponentsInChildren<Slider>();
        SetUpHUDSliders();

        objSliderArray_ = m_Goals.GetComponentsInChildren<Slider>();
        SetUpObjectiveSliders();

        //Load the player stats list with the player's stats
        //playerStats_.Add(m_PlayerData.m_Home.m_Rating); //Need to figure out how to work around the enumerator
        playerStats_.Add(m_PlayerData.m_HungerMeter);
        playerStats_.Add(m_PlayerData.m_Reputation);
        playerStats_.Add(m_PlayerData.m_Shillings);
        playerStats_.Add(m_PlayerData.m_Happiness);
        
        //Initialize the text array
        textArray_ = m_Stats.GetComponentsInChildren<Text>();
        //Turn off the stats and goal panels after initializing all stats
        m_Stats.SetActive(false);
        m_Goals.SetActive(false);
        //Set the current turn's timer
        timer_ = m_PlayerData.m_MaxTime;
	}

    //This function initializes the player HUD sliders, and then sets their min, max and current values
    void SetUpHUDSliders()
    {
        timeSlider_ = sliderArray_[0];
        hungerSlider_ = sliderArray_[1];

        timeSlider_.maxValue = m_PlayerData.m_MaxTime;
        timeSlider_.minValue = 0.0f;
        timeSlider_.value = timer_;

        hungerSlider_.maxValue = m_PlayerData.m_MaxHunger;
        hungerSlider_.minValue = 0.0f;
        hungerSlider_.value = 0.0f;
    }

    //This function initializes the objective sliders in the stats menu, then sets their min, max and current values
    void SetUpObjectiveSliders()
    {
        repObjSlider_ = objSliderArray_[0];
        currObjSlider_ = objSliderArray_[1];
        happyObjSlider_ = objSliderArray_[2];

        repObjSlider_.maxValue = m_ReputationObjective;
        repObjSlider_.minValue = 0.0f;

        currObjSlider_.maxValue = m_CurrencyObjective;
        currObjSlider_.minValue = 0.0f;

        happyObjSlider_.maxValue = m_HappinessObjective;
        happyObjSlider_.minValue = 0.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer_--;
        timeSlider_.value = timer_;
        hungerSlider_.value = m_PlayerData.m_HungerMeter;
        if (timer_ <= 0.0f)
        {
            m_PlayerData.m_HungerMeter += 10.0f;
            timer_ = m_PlayerData.m_MaxTime;
        }

        if (m_PlayerData.m_HungerMeter >= m_PlayerData.m_MaxHunger)
        {
            m_PlayerData.m_HungerMeter = m_PlayerData.m_MaxHunger;
        }
	}

    void FixedUpdate()
    {
        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            m_CurrHunger -= 10.0f;
        }*/

        if(Input.GetKeyDown(KeyCode.E))
        {
            statsActive_ = !statsActive_;
            m_Stats.SetActive(statsActive_);
            m_Goals.SetActive(statsActive_);
            PopulateStats();
            UpdateSliders();
        }
    }

    //This function gets the latest stats when the player opens the stats menu
    void PopulateStats()
    {
        //Clear the list and re-add the variables to make sure the information is completely up to date
        playerStats_.Clear();
        //playerStats_.Add(m_PlayerData.m_Home.m_Rating);
        playerStats_.Add(m_PlayerData.m_HungerMeter);
        playerStats_.Add(m_PlayerData.m_Reputation);
        playerStats_.Add(m_PlayerData.m_Shillings);
        playerStats_.Add(m_PlayerData.m_Happiness);

        //Loop through the pair of arrays to get the appropriate values next to the proper text
        for (int i = 0; i < textArray_.Length - 1; ++i)
        {
            textArray_[i].text = textArray_[i].name + ": " + playerStats_[i].ToString();
        }
    }

    //This function sets the objective sliders to their current values when the player opens the stats menu
    void UpdateSliders()
    {
        repObjSlider_.value = m_PlayerData.m_Reputation;
        currObjSlider_.value = m_PlayerData.m_Shillings;
        happyObjSlider_.value = m_PlayerData.m_Happiness;
    }
}
