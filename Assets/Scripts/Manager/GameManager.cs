using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using State;
using State.Interface;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] internal Player player;
    [SerializeField] internal CameraManager cameraManager;
    [SerializeField] internal ControlManager controlManager;

    [SerializeField] private IManager[] _managers;

// CompleteInitList
    private void Start()
    {
        Init();
    }

    void Init()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _managers = GetComponentsInChildren<IManager>();
        Array.ForEach(_managers, manager => manager.Init());
        player.Init();
    }

    // //test
    // private string nextSceneName;
    //
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Alpha1))
    //     {
    //         nextSceneName = "MainScene";
    //         LoadNextScene();
    //     }
    //
    //     if (Input.GetKeyDown(KeyCode.Alpha2))
    //     {
    //         nextSceneName = "MainScene2";
    //         LoadNextScene();
    //     }
    // }
    //
    // public void LoadNextScene()
    // {
    //     // 비동기적으로 Scene을 불러오기 위해 Coroutine을 사용한다.
    //     StartCoroutine(LoadMyAsyncScene());
    // }
    //
    // IEnumerator LoadMyAsyncScene()
    // {
    //     // AsyncOperation을 통해 Scene Load 정도를 알 수 있다.
    //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);
    //
    //     // Scene을 불러오는 것이 완료되면, AsyncOperation은 isDone 상태가 된다.
    //     while (!asyncLoad.isDone)
    //     {
    //         yield return null;
    //     }
    //
    //     Init();
    // }
    // //test
}