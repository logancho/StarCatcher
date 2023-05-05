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
            p.remainingLifetime = 0;
            enter[i] = p;
        }
        Debug.Log("hit jar");
        GameObject.Find("Global").GetComponent<GlobalScript>().PowerUpActivated();
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }
}
