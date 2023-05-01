using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem; 

public class NewStarScript : MonoBehaviour
{
    // Start is called before the first frame update

    //Fields
    // explodes upon "death"
    public GameObject deathExplosion;
    public AudioClip deathSound;
    public AudioClip pointSound;

    //Lifetime duration -> should call Die when lifetime is up
    public float growTime;
    float elapsedTime = 0;
    public float scale;

    public int pointValue;
    // interactable  
    private Interactable interactable;
    private Hand hand; 

    void Start()
    {
        //lifetime = Random.Range(0.5f, 3.0f);
        // Debug.Log("bruh");
        //pointValue = 1;
        growTime = 3.5f;
        scale = 15.0f;
        interactable = GetComponent<Interactable>();
        hand = interactable.attachedToHand;  
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable.attachedToHand != null) 
        {
            interactable.attachedToHand.GetComponent<ParticleSystem>().enableEmission = false;
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;
            Debug.Log("held tag is: " + this.gameObject.tag);

            AudioSource.PlayClipAtPoint(pointSound,
            gameObject.transform.position, 0.6f);

            Die(); 
        }

        elapsedTime += Time.deltaTime;
        //transform

        if (elapsedTime <= growTime)
        {
            float cur = scale * elapsedTime / growTime;
            this.transform.localScale = new Vector3(cur, cur, cur);
        }
    }

    // whenever star collides with floor, should die 
    private void OnCollisionEnter(Collision collision)
    {
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        Collider collider = collision.collider;
        if (collider.CompareTag("Floor"))
        {

            AudioSource.PlayClipAtPoint(deathSound,
            gameObject.transform.position, 0.6f);
            // Debug.Log("star collided with floor, died");

            //Spawn particle simulation upon death
            Instantiate(this.deathExplosion, this.gameObject.transform.position,
            Quaternion.AngleAxis(-90, Vector3.right));
            Destroy(gameObject);
        }
    }

    void Die()
    {
        //Spawn particle simulation upon death
        Instantiate(this.deathExplosion, this.gameObject.transform.position,
        Quaternion.AngleAxis(-90, Vector3.right));
        Debug.Log("dead object is: " + this.gameObject.tag);

        // call global increase score function  
        GameObject obj = GameObject.Find("Global");
        GlobalScript g = obj.GetComponent<GlobalScript>();
        g.UpdateScore(pointValue); 
        

        interactable.attachedToHand.DetachObject(gameObject, false);
        Destroy(gameObject);
    }
}
