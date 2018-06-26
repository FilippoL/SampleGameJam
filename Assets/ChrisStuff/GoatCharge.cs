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
    ///     The Target Game Object;
    /// </summary>
    public GameObject m_tObject;

    public bool     m_isChargeFinished;
    public float    m_force;
    private float   m_damping = -0.95f;
    public float    m_explosionModifier = 4;

    

    // Use this for initialization
    void Start() {
        m_transform = GetComponent<Transform>();
        m_direction = (m_tObject.GetComponent<Transform>().position - m_transform.position).normalized;
        m_rBody = GetComponent<Rigidbody>();
        m_isChargeFinished = false;
    }

    // Update is called once per frame
    void Update() {
        m_direction = (m_tObject.GetComponent<Transform>().position - m_transform.position).normalized;

        m_rBody.AddForce(m_direction * m_force);

    }

    void OnCollisionEnter(Collision collision)
    {
        
        float enemyForce = m_tObject.GetComponent<GoatCharge>().m_force;
        float explosionForce = (m_force + enemyForce);
        

        Rigidbody m_tBody = m_tObject.GetComponent<Rigidbody>();

        Vector2 ratio = new Vector2(m_force / explosionForce, enemyForce / explosionForce);

        if(ratio.y > ratio.x)
        {
            m_tBody.AddExplosionForce(explosionForce * m_explosionModifier * m_damping * ratio.x, m_rBody.position, 1.0f);
            m_rBody.AddExplosionForce(explosionForce * m_explosionModifier * ratio.y, m_tBody.position, 1.0f);
        }
        else
        {
            m_tBody.AddExplosionForce(explosionForce * m_explosionModifier * ratio.x, m_rBody.position, 1.0f);
            m_rBody.AddExplosionForce(explosionForce * m_explosionModifier * m_damping * ratio.y, m_tBody.position, 1.0f);
        }        

    }

    private void OnCollisionStay(Collision collision)
    {
        if (m_rBody.velocity.magnitude < 0.39f)
        {
            m_isChargeFinished = true;
            this.enabled = false;
        }
    }
}
