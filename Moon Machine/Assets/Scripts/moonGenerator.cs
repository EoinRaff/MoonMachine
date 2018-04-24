using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Attractor))]
public class moonGenerator : MonoBehaviour
{

    public bool canShootMoon = true;
    public float coolDownTime = 1;
    //0 = charging moon, 1 = fixed pos, 2 = static on aim.

    public float aimSpeed = 50f;
    public float minDistance = 15f;
    public GameObject cam;

    public bool charging;

    [SerializeField]
    GameObject holoMoonPrefab;
    GameObject holoMoon;

    [SerializeField]
    GameObject moonPrefab;
    GameObject moon;

    Attractor moonAtractor;
    Attractor thisAttractor;
    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thisAttractor = GetComponent<Attractor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GameIsPaused && canShootMoon)
        {
            if (Input.GetMouseButtonDown(0))
            {
                charging = true;
                holoMoon = Instantiate(holoMoonPrefab, transform.position + cam.transform.forward * minDistance, cam.transform.rotation);
                //Destroy(moon);
                //thisAttractor.active = false;
                //rb.useGravity = true;

            }
            if (Input.GetMouseButton(0))
            {
                if (charging)
                {
                    holoMoon.transform.position += cam.transform.forward * Time.deltaTime * aimSpeed;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                charging = false;
                Destroy(holoMoon);
                Destroy(moon);
                moon = Instantiate(moonPrefab, holoMoon.transform.position, holoMoon.transform.rotation);

                StartCoroutine(Cooldown(coolDownTime));

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
        }

    }
    IEnumerator Cooldown(float duration)
    {
        Debug.Log("Deactivate");
        canShootMoon = false;
        Debug.Log("Wait");
        yield return new WaitForSeconds(duration);
        Debug.Log("reactivate");
        canShootMoon = true;
    }
}
