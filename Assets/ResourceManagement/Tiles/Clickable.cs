using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ResourceManagement.Tiles
{
    public abstract class Clickable : MonoBehaviour
    {
        public bool isHovered = false;
        public bool isSelected = false;
        public Transform m_snapPoint;

        public virtual void OnHoverEnter()
        {
            isHovered = true;
        }

        public virtual void OnHoverExit()
        {
            Debug.Log("Exited: " + this);
            isHovered = false;
        }

        public virtual void OnSelect()
        {
            isHovered = false;
        }
    }
}