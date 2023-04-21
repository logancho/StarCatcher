using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    //Fields
    //Star prefab

    //Spawning bounds?
    float ceiling = 10.0f;
    float halfWidth = 5.0f;

    //Spawning timer
    public GameObject StarPrefab;

    float spawnTimer;
    float elapsedTime;

    Vector3 RandomSpawnLocation()
    {
        return new Vector3(Random.Range(-halfWidth, halfWidth), ceiling * Random.Range(0.75f, 1.25f), Random.Range(-halfWidth, halfWidth));
    }
    Quaternion RandomRotation()
    {
        return Quaternion.Euler(0, 0, 0);
    }
    void SpawnStar()
    {


        GameObject newStar = (GameObject)Instantiate(StarPrefab, RandomSpawnLocation(), RandomRotation()) as GameObject;
        float seed = Random.value;
        newStar.GetComponent<StarScript>().lifetime = seed * 5.0f + 5.0f; //Ranges between 5 and 10 sec
        Rigidbody rb = newStar.GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(0, -(50.0f + 500.0f * seed), 0));
    }
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;
        spawnTimer = Random.Range(1.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        elapsedTime += dt;

        if (elapsedTime > spawnTimer)
        {
            elapsedTime = 0;
            spawnTimer = Random.Range(1.0f, 2.0f);
            //Spawn a random number of stars, at a random number of x-z positions and a random height, at random sizes and speeds
            //For now, spawn just a single star, with some random orientation , and with some force value
            SpawnStar();
        }
    }
}
