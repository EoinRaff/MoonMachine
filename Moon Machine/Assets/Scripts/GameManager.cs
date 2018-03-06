using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
	public GameObject inputManagerPrefab;

	private InputManager inputManager;

	private int currentLevel = 0;
	private string currentState = "paused";


	private void Awake()
    {
		if (instance == null)
		{
			instance = this;
		} else if (instance != this)
		{
			Destroy(this.gameObject);
		}

		DontDestroyOnLoad(this.gameObject);

		inputManager = Instantiate(inputManagerPrefab).GetComponent<InputManager>();
		inputManager.transform.parent = this.transform;

    }

    // Update is called once per frame
    void Update()
    {
		CheckInput();
    }

	public void CheckInput(){
		inputManager.checkInput();
	}
}
