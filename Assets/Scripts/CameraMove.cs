using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;
    public float playerFollowSpeed = 0.05f;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _playerTransform.position, playerFollowSpeed);
    }
}
