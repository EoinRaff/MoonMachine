using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.instance;
    }
}
