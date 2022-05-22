using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingDeadZone : PlayerFollowingObject
{
    //public float moveSpeed = 5f;

    void Update()
    {
        transform.Translate(0f, 0f, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}
