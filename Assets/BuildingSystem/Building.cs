using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Building")]
public class Building : ScriptableObject
{
    public string name;
    public string description;
    public int cost;
    public GameObject model;

}
