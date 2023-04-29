using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class NewStarManager : MonoBehaviour
{
    //Fields
    //Star prefab

    //Spawning bounds?
    float ceiling = 8.0f;
    float halfWidth = 1.25f;

    //Spawning timer
    public GameObject StarPrefab;

    float spawnTimer;
    float elapsedTime;

    public float directionRandomness = 0.4f;


    Vector3 SquareToDiskConcentric(Vector2 sample)
    {
        Vector2 rand = 2.0f * sample - new Vector2(1, 1);

        if (rand.x == 0 && rand.y == 0)
        {
            return new Vector3(0, 0, 0);
        }

        float theta, r;
        if (Mathf.Abs(rand.x) > Mathf.Abs(rand.y))
        {
            r = rand.x;
            theta = Mathf.PI / 4.0f * rand.x / rand.y;
        } else
        {
            r = rand.y;
            theta = Mathf.PI / 2.0f - Mathf.PI / 4.0f * rand.x / rand.y;
        }

        return r * new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0);
    }

    Vector3 RandomSpawnLocation()
    {

        //TODO
        Vector2 randRect = new Vector2(Random.value, Random.value);

        Vector2 d = SquareToDiskConcentric(randRect);


        float z = Mathf.Sqrt(Mathf.Max(0.0f, 1.0f - d.x * d.x - d.y * d.y));
        return new Vector3(d.x, z, d.y) * ceiling;
    }

    Quaternion RandomRotation(Vector3 location)
    {

        location.x += location.x * Random.Range(-directionRandomness, directionRandomness);
        location.y += location.y * Random.Range(-directionRandomness, directionRandomness);
        location.z += location.z * Random.Range(-directionRandomness, directionRandomness);
        return Quaternion.LookRotation(-location);
    }

    void SpawnStar()
    {
        Vector3 randLocation = RandomSpawnLocation();
        Quaternion randRotation = RandomRotation(randLocation);
        GameObject newStar = (GameObject)Instantiate(StarPrefab, randLocation, randRotation) as GameObject;
        float seed = Random.value;
        newStar.GetComponent<StarScript>().lifetime = seed * 5.0f + 5.0f; //Ranges between 5 and 10 sec
        Rigidbody rb = newStar.GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(0, 0, (20.0f + 150.0f * seed)));
        //Debug.Log("randLocation:");
        //Debug.Log(randLocation);
        //Debug.Log("Forward vector:");
        //Debug.Log(newStar.transform.forward);
    }
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;
        spawnTimer = Random.Range(1.0f, 2.0f);
        //spawnTimer = 0.2f;
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
            //spawnTimer = 0.2f;
            //Spawn a random number of stars, at a random number of x-z positions and a random height, at random sizes and speeds
            //For now, spawn just a single star, with some random orientation , and with some force value
            SpawnStar();
        }
    }
}
