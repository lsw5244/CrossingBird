using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTTTTT : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("Enable!!!");
    }

    private void OnDisable()
    {
        Debug.Log("Disable@@@");
        gameObject.SetActive(false);
    }
}
