﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour 
{
    public AudioSource m_PlayerRunning;
    public PlayerData m_PlayerData;     //object to hold the player data, if that wasn't obvious 
    public Camera mCamera;
    public Terrain mTerrain;
    public Transform mblackSmithWaypoint;
    public bool m_IsMoving;

    private Vector3 currTarget_;
    private bool atCurrTarget_;
    private Animator animator_;
    private NavMeshAgent navAgent_;
    private float lastRot_;
    private float hungerTimer_;

    public void SetTarget(Vector3 target)
    {
        currTarget_ = target;
        atCurrTarget_ = false;
        navAgent_.SetDestination(target);
    }

    // Use this for initialization
    void Start()
    {
        currTarget_ = Vector3.zero;
        atCurrTarget_ = true;
        animator_ = GetComponent<Animator>();
        navAgent_ = GetComponent<NavMeshAgent>();
        lastRot_ = transform.rotation.eulerAngles.y;

    }

    void OnDisable()
    {
        m_IsMoving = false;
    }

    void Update()
    {
        UpdateAnimation();

        Ray pickingRay = mCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit rayData;

        if (Input.GetMouseButtonUp(0))
        {
            //if (!EventSystem.current.IsPointerOverGameObject())
            //{
                Debug.Log("Not over UI");
                if (transform != null)
                {
                    int layerMask = LayerMask.GetMask("Terrain");
                    Physics.Raycast(pickingRay, out rayData, 3000.0f, layerMask);
                    SetTarget(rayData.point);
                }

                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(pickingRay, out hitInfo);
                if (hit)
                {
                    if (hitInfo.transform.gameObject.tag == "Building")
                    {
                        Debug.Log("It's working!");
                        Transform[] posEs = hitInfo.transform.gameObject.GetComponentsInChildren<Transform>();
                        foreach (Transform tForm in posEs)
                        {
                            if (tForm.CompareTag("Waypoint"))
                            {
                                Debug.Log("Found the waypoint.");
                                SetTarget(tForm.position);
                                break;
                            }
                        }
                    }
                }
            //}
        }

        //DecreaseTime();

        if(navAgent_.velocity.magnitude > 0.0f)
        {
            m_IsMoving = true;
        }
        else
        {
            m_IsMoving = false;
        }
    
        navAgent_.speed = m_PlayerData.GetComponent<PlayerData>().m_Speed;

        UpdateWalkSound();
    }

    private void UpdateAnimation()
    {
        float speedRatio = navAgent_.velocity.magnitude / navAgent_.speed;
        float angularSpeed = transform.rotation.eulerAngles.y - lastRot_;
        lastRot_ = transform.rotation.eulerAngles.y;
        animator_.SetFloat("speed", speedRatio);
        animator_.SetFloat("xDir", 0.0f);
        animator_.SetFloat("yDir", speedRatio);
        animator_.SetFloat("rotation", Mathf.Min(1.0f, Mathf.Abs(angularSpeed / (navAgent_.angularSpeed * Time.deltaTime))));
        animator_.SetFloat("rotDir", Mathf.Clamp(angularSpeed / (navAgent_.angularSpeed * Time.deltaTime), -1.0f, 1.0f));
    }

    private void DecreaseTime()
    {
        //when the player is moving decrease time by Time.deltaTime
        if (navAgent_.velocity.magnitude > 0.0f && m_PlayerData.m_CurrTime != 0.0f)
        {
            m_PlayerData.m_CurrTime -= Time.deltaTime;
            hungerTimer_ += Time.deltaTime;
            if(hungerTimer_ >= 1.0f)
            {
                m_PlayerData.m_HungerMeter += 1.0f;
                hungerTimer_ = 0.0f;
            }
        }
    }

    private void UpdateWalkSound()
    {
        if(m_IsMoving && !m_PlayerRunning.isPlaying)
        {
            m_PlayerRunning.Play();
        }
    }
}
