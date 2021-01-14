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
        CameraWaitCount = 99999;
        //プレイヤーの座標を取得
        PlayerTrans = TargetObject.GetComponent<Transform>();
        CamTransform = this.transform.position;
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
                CamTransform = this.transform.position;
            }
            else{
                CameraSlideCount = CameraSlideWaitIN;
                this.transform.position = new Vector3(PlayerTrans.position.x + (CameraSlideLeft*(CameraSlideCount /CameraSlideWaitIN) ),
                                                                         PlayerTrans.position.y + CameraYUP, this.transform.position.z);
                CamTransform = this.transform.position;
            }
        }else if(LilliyController.playerRightAngle){//プレイヤーが右に移動する
            CameraWaitCount = 0;
            if(CameraSlideCount < CameraSlideWaitIN){//カメラの移動中
                CameraSlideCount++;
                this.transform.position = new Vector3(PlayerTrans.position.x + (CameraSlideRight*(CameraSlideCount /CameraSlideWaitIN) ), 
                                                                         PlayerTrans.position.y + CameraYUP, this.transform.position.z);
                CamTransform = this.transform.position;
            }else{
                CameraSlideCount = CameraSlideWaitIN;
                this.transform.position = new Vector3(PlayerTrans.position.x + (CameraSlideRight*(CameraSlideCount /CameraSlideWaitIN) ), 
                                                                         PlayerTrans.position.y + CameraYUP, this.transform.position.z);
                CamTransform = this.transform.position;
            }
        }else{//プレイヤーが左右に移動しようとしてない
            CameraSlideCount = 0;
            
            //カメラリセットカウントに待機時間が達する
           if(CameraWaitCount>=CameraResetCount){
               //カメラの移動時間中か
                if(CameraWaitCount < (CameraSlideWaitOUT + CameraResetCount)){
                    //カメラを指定座標へフレーム数を掛けて移動
                    this.transform.position = new Vector3(CamTransform.x + ((PlayerTrans.position.x - CamTransform.x )*((CameraWaitCount-CameraResetCount)/CameraSlideWaitOUT)), 
                                                                                     PlayerTrans.position.y + CameraYUP, this.transform.position.z);
                }else{
                    //本来は必要ないはずだがカメラがなんかバグるので書いとく（まじでこういうこと良くない
                    this.transform.position = new Vector3(PlayerTrans.position.x, PlayerTrans.position.y + CameraYUP, this.transform.position.z);
                }
            }else{
                //ジャンプ中に操作を辞めるとカメラが追従しなかったので命令
                this.transform.position = new Vector3(CamTransform.x, PlayerTrans.position.y + CameraYUP, this.transform.position.z);
            }
            CameraWaitCount++;
        }
        
        
    }
}
