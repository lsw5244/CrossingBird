using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //[SerializeField]
    //private Transform _playerTransform;
    //public float playerFollowSpeed = 0.05f;

    //public float moveSpeed = 5f;

    //void Update()
    //{
    //    transform.position = Vector3.Lerp(transform.position, _playerTransform.position, playerFollowSpeed);
    //}

    [SerializeField]
    private Transform _playerTransform;

    public float playerFollowSpeed = 0.01f;
    public float moveSpeed = 1f;

    private Vector3 _nextPosition;

    private void Start()
    {
        _nextPosition = Vector3.zero;
    }

    private void Update()
    {
        _nextPosition.x = Mathf.Lerp(transform.position.x, _playerTransform.position.x, playerFollowSpeed);
        _nextPosition.z += moveSpeed * Time.deltaTime;

        transform.position = _nextPosition;
    }
}
