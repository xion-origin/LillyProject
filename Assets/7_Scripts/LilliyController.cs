using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilliyController : MonoBehaviour
{
    public float jumpSpeed = 8.0F;   //ジャンプ力
    public float gravity = 20.0F;    //重力の大きさ

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private float h, v;
    public float move_speed ;
    public GameObject LillyObj;

    Animator animator;
    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }//Start()

    // Update is called once per frame
    void Update()
    {
        
       
        moveDirection.x = h * move_speed;
        if (controller.isGrounded)//地上
        {
            
            animator.SetBool("jump", false);
            if (Input.GetKeyDown(KeyCode.Space))
                moveDirection.y = jumpSpeed;
        }
        else//空中
        {
            Debug.Log("空中");
            animator.SetBool("jump", true);
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
        {
            h = -1;
            LillyObj.transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("dash", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            h = 1;
            LillyObj.transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("dash", true);
        }
        else
        {
            h = 0;
            animator.SetBool("dash", false);

        }

    }//Update()
}
