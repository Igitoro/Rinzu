using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class SceneChanger : MonoBehaviour
{
    public GameObject LoadingScreen;

    public Slider bar;

    public GameObject Text;

    public GameObject CutScene;

    public void ChangeScene(int scene)
    {
        if(LoadingScreen != null)
        {
            LoadingScreen.SetActive(true);

            StartCoroutine(LoadAsync(scene));
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator LoadAsync(int scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            bar.value = asyncLoad.progress * 100;

            Text.GetComponent<Text>().text = "Загрузка " + asyncLoad.progress * 100 + "%";

            if (asyncLoad.progress >= .9f && !asyncLoad.allowSceneActivation)
            {
                if (asyncLoad.progress >= .89f)
                {
                    Text.GetComponent<Text>().text = "Загрузка 100%";
                }

                Text.GetComponent<Text>().text = "Нажмите на экран";

                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    if (Input.anyKeyDown || touch.phase == TouchPhase.Began)
                    {
                      asyncLoad.allowSceneActivation = true; 
                    }
                }

            }

            yield return null;
        }
    }
}