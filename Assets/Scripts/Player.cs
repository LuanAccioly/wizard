using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Player : MonoBehaviour
{
  private float horizontal;
  private Rigidbody2D rig;
  public float Speed;
  public float JumpForce;

  public float doubleJumpForce;
  private bool isJumping;
  private bool doubleJump;
  private Animator anim;
  private bool isWallSliding;
  private float wallSlidingSpeed = 2f;

  [SerializeField] private Transform wallCheck;
  [SerializeField] private LayerMask wallLayer;

  void Start()
  {
    rig = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    horizontal = Input.GetAxis("Horizontal");
    Move();
    Jump();
    WallSlide();
  }

  void Move()
  {
    Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
    transform.position += movement * Time.deltaTime * Speed;

    if (Input.GetAxis("Horizontal") > 0f)
    {
      anim.SetBool("run", true);
    }
    else if (Input.GetAxis("Horizontal") < 0f)
    {
      anim.SetBool("run", true);
    }
    else if (Input.GetAxis("Horizontal") == 0f)
    {
      anim.SetBool("run", false);
    }

    if (Input.GetAxis("Horizontal") > 0f)
    {
      transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }
    else if (Input.GetAxis("Horizontal") < 0f)
    {
      transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }
  }

  void Jump()
  {
    if (Input.GetButtonDown("Jump"))
    {
      if (!isJumping)
      {
        rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        doubleJump = true;
        anim.SetBool("jump", true);
      }
      else
      {
        if (doubleJump)
        {
          anim.SetBool("doublejump", true);
          rig.AddForce(new Vector2(0f, doubleJumpForce), ForceMode2D.Impulse);
          doubleJump = false;
        }
      }
    }
  }

  bool IsWalled()
  {
    return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
  }

  void WallSlide()
  {
    if (IsWalled() && horizontal != 0)
    {
      isWallSliding = true;
      rig.velocity = new Vector2(rig.velocity.x, Mathf.Clamp(rig.velocity.y, -wallSlidingSpeed, float.MaxValue));
    }
    else
    {
      isWallSliding = false;
    }
  }


  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.layer == 8)
    {
      isJumping = false;
      anim.SetBool("jump", false);
      anim.SetBool("doublejump", false);
    }


    //  Se quiser que o player pare de pular quando estiver enroscado na parede, só descomentar o código abaixo	

    if (collision.gameObject.layer == 7)
    {
      isJumping = true;
    }
  }
  void OnCollisionExit2D(Collision2D collision)
  {
    if (collision.gameObject.layer == 8)
    {
      isJumping = true;
    }
    if (collision.gameObject.layer == 7)
    {
      isJumping = false;
    }
  }


}
