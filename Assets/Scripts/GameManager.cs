using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject PainelCompleto;

    public bool isPaused = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Pause()
    {
        if (isPaused)
        {
            PainelCompleto.SetActive(false);
            isPaused = false;
        }
        else
        {
            PainelCompleto.SetActive(true);
            isPaused = true;
        }
        
    }
}
