using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MagazineType
{
    PISTOL,
    AR,
    AK,
    SHOTGUN
}

public class Magazine : MonoBehaviour
{
    // Private Ammo Variables
    public MagazineType m_magazineType;
    [SerializeField] private int m_maxAmmo = 10;
    [SerializeField] private int m_magazineAmmo;

    // Ammo Properties
    public int MaxAmmo
    {
        get { return m_maxAmmo; }
        private set { m_maxAmmo = value; }
    }
    public int MagazineAmmo
    {
        get { return m_magazineAmmo; }
        set { m_magazineAmmo = value; }
    }
    public MagazineType MagazineType
    {
        get { return m_magazineType; }
        private set { m_magazineType = value; }
    }

    void Start()
    {
        MagazineAmmo = MaxAmmo;
    }
}
