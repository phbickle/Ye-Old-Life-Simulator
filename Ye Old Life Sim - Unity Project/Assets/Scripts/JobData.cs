﻿using UnityEngine;
using System.Collections;

public class JobData : MonoBehaviour 
{
    //public Requirement[] m_Requirement;
    //public SkillGain[] m_SkillGain;
    public SkillAndAmount[] m_Requirement;
    public SkillAndAmount[] m_SkillGain;

    public float m_RepRequirement;
    public float m_Wage;
    public float m_MinWage;
    public float m_MaxWage;
    public float m_RepGain;

	void Start () 
    {
	
	}
	
	void Update () 
    {
        SetWage();
	}

    void SetWage()
    {
        if(m_MinWage != m_MaxWage)
        {
            m_Wage = Random.Range(m_MinWage, m_MaxWage);
        }
    }
}
