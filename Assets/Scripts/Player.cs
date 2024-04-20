using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private Rigidbody2D rig;
  public float Speed;
  public float JumpForce;

  public float doubleJumpForce;
  private bool isJumping;
  private bool doubleJump;
  private Animator anim;

  void Start()
  {
    rig = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    Move();
    Jump();
  }

  void Move()
  {
    Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
    transform.position += movement * Time.deltaTime * Speed;
    anim.SetBool("run", true);


  }

  void Jump()
  {
    if (Input.GetButtonDown("Jump"))
    {
      if (!isJumping)
      {
        rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        doubleJump = true;
      }
      else
      {
        if (doubleJump)
        {
          rig.AddForce(new Vector2(0f, doubleJumpForce), ForceMode2D.Impulse);
          doubleJump = false;
        }
      }
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.layer == 8)
    {
      isJumping = false;
    }
  }
  void OnCollisionExit2D(Collision2D collision)
  {
    if (collision.gameObject.layer == 8)
    {
      isJumping = true;
    }
  }
}
