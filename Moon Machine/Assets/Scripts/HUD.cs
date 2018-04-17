using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {

	public GameObject player;
	moonGenerator moonGenerator;
	// Use this for initialization
	void Start () {
		moonGenerator = player.GetComponent<moonGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!moonGenerator.canShootMoon)
		{
			//display NOT READY
		}
		else
		{
			if (moonGenerator.charging)
			{
				//display CHARGING
			}
			else
			{
				//display READY
			}
		}
	}
}
