using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : PlayerFollowingObject
{
    [SerializeField]
    private Transform _playerTransform;

    public float playerFollowSpeed = 0.01f;
    [SerializeField]
    private float _orignMoveSpeed = 1f;
    //private float moveSpeed = 1f;
    public float acceleratedMoveSpeed = 5f;
    public float moveSpeedAccelerationDistatnce = 4f;

    private Vector3 _nextPosition;

    private void Start()
    {
        moveSpeed = _orignMoveSpeed;
        _nextPosition = Vector3.zero;
    }

    private void Update()
    {
        if(_playerTransform.position.z - transform.position.z > moveSpeedAccelerationDistatnce)
        {
            moveSpeed = acceleratedMoveSpeed;
        }
        else
        {
            moveSpeed = _orignMoveSpeed;
        }

        _nextPosition.x = Mathf.Lerp(transform.position.x, _playerTransform.position.x, playerFollowSpeed);
        _nextPosition.z += moveSpeed * Time.deltaTime;

        transform.position = _nextPosition;
    }
}
