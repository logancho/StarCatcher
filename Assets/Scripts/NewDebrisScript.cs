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
    public AudioClip collectSound;

    //Lifetime duration -> should call Die when lifetime is up
    public float growTime;
    float elapsedTime = 0;
    public float scale;

    // interactable  
    private Interactable interactable;
    private Hand hand;

    void Start()
    {
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
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Bruh");
        if (collider.CompareTag("LeftHand") && SteamVR_Actions._default.GrabGrip.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            Debug.Log("Bruh");

            //SteamVR_Input_Sources source = interactable.attachedToHand.handType;
            Debug.Log("held tag is: " + this.gameObject.tag);

            AudioSource.PlayClipAtPoint(collectSound,
            gameObject.transform.position, 0.6f);

            Die();
        }

        if (collider.CompareTag("RightHand") && SteamVR_Actions._default.GrabGrip.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            Debug.Log("Bruh");

            //SteamVR_Input_Sources source = interactable.attachedToHand.handType;
            Debug.Log("held tag is: " + this.gameObject.tag);

            AudioSource.PlayClipAtPoint(collectSound,
            gameObject.transform.position, 0.6f);

            Die();

        }

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

        // if hit jar, should decrease the starlight 
        if (collider.CompareTag("Jar"))
        {
            Debug.Log("debris hit jar");

            GameObject obj = GameObject.Find("Global");
            GlobalScript g = obj.GetComponent<GlobalScript>();

            // should decrease the starlight
            //For testing
            g.UpdateScore(5);
            //g.UpdateScore(-5);
            Debug.Log("Score is: " + g.score); 

            //Spawn particle simulation upon death
            Instantiate(this.deathExplosion, this.gameObject.transform.position,
            Quaternion.AngleAxis(-90, Vector3.right));
            Destroy(gameObject);
        }

//        // debris hit player's head/hands/body -> decrease health 
//        if (collider.CompareTag("Head") || collider.CompareTag("Body") || collider.CompareTag("LeftHand") || collider.CompareTag("RightHand"))
//        {
//            // Debug.Log("debris collided with " + collider.tag);
//            AudioSource.PlayClipAtPoint(deathSound,
//gameObject.transform.position, 0.3f);
//            //Spawn particle simulation upon death
//            Instantiate(this.deathExplosion, this.gameObject.transform.position,
//            Quaternion.AngleAxis(-90, Vector3.right));

//            // call global decrease health function  
//            GameObject obj = GameObject.Find("Global");
//            GlobalScript g = obj.GetComponent<GlobalScript>();

//            // g.DecreaseHealth();

//            Destroy(gameObject);
//        }
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
        
        // g.DecreaseHealth();

        interactable.attachedToHand.DetachObject(gameObject, false);
        Destroy(gameObject);
    }
}

