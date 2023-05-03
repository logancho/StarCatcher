using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class NewDebrisManager : MonoBehaviour
{
    //Fields
    //Debris prefab
    public GameObject DebrisPrefab;

    //Spawning bounds?
    public float ceiling = 10.0f;
    public float height;

    // for playing width: 
    // float halfWidth = 5.0f;

    // for testing width: 
    float halfWidth = 2.5f;

    //Spawning timer
    float spawnTimer;
    float elapsedTime;

    public float directionRandomness;
    public float minTimeWait;
    public float maxTimeWait;



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
        }
        else
        {
            r = rand.y;
            theta = Mathf.PI / 2.0f - Mathf.PI / 4.0f * rand.x / rand.y;
        }

        return r * new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0);
    }



    Vector3 RandomSpawnLocation()
    {
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
    void SpawnDebris()
    {
        Vector3 randLocation = RandomSpawnLocation();
        Quaternion randRotation = RandomRotation(randLocation);
        randLocation += new Vector3(0, height, 0);
        GameObject newDebris = (GameObject)Instantiate(DebrisPrefab, randLocation, randRotation) as GameObject;
        float seed = Random.value;
        newDebris.GetComponent<NewDebrisScript>().growTime = (1.0f - seed) * 3.5f + 0.3f; //Ranges between 0.3 and 3.8 sec
        Rigidbody rb = newDebris.GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(0, 0, (20.0f + 150.0f * seed)));
    }
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;
        spawnTimer = Random.Range(minTimeWait, maxTimeWait);
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        elapsedTime += dt;

        if (elapsedTime > spawnTimer)
        {
            elapsedTime = 0;
            spawnTimer = Random.Range(minTimeWait, maxTimeWait);
            //Spawn a random number of stars, at a random number of x-z positions and a random height, at random sizes and speeds
            //For now, spawn just a single star, with some random orientation , and with some force value
            SpawnDebris();
        }
    }
}
