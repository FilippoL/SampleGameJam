using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesScript : MonoBehaviour {
    /// <summary>
    /// What particle system to use
    /// </summary>
    static private ParticleSystem m_parts;

    /// <summary>
    /// Multiplier for intesity
    /// </summary>
    private float m_intensifier;


    public float Intensifier
    {
        get
        {
            return m_intensifier;
        }

        set
        {
            m_intensifier = value;
        }
    }

    public ParticleSystem Parts
    {
        get
        {
            return m_parts;
        }

        set
        {
            m_parts = value;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_parts = GetComponentInChildren<ParticleSystem>();
        if (m_parts == null)
        {
            return;
        }


        var main = Parts.main;


        main.maxParticles = (int)m_intensifier * 250;
        
    }
}
