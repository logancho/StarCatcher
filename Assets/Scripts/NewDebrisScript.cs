using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class NewDebrisScript : MonoBehaviour
{ 
    // Start is called before the first frame update

    //Fields
    // explodes upon "death"
    public GameObject deathExplosion;
    public AudioClip deathSound;

    //Lifetime duration -> should call Die when lifetime is up
    public float growTime;
    float elapsedTime = 0;
    public float scale;

    // interactable  
    private Interactable interactable;
    private Hand hand;

    void Start()
    {
        //lifetime = Random.Range(0.5f, 3.0f);
        // Debug.Log("bruh");
        //growTime = 3.5f;
        scale = 0.5f;
        interactable = GetComponent<Interactable>();
        hand = interactable.attachedToHand;
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

        if (elapsedTime <= growTime)
        {
            float cur = scale * elapsedTime / growTime;
            this.transform.localScale = new Vector3(cur, cur, cur);
        }
    }

    // whenever debris collides with floor, should die 
    private void OnCollisionEnter(Collision collision)
    {


        Collider collider = collision.collider;
        if (collider.CompareTag("Floor"))
        {

            AudioSource.PlayClipAtPoint(deathSound,
gameObject.transform.position, 0.3f);
            // Debug.Log("debris collided with floor, died");

            //Spawn particle simulation upon death
            Instantiate(this.deathExplosion, this.gameObject.transform.position,
            Quaternion.AngleAxis(-90, Vector3.right));
            Destroy(gameObject);
        }

        // debris hit player's head/hands/body -> decrease health 
        if (collider.CompareTag("Head") || collider.CompareTag("Body") || collider.CompareTag("LeftHand") || collider.CompareTag("RightHand"))
        {
            // Debug.Log("debris collided with " + collider.tag);
            AudioSource.PlayClipAtPoint(deathSound,
gameObject.transform.position, 0.3f);
            //Spawn particle simulation upon death
            Instantiate(this.deathExplosion, this.gameObject.transform.position,
            Quaternion.AngleAxis(-90, Vector3.right));

            // call global decrease health function  
            GameObject obj = GameObject.Find("Global");
            GlobalScript g = obj.GetComponent<GlobalScript>();
            g.DecreaseHealth();

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
        g.DecreaseHealth();

        interactable.attachedToHand.DetachObject(gameObject, false);
        Destroy(gameObject);
    }
}

