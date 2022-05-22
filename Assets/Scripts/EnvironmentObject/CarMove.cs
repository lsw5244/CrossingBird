using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    private float _moveSpeed = 10f;

    void Update()
    {
        //transform.Translate(transform.forward * _moveSpeed * Time.deltaTime);
        transform.position += transform.right * _moveSpeed * Time.deltaTime;
    }


}
