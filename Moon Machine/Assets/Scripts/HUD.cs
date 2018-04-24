using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour {

	public GameObject player;
	moonGenerator moonGenerator;
	public TextMeshProUGUI reloadText;
	// Use this for initialization
	void Start () {
		moonGenerator = player.GetComponent<moonGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!moonGenerator.canShootMoon)
		{
			//display NOT READY
			reloadText = "NOT READY";
		}
		else
		{
			if (moonGenerator.charging)
			{
				//display CHARGING
				reloadText = "CHARGING";
			}
			else
			{
				//display READY
				reloadText = "READY";
			}
		}
	}
}
