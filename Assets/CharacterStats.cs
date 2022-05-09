using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour
{
    public float Health { 
        get { return m_health; } 

        set { m_health = value; 
            OnHealthChanged.Invoke();
            if (m_health <= 0) {
                OnHealthChanged.Invoke();
                OnHealthZeroed.Invoke();
            } 
        } 
    }
    [SerializeField] private float m_health = 100;


    public UnityEvent OnHealthChanged;
    public UnityEvent OnHealthZeroed;
}
