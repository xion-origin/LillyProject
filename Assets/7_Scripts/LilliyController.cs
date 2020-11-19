using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilliyController : MonoBehaviour
{
    public float speed = 6.0F;       //歩行速度
    public float jumpSpeed = 8.0F;   //ジャンプ力
    public float gravity = 20.0F;    //重力の大きさ

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private float h, v;
    public float move_speed ;
    public GameObject mesh;
    public GameObject bone;

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

        //h = Input.GetAxis("Horizontal");    //左右矢印キーの値(-1.0~1.0)
        //v = Input.GetAxis("Vertical");      //上下矢印キーの値(-1.0~1.0)
        if (Input.GetKey(KeyCode.A))
        {
            h = -1;
            mesh.transform.localScale = new Vector3(1, 1, 1);
            bone.transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("dash", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            h = 1;
            mesh.transform.localScale = new Vector3(-1, 1, 1);
            bone.transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("dash", true);
        }
        else
        {
            h = 0;
            animator.SetBool("dash", false);

        }

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(h * move_speed, 0, v * move_speed);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetKey(KeyCode.Space))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

    }//Update()
}
