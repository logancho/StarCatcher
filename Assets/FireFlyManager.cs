using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyManager : MonoBehaviour
{
    ParticleSystem ps;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            //p.position = new Vector3(-100, -100, -100);
            p.remainingLifetime = 0;
            //p.startSize = 0;
            enter[i] = p;
            Debug.Log("bruhhhh");
        }
        Debug.Log("Particle ran into Jar");
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }
}
