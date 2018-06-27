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
    public float AttackTime;

    /// <summary>
    /// Goat states 
    /// </summary>
    IEnumerator _CurrentState;
    IEnumerator _NextState;

    private bool onClash;
    private bool onAttack;

    public List<string> Keyset;

    Charge scriptCharge;
    HeadButt scriptHeadbutt;
    Animator animator;

	// Use this for initialization
	void Start () {

        scriptCharge = GetComponent<Charge>();
        scriptHeadbutt = GetComponent<HeadButt>();
        animator = GetComponent<Animator>();

        _CurrentState = IDLE();
        StartCoroutine(STATEMACHINE());

        MaxCounter = 50.0f;
        MaxTimeout = 3.0f;
        CooldownTime = 1.0f;
        AttackTime = 1.0f;
        Power = 25.0f;
        onClash = false;
        onAttack = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            return;
        }
        onClash = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            return;
        }
        onClash = false;
        onAttack = false;
        SmashCounter = 0.0f;
    }

    // Update is called once per frame
    void Update ()
    {
        // Update Powerbar
        var powerBar = GetComponent<PowerBarScript>();
        powerBar.Completeness = SmashCounter / MaxCounter;

        if ((Input.GetKeyDown(Keyset[0]) || Input.GetKeyDown(Keyset[1])) && !Cooldown && !animator.GetBool("startRunning") && !onAttack)
        {
            // Initiate button smashing
            if (scriptCharge)
            {
                scriptCharge.enabled = true;
            }

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

    private void HeadbuttCharge()
    {
        animator.SetFloat("Force", SmashCounter / 100.0f);
        //scriptHeadbutt.m_enable = true;
    }
    private void ButtHeaded()
    {
        animator.SetFloat("Force", SmashCounter / 10.0f);
        //scriptHeadbutt.m_enable = true;
    }
    private void HeadbuttContact()
    {
        Debug.Log("Hit");
        scriptHeadbutt.m_enable = true;
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
            animator.SetFloat("Force", SmashCounter/5.0f);

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
                if (onClash == false)
                {
                    if (scriptCharge)
                    {
                        scriptCharge.m_force = SmashCounter;
                    }
                    if (animator)
                    {
                        animator.SetBool("startRunning", true);
                    }
                }
                if (onClash == true)
                {
                    if (scriptCharge)
                    {
                        scriptCharge.m_force = SmashCounter;
                    }
                    if (scriptHeadbutt)
                    {
                        scriptHeadbutt.m_enable = false;
                        scriptHeadbutt.enabled = true;
                        scriptCharge.enabled = false;
                        onAttack = true;
                    }
                }
                Overtime = 0.0f;
                _NextState = COOLDOWN();

            }

            yield return null;
        }
    }
}
