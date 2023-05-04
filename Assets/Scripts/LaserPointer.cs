using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;
using Valve.VR.Extras;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class LaserPointer : MonoBehaviour
{
    public static GameObject currentObject;
    public GameObject leftHand;
    public GameObject rightHand;
    int currentID;
    // Start is called before the first frame update
    void Start()
    {
        currentObject = null;
        currentID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0f);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            int id = hit.collider.gameObject.GetInstanceID();

            currentObject = hit.collider.gameObject;

            string name = currentObject.name;

            if (!SteamVR_Actions._default.GrabGrip.GetStateDown(SteamVR_Input_Sources.Any) && name == "Button")
            {
                Debug.Log("Highlight");
                ColorBlock cb = hit.collider.gameObject.GetComponent<Button>().colors;
                cb.normalColor = Color.green;
                hit.collider.gameObject.GetComponent<Button>().colors = cb;
            }
            if (SteamVR_Actions._default.GrabGrip.GetStateDown(SteamVR_Input_Sources.Any) && name == "Button")
            {
                Debug.Log("Click");
                hit.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                hit.collider.enabled = false;
                leftHand.GetComponent<SteamVR_LaserPointer>().pointer.GetComponent<MeshRenderer>().enabled = false;
                rightHand.GetComponent<SteamVR_LaserPointer>().pointer.GetComponent<MeshRenderer>().enabled = false;
            }

        }
       // currentID = 0;
    }
}
