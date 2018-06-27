using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadButt : MonoBehaviour {

    private Transform m_transform;
    private Rigidbody m_rBody;
    private Rigidbody m_tBody;
    private Vector3 m_direction;

    public GameObject m_tObject;
    public bool m_enable;

    private void OnEnable()
    {
        m_transform = GetComponent<Transform>();
        if (m_tObject != null)
        {
            m_direction = (m_tObject.GetComponent<Transform>().position - m_transform.position).normalized;
        }
        m_rBody = GetComponent<Rigidbody>();

        GetComponent<Animator>().SetTrigger("HeadButtTrigger");
        GetComponent<Animator>().SetBool("startCharge", false);
        GetComponent<Animator>().SetBool("isPushing", false);
        
    }

    // Use this for initialization
    void Start () {
		m_transform = GetComponent<Transform>();
        if (m_tObject != null)
        {
            m_direction = (m_tObject.GetComponent<Transform>().position - m_transform.position).normalized;
        }
        m_rBody = GetComponent<Rigidbody>();

        GetComponent<Animator>().SetTrigger("HeadButtTrigger");
        GetComponent<Animator>().SetBool("isPushing", false);
        GetComponent<Animator>().SetBool("startCharge", false);

        GetComponent<Charge>().enabled = false;

    }
	
	// Update is called once per frame
	void Update () {

        DoHeadbuttExplosion(m_enable);
    }

    private void DoHeadbuttExplosion(bool enable = false)
    {
        if (enable)
        {

            Rigidbody m_tBody = m_tObject.GetComponent<Rigidbody>();
            float force = GetComponent<Charge>().m_force + m_tObject.GetComponent<Charge>().m_force;

            float m_explosionModifier = GetComponent<Charge>().m_explosionModifier;

            m_tBody.AddExplosionForce((force * m_explosionModifier) * 10.0f, m_rBody.position, 1.0f);

            GetComponent<Animator>().SetBool("isPushing", false);

            enable = false;

            this.enabled = false;
        }
    }

}
