using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator _animator;

    private string _jumpAnimationName;

    [SerializeField]
    private float _moveTime = 0.1f;

    [SerializeField]
    private LayerMask _blockLayer;

    private Quaternion _left = Quaternion.Euler(0f, 270f, 0f);
    private Quaternion _right = Quaternion.Euler(0f, 90f, 0f);
    private Quaternion _foward = Quaternion.Euler(0f, 0f, 0f);
    private Quaternion _back = Quaternion.Euler(0f, 180f, 0f);

    private Vector3 _prevPosition;
    private Vector3 _nextPosition;

    private Quaternion _prevRotation;
    private Quaternion _nextRotation;
    private Transform _nextBlockTransform;

    private bool _canMove = true;

    void Start()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f, _blockLayer))
        {
            _nextBlockTransform = hit.transform;
        }

        _animator = GetComponent<Animator>();
        _jumpAnimationName = "Jump";

        _prevPosition = transform.position;
        _nextPosition = transform.position;

        _prevRotation = _foward;
        _nextRotation = _foward;
    }

    void Update()
    {
        if (_canMove == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SearchNextPosition(Vector3.left);
                _nextRotation = _left;
                StartCoroutine(Move(_moveTime));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SearchNextPosition(Vector3.right);
                _nextRotation = _right;
                StartCoroutine(Move(_moveTime));
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SearchNextPosition(Vector3.forward);
                _nextRotation = _foward;
                StartCoroutine(Move(_moveTime));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SearchNextPosition(Vector3.back);
                _nextRotation = _back;
                StartCoroutine(Move(_moveTime));
            }
        }
    }

    IEnumerator Move(float moveTime)
    {
        _animator.SetTrigger(_jumpAnimationName);
        _canMove = false;

        float runTime = 0.0f;

        while (runTime < moveTime)
        {
            runTime += Time.deltaTime;

            _nextPosition.x = _nextBlockTransform.position.x;
            _nextPosition.z = _nextBlockTransform.position.z;

            transform.position = Vector3.Lerp(_prevPosition, _nextPosition, runTime / moveTime);
            transform.rotation = Quaternion.Lerp(_prevRotation, _nextRotation, runTime / moveTime);

            yield return null;
        }

        _canMove = true;
    }

    void SearchNextPosition(Vector3 direction)
    {
        _prevPosition = transform.position;
        _prevRotation = transform.rotation;

        RaycastHit hit;

        // 장애물 검색
        if (Physics.Raycast(transform.position + Vector3.up, direction, out hit, 1f))
        {
            return;
        }

        // 블럭 검색
        if (Physics.Raycast(transform.position + Vector3.up + direction, Vector3.down, out hit, 2f, _blockLayer))
        {
            _nextBlockTransform = hit.transform;

            transform.parent = null;
            transform.parent = _nextBlockTransform;
        }
    }
}
