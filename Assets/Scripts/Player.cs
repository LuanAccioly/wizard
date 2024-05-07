using System.Collections;
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
  private bool imortal;
  private float imortalTime = 5.0f;


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
    if (imortal)
    {
      GetComponent<SpriteRenderer>().color =

      new Color(255, 0, 0, 1f);
    }
    else
    {
      GetComponent<SpriteRenderer>().color =
      new Color(255, 255, 255, 1f);
    }
  }



  void Move()
  {
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

  public void TurnImortal()
  {
    imortal = true;
    StartCoroutine("ImortalTime");
  }
  IEnumerator ImortalTime()
  {
    yield return new WaitForSeconds(imortalTime);
    imortal = false;
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
      if (imortal)
      {
        if (collision.gameObject.GetComponent<Rhino>())
        {
          collision.gameObject.transform.GetComponent<Animator>().SetTrigger("die");
          collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
          Destroy(collision.gameObject, 0.25f);
        }
      }
      else
      {
        GameController.instance.ShowGameOver();
        Destroy(gameObject);
      }
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
