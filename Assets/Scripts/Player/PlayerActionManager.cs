using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    #region VARIABLES

    PlayerBase player;

    public Dictionary<PlayerBase.ActionEnum, ActiveAction> activeActions;
    public Dictionary<PlayerBase.ActionEnum, PassiveAction> passiveActions;
    //public Dictionary<PlayerBase.ActionEnum, SingleUseAction> singleUseActions;

    public bool isMoving = true;
    public bool isShooting = true;
    public bool isHealing = true; // New flag for healing

    private CombatManager combatManager;

    private bool hasShot = false; // Flag to track if a shot has been fired

    #endregion
    private void Awake()
    {
        activeActions = new Dictionary<PlayerBase.ActionEnum, ActiveAction>();
        passiveActions = new Dictionary<PlayerBase.ActionEnum, PassiveAction>();
    }
    private void Start()
    {
        
        player = GetComponent<PlayerBase>();

        activeActions.Add(PlayerBase.ActionEnum.MOVE, new MoveAction());
        activeActions.Add(PlayerBase.ActionEnum.SHOOT, new ShootAction());
        passiveActions.Add(PlayerBase.ActionEnum.HEAL, new HealAction());

        combatManager = FindAnyObjectByType<CombatManager>();
    }

    public void UpdateAction(Vector3 newPos, float t)
    {
        if (combatManager != null && combatManager.allEnemiesDead)
        {
            return; // Do not execute any actions if victory condition is met
        }

        if (player.GetAction().m_action == PlayerBase.ActionEnum.MOVE && (!player.GetComponent<OG_MovementByMouse>().GetIsMoving() || isMoving))
        {
            isMoving = true;
            activeActions[PlayerBase.ActionEnum.MOVE].Execute(player, newPos);
        }
        if (player.GetAction().m_action == PlayerBase.ActionEnum.SHOOT && (!player.GetComponent<OG_MovementByMouse>().GetIsMoving() || isShooting))
        {
            if (!hasShot) { 
            isShooting = true;
            hasShot = true; // Set the flag to indicate a shot has been fired
            ((ShootAction)activeActions[PlayerBase.ActionEnum.SHOOT]).bulletPrefab = player.activeStyle.m_prefab;
            activeActions[PlayerBase.ActionEnum.SHOOT].Execute(player, newPos);
            }

        }

        if (player.GetComponent<OG_MovementByMouse>().t >= 1)
        {
            ResetFlags();
        }
    }

    public void ResetFlags()
    {
        Debug.Log("RESET");
        hasShot = false; // Reset the flag when the player stops moving
    }
}