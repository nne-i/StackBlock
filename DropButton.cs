using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DropButton : MonoBehaviour, IPointerDownHandler
{
    public static bool onButtonDown;//ボタンがクリックされてるかを判定
    /*
    private bool buttonEnabled = true;
    private WaitForSeconds waitOneSecond = new WaitForSeconds(3.0f);
*/
    /// <summary>
    /// ボタンが押されたと検出
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        onButtonDown = true;
        /*if(buttonEnabled == false){
            return;
        }
        else{
            onButtonDown = true;
            buttonEnabled = false;
            StartCoroutine(EnableButton());
        }*/
        
    }

    
    /*private IEnumerator EnableButton()
    {
        // 1秒後に解除         
        yield return waitOneSecond;
        buttonEnabled = true;
    }*/
}
