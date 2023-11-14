using System;
using System.Collections;
using System.Collections.Generic;
using SL;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameComplete : MonoBehaviour
{
    [SerializeField] private GameObject uiToStart, uiEndGame;
    [SerializeField] private SpawnerController spawnerController;
    private TeaTime _beforeStart, _start, _end;
    private bool _isStart;
    private bool _isPlayerDead;

    private void Start()
    {
        uiEndGame.SetActive(false);
        uiToStart.SetActive(true);
        _beforeStart = this.tt().Pause().Add(() =>
        {
            //Debug.Log("Before Start");
        }).Wait(()=> _isStart, 0.1f).Add(() =>
        {
            //Debug.Log("Start Play");
            uiToStart.SetActive(false);
            _start.Play();
        });
        _start = this.tt().Pause().Add(() =>
        {
            //Debug.Log("Start");
            spawnerController.StartSpawn();
        }).Wait(()=> !ServiceLocator.Instance.GetService<ILifeOfPlayer>().IsAlive(), 0.1f).Add(() =>
        {
            ServiceLocator.Instance.GetService<ISpawnController>().EndSpawn();
            _end.Play();
        });
        _end = this.tt().Pause().Add(() =>
        {
            _isStart = false;
            //Debug.Log("End");
            uiEndGame.SetActive(true);
        }).Wait(()=>true,10).Add(() =>
        {
            if (_isStart)
            {
                //logic for restart, end, add life. 
            }
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        _beforeStart.Play();
    }
    
    public void StartGame()
    {
        _isStart = true;
    }
    
    public void EndGame()
    {
        _isPlayerDead = true;
    }
}