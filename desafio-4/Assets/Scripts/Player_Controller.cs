using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float speed = 5f;
    public bool isGrounded = true;
    public float jumpForce = 300f;

    private Animator anim;
    private Rigidbody2D rig;

    public LayerMask LayerGround;
    public Transform CheckGround;
    public string isGroundBool = "eChao";

    private AudioController _audioController;
    private CameraShaker _cameraShaker;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _audioController = GameObject.FindObjectOfType<AudioController>();
        _cameraShaker = GameObject.FindObjectOfType<CameraShaker>();

        if (_audioController == null)
        {
            Debug.LogWarning("AudioController não encontrado na cena.");
        }

        if (_cameraShaker == null)
        {
            Debug.LogWarning("CameraShaker não encontrado na cena.");
        }
    }

    void Update()
    {
        MovimentaPlayer();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void MovimentaPlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        rig.linearVelocity = new Vector2(horizontal * speed, rig.linearVelocity.y);

        if (horizontal != 0)
            transform.localScale = new Vector3(horizontal > 0 ? 1 : -1, 1, 1);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(CheckGround.position, 0.2f, LayerGround);
        anim.SetBool(isGroundBool, isGrounded);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rig.linearVelocity = Vector2.zero;
            rig.AddForce(new Vector2(0, jumpForce));

            if (_audioController != null)
            {
                _audioController.TocarSomPulo();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            LevarDano();
        }
    }

    public void LevarDano()
    {
        if (_audioController != null)
        {
            _audioController.TocarSomDano();
            Debug.Log("Som de dano tocado!");
        }
        else
        {
            Debug.LogWarning("AudioController está nulo no LevarDano");
        }

        if (_cameraShaker != null)
        {
            _cameraShaker.ShakeIt();
            Debug.Log("Shake da câmera iniciado!");
        }
        else
        {
            Debug.LogWarning("CameraShaker está nulo no LevarDano");
        }
    }
}
