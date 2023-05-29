using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BlockGenerator : MonoBehaviour
{
    public GameObject[] blocks;//ブロック取得配列
    public Camera mainCamera;//カメラ取得用変数
    public float pivotHeight = 3;//生成位置

    public static int blockNum = 0;//生成されたブロックの個数を保管
    public static bool isGameOver = false;//ゲームオーバー判定

    private GameObject geneBlock;//ブロック生成（単品）
    public bool isGene;//生成されているか
    public bool isFall;//生成された動物が落下中か
    //
    public TextMeshProUGUI score;
    public int count;
    //
    /*
    const int DefaultLife = 3;
    int life = DefaultLife;
    public int life()
    {
        return life;
    }
    public void LifeCount()
    {
        if(ture)
    }
    */
//    public LifePanel lifePanel;

    //
    //Application.targetFrameRate = 60;

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Init()
    {
        blockNum = 0;
        isGameOver = false;
        Block.isMoves.Clear();//移動してるブロックのリストを初期化
        StartCoroutine(StateReset());
        //
        count = -1;//初期スコア
        //
    }

    // Update is called once per frame
    // 毎フレーム呼び出される(60fpsだったら1秒間に60回)
    void Update () {
        //
        //スコアの更新
        score.text = count.ToString();
        //ライフパネルの更新
//        lifePanel.UpdateLife(Block.Life());
        //


        //if (Input.GetMouseButton(0)){
/*            if (isGameOver)
        {
            Invoke("ReturnToTitle", 2.0f);
            //return;//ゲームオーバーならここで止める
        }
*/
        if (CheckMove(Block.isMoves))
        {
            return;//移動中なら処理はここまで
        }

        if (!isGene)//生成されてるものがない
        {
            StartCoroutine(GenerateBlock());//生成するコルーチンを動かす
            isGene = true;
            count++;
            return;
        }

        Vector2 v = new Vector2(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, pivotHeight);

        if (Input.GetMouseButtonUp(0))//もし（マウス左クリックが離されたら）
        {
            if(Screen.height/4 < Input.mousePosition.y && Screen.height*0.9 > Input.mousePosition.y)
            {
                if (!RotateButton.onButtonDown)//ボタンをクリックしていたら反応させない
            {
                geneBlock.transform.position = v;
                //geneBlock.GetComponent<Rigidbody2D>().isKinematic = false;//――――物理挙動・オン
                //blockNum++;//ブロック生成
                //isFall = true;//落ちて、どうぞ
                
            }
            RotateButton.onButtonDown = false;//マウスが上がったらボタンも離れたと思う
            }
            
        }
        else if(Input.GetMouseButton(0))//ボタンが押されている間
        {
            if(Screen.height/4 < Input.mousePosition.y && Screen.height*0.9 > Input.mousePosition.y)
            {
                geneBlock.transform.position = v;
            }
            
        }
        //}

        //ハイスコア
        if(PlayerPrefs.GetInt("HighScore") < count)
        {
            PlayerPrefs.SetInt("HighScore", count);
        }

        
	}

    /// <summary>
    /// 生成・落下状態をリセットするコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator StateReset()
    {
        while (!isGameOver)
        {
            yield return new WaitUntil(() => isFall);//落下するまで処理が止まる
            yield return new WaitForSeconds(0.1f);//少しだけ物理演算処理を待つ（ないと無限ループ）
            isFall = false;
            isGene = false;
        }
    }
    
    /// <summary>
    /// ブロックの生成コルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator GenerateBlock()
    {
        while (CameraController.isCollision)
        {
            yield return new WaitForEndOfFrame();//フレームの終わりまで待つ（無いと無限ループ）
            mainCamera.transform.Translate(0,0.1f,0);//カメラを少し上に移動
            pivotHeight += 0.1f;//生成位置も少し上に移動
        }
        //yield return new WaitForSeconds(1);
        geneBlock = Instantiate(blocks[Random.Range(0, blocks.Length)], new Vector2(0, pivotHeight), Quaternion.identity);//回転せずに生成
        geneBlock.GetComponent<Rigidbody2D>().isKinematic = true;//物理挙動をさせない状態にする
    }

    /// <summary>
    /// ブロックの落下
    /// ボタンにつけて使います
    /// </summary>
    public void DropBlock()
    {
        geneBlock.GetComponent<Rigidbody2D>().isKinematic = false;//――――物理挙動・オン
                blockNum++;//ブロック生成
                isFall = true;//落ちて、どうぞ

                //
                //count++;//スコア更新
                //
    }

    /// <summary>
    /// ブロックの回転
    /// ボタンにつけて使います
    /// </summary>
    public void RotateBlockLeft()
    {
        if(isGene)
        geneBlock.transform.Rotate(0,0,30);
        //    geneBlock.transform.Rotate(0,0,-30);//30度ずつ回転
    }

    /// <summary>
    /// ブロックの回転
    /// ボタンにつけて使います
    /// </summary>
    public void RotateBlockRight()
    {
        if(isGene)
        geneBlock.transform.Rotate(0,0,-30);
        //    geneBlock.transform.Rotate(0,0,-30);//30度ずつ回転
    }

    /// <summary>
    /// リトライボタン
    /// </summary>
    public void Retry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("game");
    }

    /// <summary>
    /// 移動中かチェック
    /// </summary>
    /// <param name="isMoves"></param>
    /// <returns></returns>
    bool CheckMove(List<Moving> isMoves)
    {
        if (isMoves == null)
        {
            return false;
        }
        foreach (Moving b in isMoves)
        {
            if (b.isMove)
            {
                //Debug.Log("移動中(*'ω'*)");
                return true;
            }
        }
        return false;
    }
    //
}

