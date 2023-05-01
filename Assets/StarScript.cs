using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem; 

public class StarScript : MonoBehaviour
{
    // Start is called before the first frame update

    //Fields
    // explodes upon "death"
    public GameObject deathExplosion;

    //Lifetime duration -> should call Die when lifetime is up
    public float lifetime;
    float elapsedTime = 0;

    // interactable  
    private Interactable interactable;
    private Hand hand; 

    private int pointValue; 

    void Start()
    {
        //lifetime = Random.Range(0.5f, 3.0f);
        // Debug.Log("bruh");
        interactable = GetComponent<Interactable>();
        hand = interactable.attachedToHand;
        pointValue = 1; 
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable.attachedToHand != null) 
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;
            Debug.Log("held tag is: " + this.gameObject.tag);
            Die(); 
        }
        elapsedTime += Time.deltaTime;
        /* 
        if (elapsedTime > lifetime)
        {
            Die();
        }
        */ 
    }

    // whenever star collides with floor, should die 
    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider; 
        if (collider.CompareTag("Floor"))
        {
            // Debug.Log("star collided with floor, died");

            //Spawn particle simulation upon death
            Instantiate(this.deathExplosion, this.gameObject.transform.position,
            Quaternion.AngleAxis(-90, Vector3.right));
            Destroy(gameObject);
        }

        // star hit player's head/hands/body 
        if (collider.CompareTag("Head") || collider.CompareTag("Body") || collider.CompareTag("LeftHand") || collider.CompareTag("RightHand"))
        {
            Debug.Log("star collided with " + collider.tag);

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
        // Debug.Log("dead object is: " + this.gameObject.tag);

        // call global increase score function  
        GameObject obj = GameObject.Find("Global");
        GlobalScript g = obj.GetComponent<GlobalScript>();
        g.UpdateScore(pointValue); 
        

        interactable.attachedToHand.DetachObject(gameObject, false);
        Destroy(gameObject);
    }
}
