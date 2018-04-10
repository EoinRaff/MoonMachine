using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {

	const float G = 667.4f;

	public Rigidbody rb;
	public bool active = true;
	public bool decays;
	public float massDecay;

	void FixedUpdate() {
		Attractor[] attractors = FindObjectsOfType<Attractor>();
		
		if (active)
		{
			foreach (Attractor a in attractors)
			{
				if (a != this)
				{
					Attract(a);
				}
			}
			if (decays)
			{
				rb.mass -= massDecay;
				if (rb.mass <= 0)
				{
					active = false;
				}
			}	
		}

	}

	void Attract(Attractor objToAttract)
	{
		
		Rigidbody rbToAttract = objToAttract.rb;

		Vector3 direction = rb.position - rbToAttract.position;
		float distance = direction.magnitude;

		float forceMagnitude = G * (rb.mass * rbToAttract.mass)/Mathf.Pow(distance, 2);
		Vector3 force = direction.normalized * forceMagnitude;

		rbToAttract.AddForce(force);
	}

	public void setMass(float n){

		rb.mass = n;

	}
}
