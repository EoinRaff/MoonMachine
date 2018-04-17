using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class devHud : MonoBehaviour {

    //TODO: Don't hardcode button input, but rather get the variable name

//    playerController playerScript;      //Referencing player controls
    Attractor attractorScript;          //Referencing attractor script
    moonGenerator mg;                   //Referencing moonGenerator script
    GameObject player;                  //Referencing the player

    private Vector3 playerVelocity;     //Stores players velocity

    public Text displayVelocity;        //Displays velocity in HUD
    public Text displayMoonActive;      //Displays if you are being affected by moon gravity or not
    public Text displayIsCharging;      //Display if you are currently charging a moon

	void Awake ()
    {

        //Associate objects of scripts with the proper real script
  //      playerScript = GameObject.Find("Player").GetComponent<playerController>();
        attractorScript = GameObject.Find("Player").GetComponent<Attractor>();
        mg = GameObject.Find("Player").GetComponent<moonGenerator>();
        player = GameObject.Find("Player");

        //Get velocity variable from the players rigidbody
        playerVelocity = player.GetComponent<Rigidbody>().velocity;
    }

    void FixedUpdate()
    {
        //For the active moon
        displayMoonActive.text = attractorScript.active ? "Affected!" : "NOT affected!";

        //For charging
        displayIsCharging.text = mg.charging ? "CHARGING!" : "";

        //For the player velocity
        playerVelocity = player.GetComponent<Rigidbody>().velocity;
        displayVelocity.text = "Velocity (x,y,z): " + playerVelocity.ToString();
    }
}
