using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryTarget : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            this.gameObject.SetActive(false);
            Debug.Log("Get damage " + collision.gameObject.name + "=" + collision.gameObject.GetComponent<Arrow>().GetDamage());
        }
    }
}
