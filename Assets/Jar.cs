using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   /* private void OnTriggerEnter(Collider collider)
    {
        // check if debris hits the jar 
        if (collider.CompareTag("Debris")) {
            GameObject obj = GameObject.Find("Global");
            GlobalScript g = obj.GetComponent<GlobalScript>();
            // should decrease the starlight 
            g.DecreaseHealth();

            // call debris die function 
            NewDebrisScript debris = collider.gameObject.GetComponent<NewDebrisScript>();

            Debug.Log("debris hit jar"); 
        } 
    }*/
}
