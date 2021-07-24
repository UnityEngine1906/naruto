using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controll : MonoBehaviour
{
  public float speed;
  public float jumpForce;

  private float gravityForce = -9;
  private Vector3 moveVector;

  public CharacterController controller;
  public Animator anim;

  private void Start()
  {
    controller = GetComponent<CharacterController>();
    anim = GetComponent<Animator>();
  }

  private void Update()
  {
    plMove();
    ISGR();
  }


  private void plMove()
  {
      moveVector = Vector3.zero;
      moveVector.x = Input.GetAxis("Horizontal") * speed;
      moveVector.z = Input.GetAxis("Vertical") * speed;

      if(moveVector.x!=0||moveVector.z!=0)
      {
         anim.SetBool("ismove", true);
      }
      else anim.SetBool("ismove", false);

      if(Vector3.Angle(Vector3.forward,moveVector)>1f || Vector3.Angle(Vector3.forward,moveVector)==0)
      {
        Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speed, 0.0f);
        transform.rotation = Quaternion.LookRotation(direct);
      }

    moveVector.y = gravityForce;
    controller.Move(moveVector * Time.deltaTime);
  }

  private void ISGR()
  {
    if(!controller.isGrounded) gravityForce -= 20f * Time.deltaTime;
    else gravityForce = -1f;
    if(Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
    {
      gravityForce = jumpForce;
      anim.SetTrigger("isjump");
    }
    if (Input.GetKeyDown(KeyCode.LeftShift)) {
      speed += 3;
      anim.SetBool("isrun", true);
    }
    if (Input.GetKeyUp(KeyCode.LeftShift)) {
      speed -= 3;
      anim.SetBool("isrun", false);
    }
  }

}
