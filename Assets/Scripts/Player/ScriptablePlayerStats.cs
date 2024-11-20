using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int health;
    public int actionPoints;
    public int baseRange;
    public int exp;
    public bool isAlive;
    public bool victory;

    [System.Serializable]
    public class BulletStyle
    {
        public GameObject prefab;
        public int range;
        public int damage;
    }

    public BulletStyle gun;
    public BulletStyle shotgun;
    public BulletStyle healStyle; // Add this line for healing
    public BulletStyle moveStyle; // Add this line for moving

    public int healAmount = 10; 

    public List<ActionData> availableActions = new List<ActionData>();

    [System.Serializable]
    public class ActionData
    {
        public ActionData(PlayerBase.ActionType _actionType, PlayerBase.ActionEnum _action, KeyCode _key, BulletStyle _style = null)
        {
            actionType = _actionType;
            action = _action;
            key = _key;
            style = _style;
        }
        public PlayerBase.ActionType actionType;
        public PlayerBase.ActionEnum action;
        public PlayerBase player;
        public KeyCode key;
        public BulletStyle style;
    }
}
