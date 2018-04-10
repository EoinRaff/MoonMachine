using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(playerMotor))]
public class playerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    [SerializeField]
    private float minimumVelocity = 5f;
    private float dist;


    public float aimSpeed = 15;
    public float minDistance = 10f;
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
    Vector3 gravity = new Vector3(0, -10, 0);
    Vector3 previousPos = Vector3.zero;
    Vector3 blackHolePos;
    Vector3 activeForce;
    Vector3 momentum;
    Vector3 force;

    void Start()
    {
        motor = GetComponent<playerMotor>();
    }


    void Update()
    {
        //TODO: Clamp camera vertical axis
        #region Movement and Rotation
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * xMov;
        Vector3 _movVertical = transform.forward * zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        motor.Move(_velocity);

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
            if (moon != null) //I can't remember this one
            {
                previousPos = moon.transform.position;
            }
        }
        if (Input.GetMouseButtonUp(0)) //generate new moon at location of aim when LMB released
        {
            charging = false;
            Destroy(holoMoon);
            Destroy(moon);
            moon = Instantiate(moonPrefab, holoMoon.transform.position, holoMoon.transform.rotation);
            dist = Vector3.Distance(transform.position, moon.transform.position);
            blackHolePos = moon.transform.position;
        }
        if (Input.GetMouseButton(1))
        {
            Destroy(moon);
        }


        if (moon != null)
        {
            if (Vector3.Distance(transform.position, moon.transform.position) < minimumVelocity / 2) //destroy the moon if you get too close
            {
                Destroy(moon);
            }
            else //move towards moon
            {
                float attraction = Vector3.Distance(transform.position, moon.transform.position);
                attraction = Mathf.Clamp(attraction, minimumVelocity, dist);
                blackHolePos = (moon.transform.position - transform.position).normalized * attraction;
            }
            // I can't remember what previousPos was meant to do, but whatever it was it doesn't work.
            //previousPos = (previousPos - transform.position) * deceleration;
            force = blackHolePos;
//            force = blackHolePos + previousPos + gravity;
        }
        else
        {
            momentum = force;
            force = Vector3.zero;
        }
        //TODO: make momentum from previous moons carry over and decay over time. VERY IMPORTANT
        if (momentum != Vector3.zero)
        {
            momentum -= new Vector3(0.1f, 0.1f, 0.1f);
        }
        
        motor.Move(force  + momentum + _velocity  + gravity);

        #endregion

    }

}
