using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoHorizontal : MonoBehaviour {

    private bool collide = false;

    private float move = -2;
    
	void Start ()
    {		
	}
	
	void Update ()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(move, GetComponent<Rigidbody2D>().velocity.y);
        if (collide)
        {
            Flip();
        }
    }

    private void Flip()
    {
        move *= -1;
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        collide = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Plataformas"))
        {
            collide = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Plataformas"))
        {
            collide = false;
        }
    }
}
