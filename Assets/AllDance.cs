using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllDance : MonoBehaviour
{
    public List<Animator> animators;


    // Start is called before the first frame update
    void Start()
    {
        animators = FindObjectsOfType<Animator>().ToList();
    }

    public void React()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("React");

        }
    }
}
