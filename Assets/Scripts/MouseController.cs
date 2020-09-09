﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField]
    private Transform character;    //キャラクターをInspectorウィンドウから選択
    [SerializeField]
    private Transform pivot;    //キャラクターの中心にある空のオブジェクトを選択

    public float sensiX;
    public float sensiY;

    // Start is called before the first frame update
    void Start()
    {
        //エラーが起きないようにNullだった場合、それぞれ設定
        if (character == null)
            character = transform.parent;
        if (pivot == null)
            pivot = transform;
    }

    //カメラ上下移動の最大、最小角度です。Inspectorウィンドウから設定してください
    [Range(-90.999f, -90.5f)]
    public float maxYAngle = -0.5f;
    [Range(90.5f, 90.999f)]
    public float minYAngle = 0.5f;

    // Update is called once per frame
    void Update()
    {
        //マウスのX,Y軸がどれほど移動したかを取得します
        float X_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = Input.GetAxis("Mouse Y");

        //Y軸を更新します（キャラクターを回転）取得したX軸の変更をキャラクターのY軸に反映します
        character.transform.Rotate(0, X_Rotation * sensiX, 0);
        character.transform.Rotate(0, Y_Rotation * sensiY, 0);
        Debug.Log(Y_Rotation);
        

        //次はY軸の設定です。
        float nowAngle = pivot.transform.localRotation.x;
        
        //最大値、または最小値を超えた場合、カメラをそれ以上動かない用にしています。
        //キャラクターの中身が見えたり、カメラが一回転しないようにするのを防ぎます。
        if (-Y_Rotation != 0)
        {
            Debug.Log("Y軸移動");
            pivot.transform.Rotate(-Y_Rotation * sensiY, 0, 0);
        }
        
    }
}
