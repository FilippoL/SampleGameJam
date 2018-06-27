using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DayCycle : MonoBehaviour {

    
    float m_startTime;

    public float m_time = 0;
    public float m_DayLengthMinutes = 3;

    private Transform m_lightTransform;

    public Material dayTimeMat;
    public Material nightTimeMat;
    public float duration = 2.0f;

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
    }
	
	// Update is called once per frame
	void Update () {

        float lerp = Mathf.PingPong(Time.time, duration) / duration;

        Debug.Log("Lerp: " + lerp.ToString());
        Debug.Log(RenderSettings.skybox.HasProperty("Tint Color").ToString());
        Debug.Log(RenderSettings.skybox.HasProperty("_TintColor").ToString());
        
       

        m_lightTransform = GetComponent<Transform>();

        if (m_time > m_DayLengthMinutes * 60.0f)
        {
            m_time = 0;
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

        
        if ( m_lightTransform.rotation.eulerAngles.x > 180 )
        {
            if(GetComponent<Light>().intensity > 0)
            {
                GetComponent<Light>().intensity -= Time.deltaTime * modifier;
            }
        }
        else if (m_lightTransform.rotation.eulerAngles.x > 0)
        {
            if (GetComponent<Light>().intensity < m_MAX_INTENSITY)
            {
                GetComponent<Light>().intensity += Time.deltaTime * modifier;
            }
        }
        m_time += Time.deltaTime;
	}
}
