using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.parent = null;
            other.isTrigger = true;

            if(gameObject.CompareTag("WaterBlock") == false)
            {
                other.GetComponent<Rigidbody>().AddForce(other.transform.position - transform.position * 100f);
            }

            GameManager.Instance.GameOver();
        }
    }

}
