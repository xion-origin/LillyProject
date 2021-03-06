﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSrecord : MonoBehaviour
{
    private float fps;
    public Text fps_text;

    public bool Debugtrigger = false;
    // Start is called before the first frame update
    private void Awake()
    {
        Application.targetFrameRate = 60; //60FPSに設定
    }
    void Start()
    {
        fps_text =  this.GetComponent<Text>();
    }


    private void Update()
    {
        fps = 1f / Time.deltaTime;
        fps_text.text = fps.ToString();
        if(Debugtrigger){
        Debug.Log("画面横サイズ : " + Screen.width + "px" );
        Debug.Log("画面縦サイズ : " + Screen.height + "px");
        }

    }
}
