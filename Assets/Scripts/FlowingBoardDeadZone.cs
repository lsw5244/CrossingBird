using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowingBoardDeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
