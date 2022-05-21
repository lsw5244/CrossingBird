using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public float _moveSpeed = 40f;
    [SerializeField]
    private TrainLine _parentRoadLine;

    void Update()
    {
        //transform.Translate(transform.forward * _moveSpeed * Time.deltaTime);
        transform.position += Vector3.left * _moveSpeed * Time.deltaTime;
    }

    private void OnDisable()
    {
        _parentRoadLine.TurnOnGreenTrafficLight();
    }
}
