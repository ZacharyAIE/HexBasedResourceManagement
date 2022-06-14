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
    Vector3 temp;
    //Is the cloud changing size?
    bool isChangingSize = false;
    //How long the cloud expands over
    public float expandDuration = 1;

    private void Start()
    {
        rand = Random.Range(.2f, 1.2f);
        temp = transform.localScale;
    }

    void Update()
    {
        transform.position += new Vector3(1, 0, 0) * Time.deltaTime * rand;

        if (transform.position.x > xLimit)
        {
            StartCoroutine(Expand());
        }
    }

    IEnumerator Expand()
    {
        //Don't run if cloud is changing size
        if (isChangingSize)
            yield break;

        //Mark cloud as changing size
        isChangingSize = true;
        //How much time has elapsed
        float elapsedTime = 0;

        //Lerp the size from 0 to original size
        while (elapsedTime < expandDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(temp, Vector3.zero, elapsedTime / expandDuration);
            yield return null;
        }

        transform.position = new Vector3(-10, transform.position.y, transform.position.z);

        elapsedTime = 0;

        //Lerp the size from 0 to original size
        while (elapsedTime < expandDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, temp, elapsedTime / expandDuration);
            yield return null;
        }

        //Mark cloud as not changing size
        isChangingSize = false;
        yield break;
    }
}
