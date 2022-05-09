using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Firearm
{
    public Animator animator;

    private void Start()
    {
        mag = GetComponentInChildren<Magazine>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(mag != null && mag.MagazineType != MagazineType.PISTOL)
        {
            EjectMagazine();
        }
    }

    [ContextMenu("Chamber Round")]
    public override void ChamberRound()
    {
        mag = GetComponentInChildren<Magazine>();

        // If we have ammo and the correct mag is inserted (sanity check), then chamber a round.
        // If we already somehow have a bullet in the chamber, don't chamber another this prevents rounds being sent to the shadow realm.
        if (!isBulletInChamber && mag != null && mag.MagazineType == MagazineType.PISTOL && mag.MagazineAmmo > 0)
        {
            isBulletInChamber = true;
            mag.MagazineAmmo--; 
        }
        isSlideForward = true;
        audioSource.PlayOneShot(slideRackForwardSound);
    }

    // Just cock the hammer. This happens in a pistol when the slide hits the rear extent.
    [ContextMenu("Cock Hammer")]
    public override void CockHammer()
    {
        if (isBulletInChamber)
        {
            // EJECT A WHOLE BULLET
            isBulletInChamber = false;
        }
        isSlideForward = false;
        isHammerCocked = true;
        audioSource.PlayOneShot(slideRackBackSound);
    }

    // If we have a bullet in the chamber and the hammer is cocked, then fire.
    [ContextMenu("Fire")]
    public override void Fire()
    {
        if(isHammerCocked && isBulletInChamber && isSlideForward)
        {
            animator.Play("Fire");

            audioSource.PlayOneShot(fireSound);
            isHammerCocked = false;
            isBulletInChamber = false;

            RaycastHit hit;
            if (Physics.Raycast(muzzleLocation.position, muzzleLocation.forward, out hit, range))
            {

                RagdollBodyPart part = hit.collider.GetComponent<RagdollBodyPart>();
                if (part != null)
                {
                    CharacterStats characterHit = part.parentCharacter;
                    characterHit.Health -= damage;
                    if (characterHit.Health <= 0)
                    {
                        hit.rigidbody.AddForce(-hit.normal * force);
                    }
                }
            }
        }
        // If we don't have any bullets but the hammer is cocked, then dry fire. This is the famous "click" sound.
        else if (isHammerCocked && !isBulletInChamber && isSlideForward)
        {
            DryFire();
        }
    }

    // If we don't have any bullets but the hammer is cocked, then reset the hammer and play the click. 
    [ContextMenu("Dry Fire")]
    public override void DryFire()
    {
        audioSource.PlayOneShot(dryFireSound);
        isHammerCocked = false;
    }

    [ContextMenu("Eject Mag")]
    // Detach the Mag from the Gun.
    public override void EjectMagazine()
    {
        if(mag != null)
        {
            StartCoroutine(ToggleMagTrigger());
            mag.transform.parent = null;
            mag.GetComponent<Rigidbody>().isKinematic = false;
            mag = null;
        }
    }
    [ContextMenu("Add Mag")]
    public override void AddMagazine()
    {
        mag.transform.position = magLocation.transform.position;
        mag.transform.rotation = magLocation.transform.rotation;
        mag.transform.SetParent(magLocation);
        mag.GetComponent<Rigidbody>().isKinematic = true;
    }

    // This is used to stop the old mag from immediately being snapped back into the gun.
    IEnumerator ToggleMagTrigger()
    {
        magTrigger.enabled = false;
        yield return new WaitForSeconds(0.7f);
        magTrigger.enabled = true;
    }
}
