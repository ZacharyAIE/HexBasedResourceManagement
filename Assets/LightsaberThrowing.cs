using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class LightsaberThrowing : MonoBehaviour
{
    List<Destructable> m_Destructables = new List<Destructable>();
    public GameObject lightsaber;
    public GameObject owner;
    public float travelTime = 1f;
    bool flying = false;

    public void AltFire(InputAction.CallbackContext ctx)
    {
        if (ctx.action.ReadValue<float>() > 0)
        {
            //if (EventSystem.current.IsPointerOverGameObject())
            //    return;

            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && m_Destructables.Count < 3)
            {
                Destructable target = hit.collider.GetComponent<Destructable>();
                m_Destructables.Add(target);
                Debug.Log(target);
            }
        }
    }
    public void Update()
    {
        while (flying)
        {
            transform.Rotate(transform.position, Time.deltaTime);
        }
    }
    public void Fire(InputAction.CallbackContext ctx)
    {
        StartCoroutine(LightsaberFly());
        
    }

    public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed)
    {
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator LightsaberFly()
    {
        if (m_Destructables.Count > 0)
        {
            foreach (Destructable target in m_Destructables)
            {
                flying = true;
                StartCoroutine(MoveOverSpeed(lightsaber, target.transform.position, travelTime));
                yield return new WaitUntil(() => lightsaber.transform.position == target.transform.position);
                flying = false;
            }
        }
    }
}
