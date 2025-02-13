using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    public float velocidade = 10f;
    public float dashForca = 30f; // Agora é força aplicada ao Dash
    public float focaPulo = 10f;
    public bool noChao = false;
    public bool podeDash = true;
    public bool corerendo = false; 
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer spriteRenderer;


    private Animator _animator;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            noChao = true;
            podeDash = true; // Permite Dash ao tocar no chão novamente
        }

        if (collision.gameObject.CompareTag("lava"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            noChao = false;
        }
    }

    void Update()
    {
        float movimento = Input.GetAxisRaw("Horizontal");

        // Define a direção do sprite
        if (movimento < 0)
        {
            spriteRenderer.flipX = true;
            corerendo = true;
        }
        else if (movimento > 0)
        {
            spriteRenderer.flipX = false;
            corerendo = true;
        }
        else
        {
            corerendo = false;
        }
        _animator.SetBool ("correndo", corerendo);

        // Aplica movimento normal
        _rigidbody2D.velocity = new Vector2(movimento * velocidade, _rigidbody2D.velocity.y);

        // Dash (uma única vez no ar)
        if (Input.GetKeyDown(KeyCode.RightShift) && podeDash && movimento != 0)
        {
            _rigidbody2D.AddForce(new Vector2(movimento * dashForca, 0), ForceMode2D.Impulse);
            podeDash = false;
        }

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, focaPulo);
        }
    }
}
