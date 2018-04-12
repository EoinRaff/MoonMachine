﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(playerMotor))]
public class playerController : MonoBehaviour
{

    //TODO:
    //- isGrounded(){
    //- if rb is grounded, disable forces
    //
    //-cooldown on moon shooter
    
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    [SerializeField]
    private float minimumVelocity = 5f;
    private float spawnDistance;


    public float aimSpeed = 50;
    public float minDistance = 1f;
    public float deceleration;
    public GameObject cam;

    public bool charging;

    [SerializeField]
    GameObject holoMoonPrefab;
    GameObject holoMoon;

    [SerializeField]
    GameObject moonPrefab;
    GameObject moon;

    private playerMotor motor;
    private Rigidbody rb;
    Vector3 gravity = new Vector3(0, -10, 0);
    Vector3 momentum = Vector3.zero;
    Vector3 attractiveForce;
    Vector3 force;

    void Start()
    {
        motor = GetComponent<playerMotor>();
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        //TODO: 
        //-Clamp camera vertical axis
        //-Lock Cursor
        #region Movement and Rotation
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * xMov;
        Vector3 _movVertical = transform.forward * zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        

        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        motor.Rotate(_rotation);

        float _xRot = Input.GetAxisRaw("Mouse Y");
        
        Vector3 _camerRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;

        motor.RotateCamera(_camerRotation);
        #endregion

        #region MoonControl
        //TODO: fix big where player gets pulled back to origin
        if (Input.GetMouseButtonDown(0)) //begin charging if the user presses LMB
        {
            charging = true;
            holoMoon = Instantiate(holoMoonPrefab, transform.position + cam.transform.forward * minDistance, cam.transform.rotation);
        }
        if (Input.GetMouseButton(0)) //continue charging while holding LMB
        {
            if (charging)
            {
                holoMoon.transform.position += cam.transform.forward * Time.deltaTime * aimSpeed;
            }
        }
        if (Input.GetMouseButtonUp(0)) //generate new moon at location of aim when LMB released
        {
            //Stop charging and delete aiming reticle
            charging = false;
            Destroy(holoMoon);

            // assign previous moon's force OR velocity as momentum then delete the active moon
            ChangeMomentum();
            Destroy(moon);

            //create the new moon and save its position
            moon = Instantiate(moonPrefab, holoMoon.transform.position, holoMoon.transform.rotation);
            spawnDistance = Vector3.Distance(transform.position, moon.transform.position);
            attractiveForce = moon.transform.position;
        }
        if (Input.GetMouseButton(1))
        {
            ChangeMomentum();
            Destroy(moon);
        }

        if (moon != null)
        {
            if (Vector3.Distance(transform.position, moon.transform.position) < minDistance) //destroy the moon if you get too close
            {
                ChangeMomentum();
                Destroy(moon);
            }
            else //move towards moon
            {
                float attraction = Vector3.Distance(transform.position, moon.transform.position);
                attraction = Mathf.Clamp(attraction, minimumVelocity, spawnDistance);
                attractiveForce = (moon.transform.position - transform.position).normalized * attraction;
            }

            motor.Move(attractiveForce + momentum);

        }
        else
        {
            motor.Move(_velocity + momentum + gravity);
        }
        //TODO: make momentum from previous moons carry over and decay over time. VERY IMPORTANT
        //motor.Move(force + momentum + gravity);
        momentum = UpdateMomentum();
        #endregion

        //motor.Move(attractiveForce);
        //motor.Move(momentum);
        //force = _velocity + attractiveForce + momentum;
        //motor.Move(force);
    }
    void ChangeMomentum()
    {
        momentum = attractiveForce;
    }
    Vector3 UpdateMomentum()
    {
        Vector3 direction = momentum.normalized;
        float magnitude = momentum.magnitude;
        magnitude *= deceleration;
        return direction*magnitude;
    }
}
