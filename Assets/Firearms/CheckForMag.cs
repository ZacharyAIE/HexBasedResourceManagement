using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForMag : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Magazine magToCheck = other.GetComponent<Magazine>();
        Firearm gun = GetComponentInParent<Firearm>();

        // Check if the mag is the correct type, that we don't have a mag already and that its not an empty magazine.
        // THIS CAN BE EDITED TO ALLOW EMPTY MAGS FOR REALISM
        if (magToCheck && magToCheck.MagazineType == gun.requiredMagazineType && !gun.mag && magToCheck.MagazineAmmo > 0)
        {
            gun.mag = magToCheck;
            gun.AddMagazine();
        }
    }
}
