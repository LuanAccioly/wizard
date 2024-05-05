using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor.Tilemaps;
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
    PauseGame();
  }



  void Move()
  {
    // Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
    // transform.position += movement * Time.deltaTime * Speed;

    float movement = Input.GetAxis("Horizontal");
    rig.velocity = new Vector2(movement * Speed, rig.velocity.y);
    if (movement > 0f)
    {
      anim.SetBool("run", true);
    }
    else if (movement < 0f)
    {
      anim.SetBool("run", true);
    }
    else if (movement == 0f)
    {
      anim.SetBool("run", false);
    }

    if (movement > 0f)
    {
      transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }
    else if (movement < 0f)
    {
      transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }
  }

  void PauseGame()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      GameController.instance.PauseGame();
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

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.layer == 8)
    {
      isJumping = false;
      anim.SetBool("jump", false);
      anim.SetBool("doublejump", false);
    }

    if (collision.gameObject.tag == "Dangerous")
    {
      GameController.instance.ShowGameOver();
      Destroy(gameObject);
    }

  }

  void OnCollisionExit2D(Collision2D collision)
  {
    if (collision.gameObject.layer == 8)
    {
      isJumping = true;
    }
    // if (collision.gameObject.layer == 7)
    // {
    //   isJumping = false;
    // }
  }


}
