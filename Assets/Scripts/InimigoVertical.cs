using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoVertical : MonoBehaviour {

    private float time = 0.0f;

    public float timer;
    public float force;

	void Start ()
    {		
	}

	void Update ()
    {
        time += Time.deltaTime;
        if(time >= timer)
        {
            time = 0f;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force), ForceMode2D.Impulse);
        }
	}
}
