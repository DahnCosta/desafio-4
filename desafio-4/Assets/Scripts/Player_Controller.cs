using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float speed = 5f;
    public bool isGrounded = true;
    public float jumpForce = 650f;

    private Animator anim;
    private Rigidbody2D rig;

    public LayerMask LayerGround;
    public Transform CheckGround;
    public string isGroundBool = "eChao";

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

        // Virar o sprite conforme direção
        if (horizontal != 0)
            transform.localScale = new Vector3(horizontal > 0 ? 1 : -1, 1, 1);
    }

    private void FixedUpdate()
    {
        // Verifica se está tocando o chão
        if (Physics2D.OverlapCircle(CheckGround.position, 0.2f, LayerGround))
        {
            anim.SetBool(isGroundBool, true);
            isGrounded = true;
        }
        else
        {
            anim.SetBool(isGroundBool, false);
            isGrounded = false;
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rig.linearVelocity = Vector2.zero;
            rig.AddForce(new Vector2(0, jumpForce));
        }
    }
}
