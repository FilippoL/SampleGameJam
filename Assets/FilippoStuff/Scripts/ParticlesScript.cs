using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesScript : MonoBehaviour {

    /// <summary>
    /// Around what point. By default the parent gameobject.
    /// </summary>
    private Vector3 m_position;

    /// <summary>
    /// What particle system to use
    /// </summary>
    public ParticleSystem m_parts;

    /// <summary>
    /// Multiplier for intesity
    /// </summary>
    private int m_intensifier;


    public Vector3 Position
    {
        get
        {
            return m_position;
        }

        set
        {
            m_position = value;
        }
    }

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


    /// <summary>
    /// Getting each point of a circle around a point, 
    /// radius needs to be defined
    /// </summary>
    /// <param name="point">What point position to spawn around</param>
    /// <param name="radius">Radius of the circle defined</param>
    /// <param name="how_many_points">Portion of the circle in degrees</param>
    /// <returns>yeld untill break</returns>

    public static List<Vector2> DefineCircleAroundPoint(Vector2 point, float radius)
    {
        float x = 0.0f;
        float z = 0.0f;

        List<Vector2> list = new List<Vector2>();

        for (int i = 0; i < 90; i++)
        {
            x = Mathf.Sin(i) * radius + point.x;
            z = Mathf.Cos(i) * radius + point.y;

            list.Add(new Vector2(x, z));
        }

        return list;
    }


    void Start () {

        m_position = Vector3.zero;
        var main = m_parts.main;
        main.startColor = Color.red;
        main.startSpeed = 50;
    }

    // Update is called once per frame
    void Update () {

        if (m_position == Vector3.zero)
        { 
            m_parts.Emit(((int)GetComponent<PowerBarScript>().Completeness * 100));
        }
        else
        {
            m_parts.transform.position = m_position;
        }
    }
}
