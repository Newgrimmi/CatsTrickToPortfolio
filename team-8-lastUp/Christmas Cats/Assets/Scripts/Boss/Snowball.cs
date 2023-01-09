using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    [SerializeField] private GameObject explosionCollider; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            GameObject explosion = Instantiate(explosionCollider, transform.position, Quaternion.identity);
            Destroy(explosion, 0.1f);
            Destroy(gameObject);
        }
    }
}
