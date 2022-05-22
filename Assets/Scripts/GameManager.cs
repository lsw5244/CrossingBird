using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [SerializeField]
    private PlayerMove _playerMove;
    [SerializeField]
    private CameraMove _cameraMove;
    void Awake()
    {
        if (Instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogWarning("씬에 두개 이상의 게임매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        UIManager.Instance.GameOver();

        _playerMove.enabled = false;

        _cameraMove.CameraStop();        
    }
}
