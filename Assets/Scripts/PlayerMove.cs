using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator _animator;

    private string _jumpAnimationName;

    [SerializeField]
    private float _moveTime = 0.1f;

    private Quaternion _left = Quaternion.Euler(0f, 270f, 0f);
    private Quaternion _right = Quaternion.Euler(0f, 90f, 0f);
    private Quaternion _foward = Quaternion.Euler(0f, 0f, 0f);
    private Quaternion _back = Quaternion.Euler(0f, 180f, 0f);

    private Vector3 _prevPosition;
    private Vector3 _nextPosition;

    private Quaternion _prevRotation;
    private Quaternion _nextRotation;

    private bool _canMove = true;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _jumpAnimationName = "Jump";

        _prevPosition = transform.position;
        _prevRotation = _foward;
    }

    void Update()
    {
        if(_canMove == true)
        {            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _nextPosition = transform.position + Vector3.left;
                _nextRotation = _left;
                StartCoroutine(Move(_moveTime));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _nextPosition = transform.position - Vector3.left;
                _nextRotation = _right;
                StartCoroutine(Move(_moveTime));
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _nextPosition = transform.position + Vector3.forward;
                _nextRotation = _foward;
                StartCoroutine(Move(_moveTime));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _nextPosition = transform.position - Vector3.forward;
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

            transform.position = Vector3.Lerp(_prevPosition, _nextPosition, runTime / moveTime);
            transform.rotation = Quaternion.Lerp(_prevRotation, _nextRotation, runTime / moveTime);

            yield return null;
        }
        _prevPosition = _nextPosition;
        _prevRotation = _nextRotation;

        _canMove = true;
    }
}
