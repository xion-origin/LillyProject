using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("基本的にリリィを選択する")]
    public GameObject TargetObject;

    [Header("カメラをどれくらいプレイヤーの頭上に置くか")]
    public float CameraYUP = 0.5f;

    [Header("カメラをどれくらい右にスライドするか")]
    public float CameraSlideRight ;

    [Header("カメラをどれくらい左にスライドするか")]
    public float CameraSlideLeft ;

    [Header("どれだけ入力しないとカメラ位置をセンターにするか")]
    public int CameraResetCount = 30;

    [Header("カメラのスライド速度IN")]
    public int CameraSlideWaitIN = 30;

    [Header("カメラのスライド速度OUT")]
    public int CameraSlideWaitOUT = 30;


    Vector3 CamTransform;
    Transform PlayerTrans;
    
    private　float CameraSlideCount;
    private float CameraWaitCount;

    public LilliyController LilliyController;

    void Start()
    {
        CameraSlideCount = 0;

        //プレイヤーの座標を取得
        PlayerTrans = TargetObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(LilliyController.playerLeftAngle){//プレイヤーが左に移動する
            CameraWaitCount = 0;
            if(CameraSlideCount < CameraSlideWaitIN){//カメラの移動中
                CameraSlideCount++;
                this.transform.position = new Vector3(PlayerTrans.position.x + (CameraSlideLeft*(CameraSlideCount /CameraSlideWaitIN) ),
                                                                         PlayerTrans.position.y + CameraYUP, this.transform.position.z);
                Debug.Log("check:Left");
            }
            else{
                CameraSlideCount = CameraSlideWaitIN;
                this.transform.position = new Vector3(PlayerTrans.position.x + (CameraSlideLeft*(CameraSlideCount /CameraSlideWaitIN) ),
                                                                         PlayerTrans.position.y + CameraYUP, this.transform.position.z);
            }
        }else if(LilliyController.playerRightAngle){//プレイヤーが右に移動する
            CameraWaitCount = 0;
            if(CameraSlideCount < CameraSlideWaitIN){//カメラの移動中
                CameraSlideCount++;
                this.transform.position = new Vector3(PlayerTrans.position.x + (CameraSlideRight*(CameraSlideCount /CameraSlideWaitIN) ), 
                                                                         PlayerTrans.position.y + CameraYUP, this.transform.position.z);
                Debug.Log("check:Right");
            }else{
                CameraSlideCount = CameraSlideWaitIN;
                this.transform.position = new Vector3(PlayerTrans.position.x + (CameraSlideRight*(CameraSlideCount /CameraSlideWaitIN) ), 
                                                                         PlayerTrans.position.y + CameraYUP, this.transform.position.z);
            }
        }else{
            
            CameraSlideCount = 0;
            
            CameraWaitCount++;
            if(CameraWaitCount>CameraResetCount){
                if(CameraWaitCount < (CameraSlideWaitOUT + CameraResetCount)){
                    this.transform.position = new Vector3(PlayerTrans.position.x + ((PlayerTrans.position.x - this.transform.position.x )/(CameraWaitCount/CameraResetCount)), 
                                                                          PlayerTrans.position.y + CameraYUP, this.transform.position.z);
                //this.transform.position = new Vector3(PlayerTrans.position.x, PlayerTrans.position.y + CameraYUP, this.transform.position.z);
                }
            }
            
            //カメラの座標をプレイヤーの頭上＋して表示(これだけがしっかりしてる)
            //this.transform.position = new Vector3(PlayerTrans.position.x, PlayerTrans.position.y + CameraYUP, this.transform.position.z);
        }
        
        
    }
}
