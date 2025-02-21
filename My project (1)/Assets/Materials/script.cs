using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float velocidade = 10f;
    public float focaPulo = 10f;

    public bool noChao = false;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "chao")
        {
            noChao = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "chao")
        {
            noChao = false;
        }
         if (collision.gameObject.tag == "lava")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void Update()
    {
        float movimento = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movimento = -velocidade;
            spriteRenderer.flipX = true;
            Debug.Log("LeftArrow");
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            movimento = velocidade;
            spriteRenderer.flipX = false;
            Debug.Log("RightArrow");
        }
        
        _rigidbody2D.velocity = new Vector2(movimento, _rigidbody2D.velocity.y);

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, focaPulo);
            Debug.Log("Jump");
        }

        // Reinicia a cena se o jogador cair
       
       
    }
}
