using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DataSystemObject", order = 1)]

public class DataSystem : ScriptableObject
{
    public List<Female> Females = new List<Female>();
    public List<Male> Males = new List<Male>();
    public List<CollectionObj> CollectionObjsPositive = new List<CollectionObj>();
    public List<CollectionObj> CollectionObjsNegative = new List<CollectionObj>();
    public List<Material> GateNegativeMaterials = new List<Material>();
    public List<Material> GatePositiveMaterials = new List<Material>();
    public List<GameObject> Particles = new List<GameObject>();
    public Human NewMale;
    public Material AlphaMat;

}
