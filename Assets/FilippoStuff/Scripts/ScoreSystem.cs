using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{

    private int m_score = 0;
    private int m_highscore;

    private Text score_text;

    string highScoreKey = "HighScore";
    string currentScore = "Score";

    public int Score
    {
        get
        {
            return m_score;
        }

        set
        {
            m_score = value;
        }
    }


    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
        score_text = GetComponent<Text>();
        m_highscore = PlayerPrefs.GetInt(highScoreKey, 0);
    }

    // Update is called once per frame
    void Update()
    {
        DrawLine(Vector3.zero, Vector3.zero, Color.red);
        if (score_text)
        {
            score_text.text = "Score: " + Score;
            Score += (int)Time.deltaTime;
        }
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        /*GameObject myLine = new GameObject();
        myLine.transform.position = transform.position;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.startColor = Color.red;
        lr.endColor = Color.red;
        lr.startWidth = 5.0f;
        lr.endWidth = 5.0f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        Destroy(myLine, duration);*/
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt(currentScore, Score);
        PlayerPrefs.Save();
    }
}
