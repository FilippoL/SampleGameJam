using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBarScript : MonoBehaviour {

    /// <summary>
    /// The Class can be applied without a canvas to render in, 
    /// Just drag and drop on a camera-owning actor
    /// TODO: 
    /// Render texture in 3d using 
    /// </summary>

    [SerializeField] private double m_completeness;
    [SerializeField] private Texture2D m_progress_bar_img;
    [SerializeField] private Texture2D m_background_progress_bar_img;
    [SerializeField] private Vector2 m_position;
    [SerializeField] private Vector2 m_size;

    
    /// <summary>
    /// Gets or sets the completeness.
    /// </summary>
    /// <value>Completeness of status bar out of 100.</value>
    public double Completeness{
		get{ 
			return m_completeness;
		}
		set{
			m_completeness = value; 
		}
	}

    /// <summary>
    /// The actual power image to fill background with.
    /// </summary>
    public Texture2D Img
    {
        get
        {
            return m_progress_bar_img;
        }

        set
        {
            m_progress_bar_img = value;
        }
    }

    /// <summary>
    /// Position of the status bar image.
    /// </summary>
    public Vector2 Position
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

    /// <summary>
    /// Background image for the progress bar.
    /// </summary>
    public Texture2D Background_img
    {
        get
        {
            return m_background_progress_bar_img;
        }

        set
        {
            m_background_progress_bar_img = value;
        }
    }

    /// <summary>
    /// Size of the Status bar.
    /// </summary>
    public Vector2 Size
    {
        get
        {
            return m_size;
        }

        set
        {
            m_size = value;
        }
    }
    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start () {
		m_completeness = 0.0f;
	}
	
	/// <summary>
	/// Call this for increasing the value of status bar
	/// </summary>
	/// <param name="increase_value">Increasement value.</param>
	public void Set (float increase_value = 0.5f) {

        m_completeness = increase_value;
        //m_progress_bar_img = Sprite.Create(new Texture2D(250,50),new Rect(0.0f, 0.0f, 250, 50), new Vector2(0.5f, 0.5f), 100.0f);
    }   

    void Update() {

        //m_completeness = Time.time * 0.05;
    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(m_position.x, m_position.y, m_size.x, m_size.y));
        GUI.DrawTexture(new Rect(0, 0, m_size.x, m_size.y), m_background_progress_bar_img);
 
            GUI.BeginGroup(new Rect(0, 0, (float)m_completeness * m_size.x, m_size.y));
            GUI.DrawTexture(new Rect(0, 0, m_size.x, m_size.y), m_progress_bar_img);
            GUI.EndGroup();

        GUI.EndGroup();
    }
}
