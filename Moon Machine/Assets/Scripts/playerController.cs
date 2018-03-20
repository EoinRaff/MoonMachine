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
    Vector3 gravity = new Vector3(0, -1, 0);
	Vector3 previousPos = Vector3.zero;
	Vector3 blackHolePos;
    Vector3 force;

    void Start()
    {
        motor = GetComponent<playerMotor>();
    }


    void Update()
    {
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
        if (Input.GetMouseButtonDown(0))
        {
            charging = true;
            holoMoon = Instantiate(holoMoonPrefab, transform.position + cam.transform.forward * minDistance, cam.transform.rotation);
        }
        if (Input.GetMouseButton(0))
        {
            if (charging)
            {
                holoMoon.transform.position += cam.transform.forward * Time.deltaTime * aimSpeed;
            }
			if (moon != null)
			{
				previousPos = moon.transform.position;
			}
        }
        if (Input.GetMouseButtonUp(0))
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
            if (Vector3.Distance(transform.position, moon.transform.position) < minimumVelocity/2)
            {
                Destroy(moon);
            }
            else
            {
                float attraction = Vector3.Distance(transform.position, moon.transform.position);
                attraction = Mathf.Clamp(attraction, minimumVelocity, dist);
                Debug.Log(attraction);
                blackHolePos = (moon.transform.position - transform.position).normalized * attraction;
            }
        }
		previousPos = (previousPos - transform.position) * deceleration;
		force = blackHolePos + previousPos + gravity;
		motor.Move(force + _velocity);


        #endregion
    }

}
