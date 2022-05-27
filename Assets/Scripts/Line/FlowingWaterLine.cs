using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowingWaterLine : MonoBehaviour
{
    enum BoardMoveDirection
    {
        Left, Right, End
    }
    private BoardMoveDirection _boardMoveDirection;

    [SerializeField]
    private GameObject _board;
    private GameObject[] _boardPool;
    private GameObject _selectBoard;

    [SerializeField]
    private int _spawnBoardCount = 2;

    private Vector3 _boardSpawnPosition;

    [SerializeField]
    private float _minSpawnDelay = 1.0f;
    [SerializeField]
    private float _maxSpawnDelay = 3.0f;

    private void Awake()
    {
        _boardPool = new GameObject[_spawnBoardCount];
        for (int i = 0; i < _boardPool.Length; ++i)
        {
            _boardPool[i] = Instantiate(_board, _board.transform.position, Quaternion.identity);
            _boardPool[i].transform.parent = transform;
            _boardPool[i].SetActive(false);            
        }
    }

    private void OnEnable()
    {
        _boardMoveDirection = (BoardMoveDirection)Random.Range(0, (int)BoardMoveDirection.End);

        StartCoroutine(SpawnBoard());
    }

    private void OnDisable()
    {
        StopCoroutine(SpawnBoard());
        for (int i = 0; i < _boardPool.Length; ++i)
        {
            _boardPool[i].SetActive(false);
        }
    }

    IEnumerator SpawnBoard()
    {
        while (gameObject.activeSelf == true)
        {
            yield return new WaitForSeconds(Random.Range(_minSpawnDelay, _maxSpawnDelay));

            _selectBoard = GetBoard();

            if(_selectBoard == null)
            {
                continue;
            }

            SetBoardSpawnPosition();
            SetBoardSpawnRotation();

            _selectBoard.SetActive(true);
        }
    }

    GameObject GetBoard()
    {
        for(int i = 0; i < _boardPool.Length; ++i)
        {
            if(_boardPool[i].activeSelf == false)
            {
                return _boardPool[i];
            }
        }

        return null;
    }

    void SetBoardSpawnPosition()
    {
        _boardSpawnPosition.x = _boardMoveDirection == BoardMoveDirection.Right ? -7f : 7f;
        _boardSpawnPosition.y = _selectBoard.transform.position.y;
        _boardSpawnPosition.z = transform.position.z;
        _selectBoard.transform.position = _boardSpawnPosition;
    }

    void SetBoardSpawnRotation()
    {
        if (_boardMoveDirection == BoardMoveDirection.Right)
        {
            _selectBoard.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            _selectBoard.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
}
