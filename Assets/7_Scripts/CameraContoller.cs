using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("基本的にリリィを選択する")]
    public GameObject TargetObject;

    [Header("カメラをどれくらいプレイヤーの頭上に置くか")]
    public float Camera_Y_offset = 0.5f;

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
        CameraWaitCount = 99999;//CameraResetCountより上の値にしておく

        //プレイヤーの座標を取得
        PlayerTrans = TargetObject.GetComponent<Transform>();
        CamTransform = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //■プレイヤーが左入力を行っている
        if(LilliyController.playerLeftAngle){
            CameraWaitCount = 0;
            //カメラ移動中
            if(CameraSlideCount < CameraSlideWaitIN){//カメラの移動中
                CameraSlideCount++;
                //this.transform.position = new Vector3(PlayerTrans.position.x + (CameraSlideLeft*(CameraSlideCount /CameraSlideWaitIN) ),
                //                                                         PlayerTrans.position.y + Camera_Y_offset, this.transform.position.z);
                this.transform.position = new Vector3(this.transform.position.x + ((CameraSlideLeft+(PlayerTrans.position.x -CamTransform.x))*(CameraSlideCount /CameraSlideWaitIN) ),
                                                                         PlayerTrans.position.y + Camera_Y_offset, this.transform.position.z);
                CamTransform = this.transform.position;
            }
            else{
                //カメラが指定位置までずれているのでそのままオフセットを効かせて追従
                this.transform.position = new Vector3(PlayerTrans.position.x + CameraSlideLeft , PlayerTrans.position.y + Camera_Y_offset, this.transform.position.z);
                //入力を辞めた際のカメラの位置を保存
                CamTransform = this.transform.position;
            }

        //■プレイヤーが右入力を行っている
        }else if(LilliyController.playerRightAngle){
            CameraWaitCount = 0;
            //カメラ移動中
            if(CameraSlideCount < CameraSlideWaitIN){
                CameraSlideCount++;
                //this.transform.position = new Vector3(PlayerTrans.position.x + (CameraSlideRight*(CameraSlideCount /CameraSlideWaitIN) ), 
                //                                                         PlayerTrans.position.y + Camera_Y_offset, this.transform.position.z);
                this.transform.position = new Vector3(this.transform.position.x + ((CameraSlideRight+(PlayerTrans.position.x -CamTransform.x))*(CameraSlideCount /CameraSlideWaitIN) ),
                                                                         PlayerTrans.position.y + Camera_Y_offset, this.transform.position.z);
                CamTransform = this.transform.position;
            }else{
                //カメラが指定位置までずれているのでそのままオフセットを効かせて追従
                this.transform.position = new Vector3(PlayerTrans.position.x + CameraSlideRight , PlayerTrans.position.y + Camera_Y_offset, this.transform.position.z);
                //入力を辞めた際のカメラの位置を保存
                CamTransform = this.transform.position;
            }

        //■プレイヤーが入力を辞めている
        }else{
            CameraSlideCount = 0;
            
            //カメラをもとの位置に戻してヨシ！
           if(CameraWaitCount>=CameraResetCount){
               //カメラが移動中
                if(CameraWaitCount < (CameraSlideWaitOUT + CameraResetCount)){
                    //カメラを指定座標へフレーム数を掛けて移動
                    this.transform.position = new Vector3(CamTransform.x + ((PlayerTrans.position.x - CamTransform.x )*((CameraWaitCount-CameraResetCount)/CameraSlideWaitOUT)), 
                                                                                     PlayerTrans.position.y + Camera_Y_offset, this.transform.position.z);
                }else{
                    //プレイヤーが一切操作してないし、カメラの移動もしてない時はプレイヤーの位置を維持する
                    this.transform.position = new Vector3(PlayerTrans.position.x, PlayerTrans.position.y + Camera_Y_offset, this.transform.position.z);
                }
            }else{
                //ジャンプ中に操作を辞めるとカメラが追従しなかったので命令
                this.transform.position = new Vector3(CamTransform.x, PlayerTrans.position.y + Camera_Y_offset, this.transform.position.z);
                //CamTransform = this.transform.position;
            }
            CameraWaitCount++;
        }
        
        
    }
}
