using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public static List<Moving> isMoves = new List<Moving>();//移動してるかチェックするリスト
    public int Life = 3;

    Rigidbody2D rigid;
    Moving moving = new Moving();//移動チェック変数

    /// <summary>
    /// リストに追加&Rigidbody2D取得
    /// </summary>

    // Start is called before the first frame update
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        isMoves.Add(moving);
    }

    /// <summary>
    /// 固定フレームレートで移動チェック
    /// </summary>
    void FixedUpdate () {
        if(rigid.velocity.magnitude > 0.01f)//少しでも移動していれば動いてると判定
        {
            //Debug.Log("move");
            moving.isMove = true;
        }
        else
        {
            //Debug.Log("stop");
            moving.isMove = false;
        }
	}
}

/// <summary>
/// 移動チェッククラス
/// 
/// </summary>
public class Moving
{
    public bool isMove;
}

