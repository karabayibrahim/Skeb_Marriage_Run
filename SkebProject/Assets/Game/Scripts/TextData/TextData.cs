using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TextDataSystemObject", order = 1)]

public class TextData : ScriptableObject
{
    public List<GameObject> PositiveTexts = new List<GameObject>();
    public List<GameObject> NegativeTexts = new List<GameObject>();
}
