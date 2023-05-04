using UnityEngine;
using Valve.VR;

public class SteamVRPointer : MonoBehaviour
{
    public SteamVR_Action_Vector2 touchpadAction;
    public SteamVR_Action_Boolean triggerAction;
    public SteamVR_Action_Boolean interactWithUIAction; // Added the new action
    public SteamVR_Input_Sources handType;
    public GameObject pointer;
    public float defaultLength = 5.0f;
    public Color color = Color.blue;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.startWidth = 0.005f;
        lineRenderer.endWidth = 0.005f;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color")) { color = this.color };
    }

    private void Update()
    {
        if (triggerAction.GetState(handType))
        {
            RaycastHit hit;
            if (Physics.Raycast(pointer.transform.position, pointer.transform.forward, out hit))
            {
                lineRenderer.SetPosition(0, pointer.transform.position);
                lineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                lineRenderer.SetPosition(0, pointer.transform.position);
                lineRenderer.SetPosition(1, pointer.transform.position + pointer.transform.forward * defaultLength);
            }
        }
        else
        {
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
        }

        // Added a new section to handle the "Interact With UI" action
        if (interactWithUIAction.GetStateDown(handType))
        {
            RaycastHit hit;
            if (Physics.Raycast(pointer.transform.position, pointer.transform.forward, out hit))
            {
                // Interact with the UI element if it has a UI component
                UnityEngine.UI.Button button = hit.collider.gameObject.GetComponent<UnityEngine.UI.Button>();
                if (button != null)
                {
                    button.onClick.Invoke();
                }
            }
        }
    }
}
