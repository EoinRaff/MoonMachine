using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMouseLook : MonoBehaviour {

	Vector2 mouseLook;
	Vector2 smoothV;
	public bool invertLook;
	public float sensitivity = 5.0f;
	public float smoothing = 1.0f;
	public float lowerClamp = -90f;
	public float upperClamp = 90f;

	GameObject character;

	void Start () 
	{
		character = this.transform.parent.gameObject;
	}
	
	void Update () 
	{
		
		float inversion;
		
		if (invertLook)
		{
			inversion = 1;
		} else
		{
			inversion = -1;
		}

		Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
		smoothV.x = Mathf.Lerp(smoothV.x, mouseDelta.x, 1f/smoothing);
		smoothV.y = Mathf.Lerp(smoothV.y, mouseDelta.y, 1f/smoothing);
		mouseLook += smoothV;
		mouseLook.y = Mathf.Clamp(mouseLook.y, lowerClamp, upperClamp);

		transform.localRotation = Quaternion.AngleAxis(mouseLook.y * inversion, Vector3.right);
		character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);		
	}
}
