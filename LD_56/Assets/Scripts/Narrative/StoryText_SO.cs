using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryText", menuName = "ScriptableObjects/Narrative", order = 3)]
public class StoryText_SO : ScriptableObject
{
    public List<string> texts;
}
