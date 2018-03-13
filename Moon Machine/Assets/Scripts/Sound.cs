using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {

	AudioSource audio;
	float _speed;
	float debuff;
	float vIncrease;
	// Use this for initialization
	void Start()
	{
		audio = GetComponent<AudioSource>();
		debuff = 2;
		vIncrease = 0;
	}

	// Update is called once per frame
	void Update()
	{
		GameObject speed = GameObject.Find("Player");
		Attractor attractor = speed.GetComponent<Attractor>();

		this._speed = attractor.force.magnitude;

		this._speed = this._speed / debuff;

		if (!audio.isPlaying && this._speed >= 1)
		{
				audio.Play();
		}

		if (audio.isPlaying)
		{
			vIncrease+=0.01f;
			audio.volume = Mathf.Lerp(0, 1,vIncrease);

			audio.pitch = Mathf.Lerp(1,3, this._speed / 100);

			if (this._speed <= 1)
			{
				audio.Stop();
				vIncrease = 0;
			}

		}


		  
	}
}
