using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class DebrisScript : MonoBehaviour
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

    void Start()
    {
        //lifetime = Random.Range(0.5f, 3.0f);
        // Debug.Log("bruh");
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

        /* 
        elapsedTime += Time.deltaTime;
        if (elapsedTime > lifetime)
        {
            Die();
        }
        */ 
    }

    void Die()
    {
        //Spawn particle simulation upon death
        Instantiate(this.deathExplosion, this.gameObject.transform.position,
        Quaternion.AngleAxis(-90, Vector3.right));
        Debug.Log("dead object is: " + this.gameObject.tag);
        interactable.attachedToHand.DetachObject(gameObject, false);
        Destroy(gameObject);
    }
}

