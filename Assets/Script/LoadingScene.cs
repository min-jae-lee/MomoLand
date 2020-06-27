using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{

    //로딩씬
    public Text loadingValue;
    public Image image1;
    public Image image2;
    public Image image3;
    private int ranNumber;

    //로딩 슬라이더 인스펙터에 공개
    [SerializeField]
    Slider loadingBar;


    void Start()
    {
        image1.enabled = false;
        image2.enabled = false;
        image3.enabled = false;
        //3개의 이미지중 랜덤으로 1개의 이미지를 로딩씬의 배경이미지로 사용
        ranNumber = Random.Range(0, 3);
        switch (ranNumber)
        {
            case 0: image1.enabled = true;
                break;
            case 1: image2.enabled = true;
                break;
            case 2: image3.enabled = true;
                break;
        }
        loadingBar.value = 0;
        StartCoroutine(LoadAsyncScene());
    }

    //AsyncOperation으로 비동기적 로딩씬 구현
    IEnumerator LoadAsyncScene()
    {
        
        yield return null;
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync("Scene");
        asyncScene.allowSceneActivation = false;
        float timer = 0;
        float fakeTimer = 95;
        float progressValue;
        while (!asyncScene.isDone)
        {
            //AsyncOperation의 progress가 0.9가 되면 95부터 100까지 초단위로 로딩을 흐르게하여 유저가 배경화면의 정보를 취득할 시간을 줌
            yield return null;
            timer += Time.deltaTime;
            if(asyncScene.progress >= 0.9f)
            {
                fakeTimer += Time.deltaTime;
                loadingBar.value = fakeTimer;
                loadingValue.text = "Loading:" + (int)fakeTimer + "%";
                //loadingBar.value = Mathf.Lerp(loadingBar.value, 100, timer);
                if (loadingBar.value == 100f)
                { 
                        yield return new WaitForSeconds(1f);
                        asyncScene.allowSceneActivation = true;  
                }
            }
            else
            {
                progressValue = asyncScene.progress;
                loadingValue.text = "Loading:" + (int)progressValue * 100 + "%";
                loadingBar.value = Mathf.Lerp(loadingBar.value, asyncScene.progress, timer);
                if(loadingBar.value >= asyncScene.progress)
                {
                    timer = 0f;
                }
            }
        }
    }

}
