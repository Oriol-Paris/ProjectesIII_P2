using UnityEngine;

[CreateAssetMenu(fileName = "ScriptablePlayerStats", menuName = "Scriptable Objects/ScriptablePlayerStats")]
public class ScriptablePlayerStats : ScriptableObject
{
    PlayerBase player;
    private void Awake()
    {
        player = FindAnyObjectByType<PlayerBase>();
    }
}
