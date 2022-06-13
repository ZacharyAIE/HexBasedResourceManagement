using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloudsMove : MonoBehaviour
{
    public Collider resetCollider;
    public float speed;
    public float xLimit;
    float rand;



    private void Start()
    {
        rand = Random.Range(.2f, 1.2f);
    }

    void Update()
    {
        
        transform.position += new Vector3(1,0,0)*Time.deltaTime*rand;

        if(transform.position.x > xLimit)
        {
            Vector3 temp = transform.localScale;
            transform.localScale = Vector3.zero;

            transform.position = new Vector3(-10, transform.position.y, transform.position.z);
            transform.localScale = Vector3.Lerp(transform.localScale, temp, 1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider == resetCollider)
        {
            
        }
    }
}
