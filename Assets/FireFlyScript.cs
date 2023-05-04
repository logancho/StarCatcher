using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jar"))
        {
            Debug.Log("firefly hit jar");
            Destroy(gameObject);
        }
    }
}
