using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class devHud : MonoBehaviour {

    playerController playerScript;      //Referencing player controls
    GameObject player;                  //Referencing the player

    private Vector3 playerVelocity;     //Stores players velocity

    public Text displayVelocity;        //Displays velocity in HUD      

	void Awake ()
    {

        //Associate playerScript and player with the relevant GameObjects
        playerScript = GameObject.Find("Player").GetComponent<playerController>();
        player = GameObject.Find("Player");
        playerVelocity = player.GetComponent<Rigidbody>().velocity;
    }

    void FixedUpdate()
    {
        //Get player velocity and display it in HUD
        playerVelocity = player.GetComponent<Rigidbody>().velocity;
        displayVelocity.text = "Velocity: " + playerVelocity.ToString();
    }
}
