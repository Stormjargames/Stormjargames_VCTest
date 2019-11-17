using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class AddFootsteps : MonoBehaviour
{
    Rigidbody playerControls;
    RigidbodyFirstPersonController playerControlsRigid;
    private float m_StepCycle;
    private Vector2 m_Input;
    private float m_NextStep;
    public float m_StepInterval = 1;
    private bool m_IsWalking;
    public float m_RunstepLenghten = 2;
    public float m_WalkSpeed = 4, m_RunSpeed = 8;
    public List<string> IDs;
    AudioManager am;
    int storeLastLocation = 0;
    

    void Awake()
    {
        m_StepCycle = 0f;
        m_NextStep = m_StepCycle / 2f;
        am = GameObject.Find("GameManager").GetComponent<AudioManager>();
        playerControls = GetComponent<Rigidbody>();
      
    }

    void ProgressStepCycle(float speed)
    {
        if (playerControls.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
        {
            m_StepCycle += (playerControls.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunstepLenghten))) *
                         Time.fixedDeltaTime;
        }

        if (!(m_StepCycle > m_NextStep))
        {
            return;
        }

        m_NextStep = m_StepCycle + m_StepInterval;

        PlayFootStepAudio();
    }

    void GetInput(out float speed)
    {
        // Read input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
        // On standalone builds, walk/run speed is modified by a key press.
        // keep track of whether or not the character is walking or running
        m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
        // set the desired speed to be walking or running
        speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
        m_Input = new Vector2(horizontal, vertical);

        // normalize input if it exceeds 1 in combined length:
        if (m_Input.sqrMagnitude > 1)
        {
            m_Input.Normalize();
        }
    } 

    void FixedUpdate()
    {
        float speed = 0;
        GetInput(out speed);
        ProgressStepCycle(speed); 
    }

    private void PlayFootStepAudio()
    {
        //if (!m_CharacterController.isGrounded)
        //{
        //    return;
        //}
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int rand = Random.Range(0, IDs.Count);
        if (rand == storeLastLocation)
        {
            if (rand + 1 >= IDs.Count)
            {
                rand--;
            }
            else
            {
                rand++;
            }
        }
        storeLastLocation = rand;
        am.PlayBackgroundMusic(IDs[rand], 0, 3, false, false, false);
    }
}
