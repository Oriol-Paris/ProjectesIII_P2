using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ScriptablePlayerBase", menuName = "Scriptable Objects/ScriptablePlayerBase")]
public class ScriptablePlayerBase : ScriptableObject
{
    public int health;
    public List<PlayerBase.Action> availableActions;
    public PlayerData.BulletStyle gun;
    public PlayerData.BulletStyle shotgun;
}