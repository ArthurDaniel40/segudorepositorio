using System.Collections;
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

    // Detect when player enters ground collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            noChao = true;
        }

        if (collision.gameObject.CompareTag("lava"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Detect when player exits ground collision
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            noChao = false;
        }
    }

    void Update()
    {
        float movimento = 0f;

        // Movement
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movimento = -velocidade;
            spriteRenderer.flipX = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            movimento = velocidade;
            spriteRenderer.flipX = false;
        }

        _rigidbody2D.velocity = new Vector2(movimento, _rigidbody2D.velocity.y);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, focaPulo);
        }
    }
}
