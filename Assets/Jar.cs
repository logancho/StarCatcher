using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Jar : MonoBehaviour
{
    public GameObject starLiquid;
    GlobalScript gs;
    ParticleSystem ps;
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
        ps = GameObject.Find("FireFlySystem").GetComponent<ParticleSystem>();
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

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Bruh");
        Destroy(other);
    }

    //void OnParticleTrigger()
    //{
    //    //ps = GameObject.Find("FireFlySystem").GetComponent<ParticleSystem>();
    //    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    //    //ps.GetTriggerParticles(ParticleSystemTriggerEventType.E)
    //    int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    //    for (int i = 0; i < numEnter; i++)
    //    {
    //        ParticleSystem.Particle p = enter[i];
    //        //p.position = new Vector3(-100, -100, -100);
    //        p.remainingLifetime = 0;
    //        //p.startSize = 0;
    //        enter[i] = p;
    //        Debug.Log("bruhhhh");
    //    }
    //    Debug.Log("huh");
    //    //ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    //}
}
