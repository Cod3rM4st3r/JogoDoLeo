using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.AI;

public class Player : MonoBehaviour {

    public float forcaPulo;
    public float velocidadeMaxima;

    public int lives;
    public int rings;

    public Text TextLives;
    public Text TextRings;

    public bool isGrounded;

    public bool canFly;

    public bool inWater;

    public GameObject LastCheckpoint;

    void Start ()
    {
        TextLives.text = lives.ToString();
        TextRings.text = rings.ToString();
    }

    void Update()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        float movimento = Input.GetAxis("Horizontal");

        rigidbody.velocity = new Vector2(movimento * velocidadeMaxima, rigidbody.velocity.y);

        if (movimento < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        } else if (movimento > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (movimento > 0 || movimento < 0)
        {
            GetComponent<Animator>().SetBool("walking", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("walking", false);
        }

        if (!inWater)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded)
                {
                    rigidbody.AddForce(new Vector2(0, forcaPulo));
                    GetComponent<AudioSource>().Play();
                    canFly = false;
                }
                else
                {
                    canFly = true;
                }
            }

            if (canFly && Input.GetKey(KeyCode.Space))
            {
                GetComponent<Animator>().SetBool("flying", true);
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, -0.5f);
            }
            else
            {
                GetComponent<Animator>().SetBool("flying", false);
            }

            if (isGrounded)
            {
                GetComponent<Animator>().SetBool("jumping", false);
            }
            else
            {
                GetComponent<Animator>().SetBool("jumping", true);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rigidbody.AddForce(new Vector2(0, 6f * Time.deltaTime), ForceMode2D.Impulse);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                rigidbody.AddForce(new Vector2(0, -6f * Time.deltaTime), ForceMode2D.Impulse);
            }

            rigidbody.AddForce(new Vector2(0, 10f * Time.deltaTime), ForceMode2D.Impulse);
        }

        GetComponent<Animator>().SetBool("swimming", inWater);
    }

    void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Water"))
        {
            inWater = true;
        }

        if (collision2D.gameObject.CompareTag("Moedas"))
        {
            Destroy(collision2D.gameObject);
            rings++;
            TextRings.text = rings.ToString();
        }

        if (collision2D.gameObject.CompareTag("Checkpoint"))
        {
            LastCheckpoint = collision2D.gameObject;
        }

    }

    void OnTriggerExit2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Water"))
        {
            inWater = false;
        }        
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.CompareTag("Monstros"))
        {
            lives--;
            TextLives.text = lives.ToString();
            if(lives == 0)
            {
                transform.position = LastCheckpoint.transform.position;
            }
        }

        if (collision2D.gameObject.CompareTag("Plataformas"))
        {
            isGrounded = true;
        }        

        if (collision2D.gameObject.CompareTag("Trampolim"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 10f);
        }
    }

    void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Plataformas"))
        {
            isGrounded = false;
        }        
    }
}
