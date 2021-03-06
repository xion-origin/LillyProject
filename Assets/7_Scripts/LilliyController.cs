using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilliyController : MonoBehaviour
{
    //インスペクターで弄れるデータ
    //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    [Header("ジャンプ力")]
    public float jumpSpeed = 8.0F;

    [Header("重力の大きさ")]
    public float gravity = 20.0F;

    [Header("移動力")]
    public float move_speed ;

    [Header("操作するゲームオブジェクト")]
    public GameObject LillyObj;

    [Header("ドラッグ入力の横軸感度")]
    public float mousbufx  = 5;

    [Header("ドラッグ入力のジャンプ感度")]
    public float mousbufy =  5;

    public bool  playerLeftAngle;
    public bool  playerRightAngle;
    //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■


    //外には出さないデータ
    //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private float h, v;
    private bool JumpOK = false;
    Vector3 MousePos;
    Vector3 SaveMousePos;
    float mouseposX;
    float mouseposY;
    Animator animator;
    float dragXCheck;
    float dragYCheck;
    //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }//Start()

    // Update is called once per frame
    void Update()
    {
        //マウスの座標を保存
        MousePos = Input.mousePosition;
        dragXCheck = Screen.width  / mousbufx;
        dragYCheck = Screen.height / mousbufy;

        //クリックした瞬間
        if (Input.GetMouseButtonDown(0))
        {
            //クリック位置を保存
            SaveMousePos = MousePos;
            mouseposX = SaveMousePos.x;
            mouseposY = SaveMousePos.y;
        }

        //ドラッグ操作検出
        if (Input.GetMouseButton(0))
        {
            //横移動の検出（クリック位置からある程度ドラッグしたら移動
            if (MousePos.x < mouseposX - dragXCheck)
            {
                h = -1;
                LillyObj.transform.localScale = new Vector3(0.17f, 0.17f, 0.17f);
                animator.SetBool("dash", true);
                
                playerRightAngle = false;
                playerLeftAngle = true;
            }
            else if (MousePos.x > mouseposX + dragXCheck)
            {
                h = 1;
                LillyObj.transform.localScale = new Vector3(-0.17f, 0.17f, 0.17f);
                animator.SetBool("dash", true);
                playerRightAngle = true;
                playerLeftAngle = false;
            }else{
                h = 0;
                animator.SetBool("dash", false);
                playerRightAngle = false;
                playerLeftAngle = false;
            }
        }
        else
        {
            h = 0;
            animator.SetBool("dash", false);
            playerRightAngle = false;
            playerLeftAngle = false;
        }


        if (controller.isGrounded)//地上
        {
            if (MousePos.y < mouseposY + dragYCheck)
            JumpOK = true;  //地上に降りたのでジャンプOKよ
            //ジャンプ入力
            if (Input.GetMouseButton(0))
            {
                if(JumpOK){
                     if (MousePos.y > mouseposY + dragYCheck)
                     {
                         JumpOK = false;
                         animator.SetBool("jump", false);
                         moveDirection.y = jumpSpeed;
                     }
                }
            }
            animator.SetBool("jump", false);
        }
        else//空中
        {
            animator.SetBool("jump", true);
            //上下の移動量の適用
        moveDirection.y -= gravity * Time.deltaTime;
        }

        
        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        
        //左右の移動量の適用
        moveDirection.x = h * move_speed ;
        //最終的な移動量を反映指せる
        controller.Move(moveDirection * Time.deltaTime);
        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

    }//Update()
}
