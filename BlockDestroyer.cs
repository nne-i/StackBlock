using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BlockDestroyer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.tag == "Block")
       // {
            //ブロックの削除
            Destroy(other.gameObject);

            Invoke("ReturnToTitle", 2.0f);

        //}
    }
    void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
