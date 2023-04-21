using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    // Start is called before the first frame update

    //Fields
    //Lifetime duration -> should call Die when lifetime is up
    public float lifetime;
    float elapsedTime = 0;

    void Start()
    {
        //lifetime = Random.Range(0.5f, 3.0f);
        Debug.Log("bruh");
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > lifetime)
        {
            Die();
        }
    }

    void Die()
    {
        //Spawn particle simulation upon death
        Destroy(gameObject);
    }
}
