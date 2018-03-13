using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
	public GameObject inputManagerPrefab;

	private InputManager inputManager;

	private Scene scene;
	private int currentLevel = 0;
	private string currentState = "paused";

	private void Awake()
    {
		scene = SceneManager.GetActiveScene();

		if (instance == null)
		{
			instance = this;
		} 
		else if (instance != this)
		{
			Destroy(this.gameObject);
		}

		DontDestroyOnLoad(this.gameObject);

		inputManager = Instantiate(inputManagerPrefab).GetComponent<InputManager>();
		inputManager.transform.parent = this.transform;

    }

    void Update()
    {
		CheckInput();
    }

	public void CheckInput()
	{
		inputManager.checkInput();
	}

	public void ResetLevel()
	{
		//TODO: Implement a "soft reset" within a level
		scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}
	public void LoadNextLevel()
	{
		SceneManager.LoadScene(scene.buildIndex +1);
	}

}
