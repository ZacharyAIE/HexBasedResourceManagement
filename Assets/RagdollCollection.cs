using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollCollection : MonoBehaviour
{
    public Rigidbody[] m_rigidbodies;
    private Animator m_animator;

    // Start is called before the first frame update
    void Awake()
    {
        m_rigidbodies = GetComponentsInChildren<Rigidbody>();
        m_animator = GetComponent<Animator>();

        SwitchAnimatable();
    }

    private void Start()
    {
        CharacterStats stats = GetComponent<CharacterStats>();
        foreach (Rigidbody rigidbody in m_rigidbodies)
        {
            RagdollBodyPart part = rigidbody.gameObject.AddComponent<RagdollBodyPart>();
            part.parentCharacter = stats;
        }
    }
    public void ToggleAllKinematic()
    {
        foreach(Rigidbody rb in m_rigidbodies)
        {
            if(!rb.isKinematic)
                rb.isKinematic = true;
            else
                rb.isKinematic = false;
        }
    }

    public void EnableAllKinematic()
    {
        foreach (Rigidbody rb in m_rigidbodies)
        {
            rb.isKinematic = true;
        }
    }

    public void DisableAllKinematic()
    {
        foreach (Rigidbody rb in m_rigidbodies)
        {
            rb.isKinematic = false;
        }
    }

    public void SwitchToRagdoll()
    {
        if(m_animator != null && m_rigidbodies != null)
        {
            foreach (Rigidbody rb in m_rigidbodies)
            {
                m_animator.enabled = false;
                rb.isKinematic = false;
            }
        }
    }

    public void SwitchAnimatable()
    {
        if (m_animator != null && m_rigidbodies != null)
        {
            foreach (Rigidbody rb in m_rigidbodies)
            {   
                m_animator.enabled = true;
                rb.isKinematic = true;
            }
        }
    }
}
