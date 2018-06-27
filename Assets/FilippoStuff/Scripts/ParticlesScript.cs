using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesScript : MonoBehaviour {

    /// <summary>
    /// What particle system to use
    /// </summary>
    private ParticleSystem m_parts;

    /// <summary>
    /// Multiplier for intesity
    /// </summary>
    private int m_intensifier;


    public int Intensifier
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
    

    void Start () {
        m_parts = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update () {

        var main = m_parts.main;

        //main.maxParticles = m_intensifier;
        //main.startSpeed = m_intensifier;

        main.maxParticles = (int)(GetComponent<PowerBarScript>().Completeness * 750); 
        main.startSpeed = (int)(GetComponent<PowerBarScript>().Completeness);
    }
}
