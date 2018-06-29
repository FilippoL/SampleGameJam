using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DayCycle : MonoBehaviour {

    float m_startTime;

    public float m_time = 0;
    public float m_DayLengthMinutes = 3;

    private Transform m_lightTransform;

    public Color dayTimeTint;
    public Color nightTimeTint;
    public float duration = 1.0f;
    public float lerpAmount = 0;

    public float m_angle;
    public float offset;


    [SerializeField]private float m_MAX_INTENSITY;

    public float MAX_INTENSITY
    {
        get
        {
            return m_MAX_INTENSITY;
        }

        set
        {
            m_MAX_INTENSITY = value;
        }
    }

    // Use this for initialization
    void Start () {
        m_time = 0;
        m_DayLengthMinutes = 3;
        m_MAX_INTENSITY = GetComponent<Light>().intensity;
        m_startTime = Time.time;
        m_angle = 0;
        offset = 0;
    }
	
	// Update is called once per frame
	void Update () {


        m_lightTransform = GetComponent<Transform>();

        if (m_time > m_DayLengthMinutes * 60.0f) {
            m_time = 0;
        }
        else if( m_time < 0) {
            m_time = m_DayLengthMinutes * 60.0f;
        }

        float modifier = 0.0f;
        if (CompareTag("Sun"))
        {
            m_lightTransform.rotation = Quaternion.Euler(Utils.MapRange(m_time, 0.0f, m_DayLengthMinutes * 60.0f, 0.0f, 360.0f), -90.0f, 0);
            modifier = 1.0f;
        }
        if (CompareTag("Moon"))
        {
            modifier = 0.1f;
            m_lightTransform.rotation = Quaternion.Euler(-Utils.MapRange(m_time, 0.0f, m_DayLengthMinutes * 60.0f, 0.0f, 360.0f), 90.0f, 0);
        }

        m_angle = Utils.MapRange(m_time, 0.0f, m_DayLengthMinutes * 60.0f, 0.0f, 360.0f);
        

        //Moon Up
        if(m_angle > 0 && m_angle < 180)
        {
            if (GetComponent<Light>().intensity < m_MAX_INTENSITY)
            {
                GetComponent<Light>().intensity += Time.deltaTime * modifier;
            }
        }
        else
        {
            if (GetComponent<Light>().intensity > 0)
            {
                GetComponent<Light>().intensity -= Time.deltaTime * modifier;
            }
        }

        if (m_angle > 0 && m_angle < 20)
        {
            lerpAmount = Utils.MapRange(m_angle + offset, 0, 20, 1, 0);
        }
        else if(m_angle > 160 && m_angle < 180)
        {
            lerpAmount = Utils.MapRange(m_angle - offset, 160, 180, 0, 1);
        }

        if (CompareTag("Sun"))
        {

            if (RenderSettings.skybox.HasProperty("_TintColor"))
                RenderSettings.skybox.SetColor("_TintColor", Color.Lerp(dayTimeTint, nightTimeTint, lerpAmount));

            //Debug.Log(m_lightTransform.rotation.eulerAngles.x.ToString());
            //Debug.Log(lerpAmount.ToString());
        }

        m_time += Time.deltaTime;
	}
}
