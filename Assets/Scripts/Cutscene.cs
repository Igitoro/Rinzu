using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    [SerializeField] Text txt;
    [SerializeField] GameObject Continue;
    [SerializeField] GameObject Skip;
    
    private string TextTxt = "Вас призвали в этот мир, чтобы вы защитили его жителей от наступающей угрозы. Побеждайте монстров и улучшайте свои навыки. Удачи! \nВаша первая задача - убить гоблинов.";
    
    void Start()
    {
        StartCoroutine(c_Output(TextTxt, .1f));
    }

    IEnumerator c_Output(string str, float delay)
    {
        foreach (var sym in str)
        {
            print(sym);
 
            txt.text += sym;

            yield return new WaitForSeconds(delay);
        }
        Continue.SetActive(true);
        Skip.SetActive(false);
    }
}
