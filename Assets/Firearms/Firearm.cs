using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Firearm : MonoBehaviour
{

    [Header("Damage Settings")]
    [SerializeField] protected float damage;
    [SerializeField] protected float force;
    [SerializeField] protected float range;

    [Header("Muzzle Settings")]
    [SerializeField] protected Transform muzzleLocation;

    [Header("Magazine Settings")]
    [SerializeField] public MagazineType requiredMagazineType;
    [SerializeField] protected Transform magLocation;
    [SerializeField] protected Transform shellEjectLocation;
    [SerializeField] protected Collider magTrigger;
    public Magazine mag;

    [Header("Grip Points")]
    [SerializeField] protected Transform gripPoint;
    [SerializeField] protected Transform gripPoint2;

    [Header("Audio Clips")]
    [SerializeField] protected AudioClip fireSound;
    [SerializeField] protected AudioClip dryFireSound;
    [SerializeField] protected AudioClip slideRackBackSound;
    [SerializeField] protected AudioClip slideRackForwardSound;
    [SerializeField] protected AudioClip magInSound;
    [SerializeField] protected AudioClip magOutSound;
    protected AudioSource audioSource;

    [Header("State Debug | DO NOT CHANGE MANUALLY")]
    [SerializeField] protected bool isBulletInChamber = false;
    [SerializeField] protected bool isHammerCocked = false;
    [SerializeField] protected bool isSlideForward = false; // If the slide isn't forward, the hammer cant move forward and thus can't fire.

    public abstract void ChamberRound();

    public abstract void CockHammer();

    public abstract void EjectMagazine();

    public abstract void Fire();
    
    // When chamber is empty and player attempts to fire, do something.
    public abstract void DryFire();

    public abstract void AddMagazine();

}
