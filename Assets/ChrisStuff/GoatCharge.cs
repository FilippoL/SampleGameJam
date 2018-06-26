using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoatCharge : MonoBehaviour {

    /// <summary>
    ///     RigidBody Of The Goat.
    /// </summary>
    private Rigidbody m_rBody;

    /// <summary>
    ///     RigidBody Of The Target
    /// </summary>
    private Rigidbody m_tBody;

    /// <summary>
    ///     Game Objects Transform.
    /// </summary>
    private Transform m_transform;

    /// <summary>
    ///     Direction Vector of the Object
    /// </summary>
    private Vector3 m_direction;

    /// <summary>
    ///     Whether the goat should start to charge or not
    /// </summary>
    private bool m_shouldStartCharge;

    /// <summary>
    ///     The Target Game Object;
    /// </summary>
    public GameObject m_tObject;

    public float force;
    public float damping = 1;
    public float Explosion_Force;
    
    void OnCollisionEnter(Collision collision)
    {
        float enemyForce = m_tObject.GetComponent<GoatCharge>().force;
        float explosionForce = (force + enemyForce);
        

        Rigidbody m_tBody = m_tObject.GetComponent<Rigidbody>();

        Vector2 ratio = new Vector2(explosionForce, enemyForce);

        m_rBody.AddExplosionForce(((explosionForce * Explosion_Force) * damping) * ratio.y, m_tBody.position, 1.0f);
        m_tBody.AddExplosionForce(((explosionForce * Explosion_Force) * damping) * ratio.x, m_rBody.position, 1.0f);

    }

    // Use this for initialization
    void Start() {
        m_transform = GetComponent<Transform>();
        m_direction = (m_tObject.GetComponent<Transform>().position - m_transform.position).normalized;
        m_rBody = GetComponent<Rigidbody>();
        m_shouldStartCharge = false;
        
    }

    // Update is called once per frame
    void Update() {
        m_direction = (m_tObject.GetComponent<Transform>().position - m_transform.position).normalized;

        if (m_shouldStartCharge) {   
            m_shouldStartCharge = true;
        }
        else
        {
            m_rBody.AddForce(m_direction * force);
        }

    }

}
