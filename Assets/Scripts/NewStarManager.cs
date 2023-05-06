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
    public float ceiling = 8.0f;
    public float height;
    //float halfWidth = 1.25f;

    //Spawning timer
    public GameObject StarPrefab;

    float spawnTimer;
    float elapsedTime;

    public float directionRandomness = 0.4f;
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

    float Bias(float x, float b)
    {
        b = -Mathf.Log(1.0f - b, 2.0f);
        return 1.0f - Mathf.Pow(1.0f - Mathf.Pow(x, 1.0f/ b), b);
    }

    void SpawnStar()
    {
        Vector3 randLocation = RandomSpawnLocation();
        Quaternion randRotation = RandomRotation(randLocation);
        randLocation += new Vector3(0, height, 0);
        GameObject newStar = (GameObject)Instantiate(StarPrefab, randLocation, randRotation) as GameObject;


        float seed = Random.value;
        seed = Bias(seed, 0.45f);

        Rigidbody rb = newStar.GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(0, 0, (10.0f + 190.0f * seed)));
        //rb.AddRelativeTorque(seed * 0.5f * new Vector3(Random.value, Random.value, Random.value));

        newStar.GetComponent<Renderer>().material.SetColor("_Color", Color.Lerp(Color.magenta, Color.yellow, seed));
        newStar.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.magenta, Color.yellow, seed));

        ParticleSystem ps = newStar.GetComponent<ParticleSystem>();
        var col = ps.colorOverLifetime;
        col.enabled = true;

        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.Lerp(Color.magenta, Color.yellow, seed), 0.0f), new GradientColorKey(Color.red, 0.5f) }, new GradientAlphaKey[] { new GradientAlphaKey(seed * seed, 0.0f), new GradientAlphaKey(0.0f, 0.6f) });
        col.color = grad;

        newStar.GetComponent<NewStarScript>().pointValue = Mathf.CeilToInt(5.0f + seed * 10.0f);
        //Debug.Log("randLocation:");
        //Debug.Log(randLocation);
        //Debug.Log("Forward vector:");
        //Debug.Log(newStar.transform.forward);
    }
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;
        //minTimeWait = 0.5f;
        //maxTimeWait = 3.0f;
        spawnTimer = Random.Range(minTimeWait, maxTimeWait);
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
            spawnTimer = Random.Range(minTimeWait, maxTimeWait);
            //spawnTimer = 0.2f;
            //Spawn a random number of stars, at a random number of x-z positions and a random height, at random sizes and speeds
            //For now, spawn just a single star, with some random orientation , and with some force value
            SpawnStar();
        }
    }
}
