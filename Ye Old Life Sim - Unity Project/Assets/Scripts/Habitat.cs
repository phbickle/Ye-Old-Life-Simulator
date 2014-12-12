﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Habitat : MonoBehaviour 
{
    public float m_Rent = 0.0f;

    void Start()
    {
        CalculateHomePenalty();
    }
    public enum BuildingRating
    {
        NOSTAR = 0,
        ONESTAR = 1,
        TWOSTAR = 2,
        THREESTAR = 3,
        FOURSTAR = 4,
        FIVESTAR = 5
    };
    public BuildingRating m_Rating = BuildingRating.NOSTAR;

    public float CalculateHomePenalty()
    {
        switch (m_Rating)
        {
            case BuildingRating.NOSTAR:
                m_Rent = 5.0f;
                return ValueConstants.ZERO_STAR_HABITAT_PENALTY;
               

            case BuildingRating.ONESTAR:
                m_Rent = 10.0f;
                return ValueConstants.ONE_STAR_HABITAT_PENALTY;


            case BuildingRating.TWOSTAR:
                m_Rent = 25.0f;
                return ValueConstants.TWO_STAR_HABITAT_PENALTY;

            case BuildingRating.THREESTAR:
                m_Rent = 50.0f;
                return ValueConstants.THREE_STAR_HABITAT_PENALTY;

            case BuildingRating.FOURSTAR:
                m_Rent = 100.0f;
                return ValueConstants.FOUR_STAR_HABITAT_PENALTY;

            case BuildingRating.FIVESTAR:
                m_Rent = 200.0f;
                return ValueConstants.FIVE_STAR_HABITAT_PENALTY;
        }

        return 0;
    }  
}
