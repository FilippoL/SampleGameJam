using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Charge : MonoBehaviour {

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
    private float   m_damping = -4.0f;
    public float    m_explosionModifier = 4;

    private void OnEnable()
    {
        m_transform = GetComponent<Transform>();
        if (m_tObject)
        {
            m_direction = (m_tObject.GetComponent<Transform>().position - m_transform.position).normalized;
        }

        GetComponent<Animator>().SetBool("startCharge", true);

        m_rBody = GetComponent<Rigidbody>();
        m_isChargeFinished = false;
    }

    // Use this for initialization
    void Start() {
        m_transform = GetComponent<Transform>();
        if(m_tObject)
        {
            m_direction = (m_tObject.GetComponent<Transform>().position - m_transform.position).normalized;
        }

        GetComponent<Animator>().SetBool("startCharge", true);

        m_rBody = GetComponent<Rigidbody>();
        m_isChargeFinished = false;
    }

    // Update is called once per frame
    void Update() {
        if (m_tObject)
        {
            if(!GetComponent<Animator>().GetBool("startCharge") && GetComponent<Animator>().GetBool("startRunning")){
                m_direction = (m_tObject.GetComponent<Transform>().position - m_transform.position).normalized;
                m_rBody.AddForce(m_direction * m_force);
            }

            if (GetComponent<Animator>().GetBool("startRunning"))
            {
                GetComponent<Animator>().SetBool("startCharge", false);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!m_tObject || collision.gameObject.CompareTag("Terrain"))
        {
            return;
        }
        float enemyForce = m_tObject.GetComponent<Charge>().m_force;
        float explosionForce = (m_force + enemyForce);

        GetComponent<Animator>().SetBool("isPushing", true);
        GetComponent<Animator>().SetBool("startRunning", false);


        Rigidbody m_tBody = m_tObject.GetComponent<Rigidbody>();
        m_rBody = GetComponent<Rigidbody>();

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
        if (!m_tObject || collision.gameObject.CompareTag("Terrain"))
        {
            return;
        }
        m_rBody = GetComponent<Rigidbody>();
        if (m_rBody.velocity.magnitude < 0.39f)
        {
            m_isChargeFinished = true;
            this.enabled = false;
        }
    }
}
