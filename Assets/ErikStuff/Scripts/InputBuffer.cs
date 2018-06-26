using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBuffer : MonoBehaviour {
    
    /// <summary>
    /// Max smashing button count
    /// </summary>
    [SerializeField]
    private float MaxCounter;

    /// <summary>
    /// Max time spent smashing
    /// </summary>
    [SerializeField]
    private float MaxTimeout;

    /// <summary>
    /// Smashed button counter
    /// </summary>
    [SerializeField]
    private float SmashCounter;

    /// <summary>
    /// Time lapsed between states
    /// </summary>
    [SerializeField]
    private float Overtime;

    /// <summary>
    /// Cooldown timer
    /// </summary>
    [SerializeField]
    private bool Cooldown;

    public float Power;
    public float CooldownTime;

    /// <summary>
    /// Goat states 
    /// </summary>
    IEnumerator _CurrentState;
    IEnumerator _NextState;

	// Use this for initialization
	void Start () {
        _CurrentState = IDLE();
        StartCoroutine(STATEMACHINE());

        MaxCounter = 10.0f;
        MaxTimeout = 3.0f;
        CooldownTime = 1.0f;
        Power = 25.0f;
    }

	// Update is called once per frame
	void Update ()
    {
        // Update Powerbar
        var powerBar = GetComponent<PowerBarScript>();
        powerBar.Set(SmashCounter / MaxCounter);

        if (Input.GetKeyDown("space") && !Cooldown)
        {
            // Initiate button smashing
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

    /// <summary>
    /// Cool down state (impossible to charge)
    /// </summary>
    /// <returns></returns>
    IEnumerator COOLDOWN()
    {
        _NextState = null;

        while (_NextState == null)
        {
            Overtime += Time.deltaTime;
            Cooldown = true;

            if (Overtime > CooldownTime)
            {
                _NextState = IDLE();
                Overtime = 0.0f;
                Cooldown = false;
            }

            yield return null;
        }
    }

    /// <summary>
    /// Charging coorutine and timeout
    /// </summary>
    /// <returns></returns>
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
