using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Jar : MonoBehaviour
{
    public GameObject starLiquid;
    GlobalScript gs;
    float maxScale = 0.09f;
    float cur_scale;
    //0.09 scale = 0.016 z movement

    //z because 90 degree rotation of jar
    float CurZPosition(float cur_scale)
    {
        return cur_scale * 0.9375f - 0.068375f;
    }

    // Start is called before the first frame update
    void Start()
    {
        gs = GameObject.Find("Global").GetComponent<GlobalScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 liquid_scale = starLiquid.transform.localScale;
        Vector3 liquid_pos = starLiquid.transform.localPosition;
        if (!gs.hasWon())
        {
            if (gs.score >= 0)
            {
                cur_scale = maxScale * ((float)gs.score / (float)gs.pointThreshold);
            }
            else
            {
                cur_scale = 0;
            }
            liquid_scale.y = cur_scale;
            liquid_pos.z = CurZPosition(cur_scale);
            starLiquid.transform.localScale = liquid_scale;
            starLiquid.transform.localPosition = liquid_pos;
        }
    }

   /* private void OnTriggerEnter(Collider collider)
    {
        // check if debris hits the jar 
        if (collider.CompareTag("Debris")) {
            GameObject obj = GameObject.Find("Global");
            GlobalScript g = obj.GetComponent<GlobalScript>();
            // should decrease the starlight 
            g.DecreaseHealth();

            // call debris die function 
            NewDebrisScript debris = collider.gameObject.GetComponent<NewDebrisScript>();

            Debug.Log("debris hit jar"); 
        } 
    }*/
}
