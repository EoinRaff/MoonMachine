using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

	private GameManager gameManager;

	private void Awake()
    {
		gameManager = GameManager.instance;
    }
    public void checkInput()
	{
	}
}
