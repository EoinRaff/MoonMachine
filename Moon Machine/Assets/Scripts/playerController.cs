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


    public float aimSpeed = 5;
    public float minDistance = 1f;
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
                //holoMoon.GetComponent<Rigidbody>().MovePosition(cam.transform.forward * Time.deltaTime * aimSpeed);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            charging = false;
            Destroy(holoMoon);
            Destroy(moon);
            moon = Instantiate(moonPrefab, holoMoon.transform.position, holoMoon.transform.rotation);
			dist = Vector3.Distance(transform.position, moon.transform.position);
			
            //moonAtractor = moon.GetComponent<Attractor>();
            //thisAttractor.active = true;
            //rb.useGravity = false;
        }
        if (Input.GetMouseButton(1))
        {
            Destroy(moon);
            //thisAttractor.active = false;
            //rb.useGravity = true;
        }
        if (moon != null)
        {
            float attraction = Vector3.Distance(transform.position, moon.transform.position);
			attraction = Mathf.Clamp(attraction, minimumVelocity,dist);
			Debug.Log(attraction);
            Vector3 force = (moon.transform.position - transform.position).normalized * attraction;
            motor.Move(force);
        }

        #endregion
    }

}
