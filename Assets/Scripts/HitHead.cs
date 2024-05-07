using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHead : MonoBehaviour
{
  private Animator anim;
  void Start()
  {
    anim = transform.parent.GetComponent<Animator>();
  }
  void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.tag == "Player")
    {
      transform.parent.gameObject.tag = "Untagged";
      anim.SetTrigger("die");
      Destroy(gameObject.transform.parent.gameObject, 0.25f);
      col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    }
  }
}
