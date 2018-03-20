using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {

	public GameObject weapon;
	public GameObject Envi;

	private  Attractor attractor;
	private GameObject speed;

	private float _speed;
	private float debuff;
	private float vIncrease;
	// Use this for initialization
	void Awake()
	{
		debuff = 2;
		vIncrease = 0;
		speed = GameObject.Find("Player");
		attractor = speed.GetComponent<Attractor>();

	}

	// Update is called once per frame
	void Update()
	{
		this._speed = attractor.force.magnitude;
		this._speed = this._speed / debuff;

		// Environmental sounds		(The wind is based on the player's velocity)
		if (!Envi.GetComponent<AudioSource>().isPlaying && this._speed >= 1)
			Envi.GetComponent<AudioSource>().Play();
		

		if (Envi.GetComponent<AudioSource>().isPlaying)
		{
			vIncrease += 0.005f;
			Envi.GetComponent<AudioSource>().volume = Mathf.Lerp(0, 1, vIncrease);
			Envi.GetComponent<AudioSource>().pitch = Mathf.Lerp(1, 3, this._speed / 100);

			if (this._speed <= 1)
			{
				Envi.GetComponent<AudioSource>().Stop();
				vIncrease = 0;
			}
		}

		// Weapon sounds
		if (Input.GetMouseButton(0))				  
		{
			if (!weapon.GetComponent<AudioSource>().isPlaying)
				weapon.GetComponent<AudioSource>().Play();
			weapon.GetComponent<AudioSource>().pitch -= 0.005f;
		}

		else if (!Input.GetMouseButton(0))
		{
			if (weapon.GetComponent<AudioSource>().isPlaying)
				weapon.GetComponent<AudioSource>().Stop();
			weapon.GetComponent<AudioSource>().pitch = 1;
		}
	}
}
