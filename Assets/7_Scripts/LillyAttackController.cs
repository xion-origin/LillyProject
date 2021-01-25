using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LillyAttackController : MonoBehaviour
{
    //本体だからいらなかった？
    //public LilliyController LilliyController;
　　//外には出さないデータ
    //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    private CharacterController controller;
    Animator animator;
    //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)//地上
        {
            if (Input.GetKeyDown("space")){

            }
        }
    }
}
