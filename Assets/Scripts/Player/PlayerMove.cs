using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator _animator;

    private string _jumpAnimationName;
    private string _flowingBoardTag;

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
    
    [HideInInspector]
    public int progress = 0;
    private int maxProgress = 0;

    private bool _changeNextPosition = true;

    private bool _wait;
    private Vector3 _firstTouchPosition;
    private Vector3 _TouchPositionGap;

    void Start()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f, _blockLayer))
        {
            _nextBlockTransform = hit.transform;
        }

        _animator = GetComponent<Animator>();
        _jumpAnimationName = "Jump";
        _flowingBoardTag = "FlowingBoard";

        _prevPosition = transform.position;
        _nextPosition = transform.position;

        _prevRotation = _foward;
        _nextRotation = _foward;
    }

    
    void Update()
    {
        if (_canMove == true)
        {
            TouchMove();

            KeyboardMove();
            
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
        _changeNextPosition = true;

        RaycastHit hit;

        // 장애물 검색
        if (Physics.Raycast(transform.position + Vector3.up, direction, out hit, 1f))
        {
            _changeNextPosition = false;
            if(hit.collider.CompareTag("AttackObject"))
            {
                GameManager.Instance.GameOver();
                GetComponent<BoxCollider>().isTrigger = true;
                GetComponent<Rigidbody>().AddForce(hit.transform.position - transform.position * 100f);
            }
            return;
        }

        // 블럭 검색
        if (Physics.Raycast(transform.position + Vector3.up + direction, Vector3.down, out hit, 2f, _blockLayer))
        {
            _nextBlockTransform = hit.transform;

            transform.parent = null;
            transform.parent = _nextBlockTransform;

            if(hit.collider.tag == _flowingBoardTag)
            {
                hit.collider.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }

    void TouchMove()
    {
        // 처음 터치 한 위치 저장  // 터치 한손으로 && 터치발생상태 == 터치시작
        if (Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began))  // 확인
        {
            _wait = true;
            _firstTouchPosition = Input.GetMouseButtonDown(0) ? Input.mousePosition : (Vector3)Input.GetTouch(0).position;
        }

        // 움직인 부분에서 처음 터치한 부분을 -하여 첫번째 터치와 두번째 터치의 gap 저장
        if (Input.GetMouseButton(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            _TouchPositionGap = (Input.GetMouseButton(0) ? Input.mousePosition : (Vector3)Input.GetTouch(0).position) - _firstTouchPosition;

            if (_TouchPositionGap.magnitude < 100)
            {
                return;
            }

            _TouchPositionGap.Normalize();

            if (_wait == true)
            {
                _wait = false;

                if (_TouchPositionGap.y > 0 && _TouchPositionGap.x > -0.5f && _TouchPositionGap.x < 0.5f)
                {
                    // 앞으로 전진
                    SearchNextPosition(Vector3.forward);
                    if (_changeNextPosition == true)
                    {
                        ++progress;
                    }

                    if (progress > maxProgress)
                    {
                        maxProgress = progress;
                        UIManager.Instance.UpdateScore();
                    }

                    _nextRotation = _foward;
                    StartCoroutine(Move(_moveTime));
                }
                else if (_TouchPositionGap.y < 0 && _TouchPositionGap.x > -0.5f && _TouchPositionGap.x < 0.5f)
                {
                    // 아래로 드래그 했을 때
                    SearchNextPosition(Vector3.back);
                    if (_changeNextPosition == true)
                    {
                        --progress;
                    }
                    _nextRotation = _back;
                    StartCoroutine(Move(_moveTime));
                }
                else if (_TouchPositionGap.x > 0 && _TouchPositionGap.y > -0.5f && _TouchPositionGap.y < 0.5f)
                {
                    // 오른쪽으로 드래그 했을 때
                    SearchNextPosition(Vector3.right);
                    _nextRotation = _right;
                    StartCoroutine(Move(_moveTime));
                }
                else if (_TouchPositionGap.x < 0 && _TouchPositionGap.y > -0.5f && _TouchPositionGap.y < 0.5f)
                {
                    // 왼쪽으로 드래그 했을 때
                    SearchNextPosition(Vector3.left);
                    _nextRotation = _left;
                    StartCoroutine(Move(_moveTime));
                }
            }
        }
    }

    void KeyboardMove()
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
            if (_changeNextPosition == true)
            {
                ++progress;
            }

            if (progress > maxProgress)
            {
                maxProgress = progress;
                UIManager.Instance.UpdateScore();
            }

            _nextRotation = _foward;
            StartCoroutine(Move(_moveTime));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SearchNextPosition(Vector3.back);
            if (_changeNextPosition == true)
            {
                --progress;
            }
            _nextRotation = _back;
            StartCoroutine(Move(_moveTime));
        }
    }

}
