using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationType
{
    Random,
    Stepped
}

public class RandomRotation : MonoBehaviour
{
    public RotationType rotationType;
    // Start is called before the first frame update
    void Start()
    {
        if(rotationType == RotationType.Random)
        {
            RotateRandom(gameObject);
        }
        else if(rotationType == RotationType.Stepped)
        {
            RotateStepped(gameObject);
        }
    }

    void RotateRandom(GameObject obj)
    {
        obj.transform.rotation = Quaternion.Euler(transform.rotation.x, Random.Range(0, 360), transform.rotation.z);
    }

    void RotateStepped(GameObject obj)
    {
        // Possible angles
        int[] angles = new int[7] {-180, -120, -60, 0, 60, 120, 180 };

        int pickedAngle = angles[Random.Range(0, angles.Length)];

        obj.transform.rotation = Quaternion.Euler(transform.rotation.x, pickedAngle, transform.rotation.z);
    }
}
