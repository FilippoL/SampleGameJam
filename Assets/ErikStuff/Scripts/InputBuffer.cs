using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBuffer : MonoBehaviour {
    [SerializeField]
    private float MaxCounter;
    [SerializeField]
    private float MaxTimeout;
    [SerializeField]
    private float SmashCounter;
    [SerializeField]
    private float Overtime;
    [SerializeField]
    private bool Cooldown;

    public float Power;
   

    IEnumerator _CurrentState;
    IEnumerator _NextState;

	// Use this for initialization
	void Start () {
        _CurrentState = IDLE();
        StartCoroutine(STATEMACHINE());

        MaxCounter = 30.0f;
        MaxTimeout = 3.0f;
        Power = 25.0f;
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("space") && !Cooldown)
        {
            SmashCounter += Power * Time.deltaTime;
            _NextState = SMASHING();
        }
    }

    IEnumerator STATEMACHINE()
    {
        while (_CurrentState != null)
        {
            yield return StartCoroutine(_CurrentState);

            _CurrentState = _NextState;
            _NextState = null;
        }
    }

    IEnumerator IDLE()
    {
        while (_NextState == null)
        {
            _NextState = null;
            yield return null;
        }
    }

    IEnumerator COOLDOWN()
    {
        _NextState = null;

        while (_NextState == null)
        {
            Overtime += Time.deltaTime;
            Cooldown = true;

            if (Overtime > MaxTimeout)
            {
                _NextState = IDLE();
                Overtime = 0.0f;
                Cooldown = false;
            }

            yield return null;
        }
    }

    IEnumerator SMASHING()
    {
        _NextState = null;

        while (_NextState == null)
        {
            SmashCounter -= Time.deltaTime;
            Overtime += Time.deltaTime;
        
            if (SmashCounter < 0.0f)
            {
                SmashCounter = 0.0f;
            }
            if (SmashCounter > MaxCounter)
            {
                SmashCounter = MaxCounter;
            }
            if (Overtime > MaxTimeout)
            {
                SmashCounter = 0.0f;
                Overtime = 0.0f;
                _NextState = COOLDOWN();
            }

            yield return null;
        }
    }
}
