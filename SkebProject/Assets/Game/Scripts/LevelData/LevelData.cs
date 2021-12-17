using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelDataObject", order = 1)]
public class LevelData : ScriptableObject
{
    public List<Level> levels = new List<Level>();
}
