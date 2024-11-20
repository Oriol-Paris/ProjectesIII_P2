using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] private PlayerBase player;
    public Animator fx;
    private PlayerData playerData;

    // Dictionaries to store actions by type
    public Dictionary<PlayerBase.ActionEnum, ActiveAction> activeActions;
    public Dictionary<PlayerBase.ActionEnum, PassiveAction> passiveActions;

    public bool isMoving = true;
    public bool isShooting = true;
    public bool isHealing = true; // New flag for healing
    public bool turnAdded = false;
    public int turnsDone = 0;

    private CombatManager combatManager;
    private bool hasShot = false; // Flag to track if a shot has been fired

    private Animator animationToExecute;

    #endregion

    private void Awake()
    {
        activeActions = new Dictionary<PlayerBase.ActionEnum, ActiveAction>();
        passiveActions = new Dictionary<PlayerBase.ActionEnum, PassiveAction>();
        animationToExecute = GetComponent<Animator>();
    }

    private void Start()
    {
        player = GetComponent<PlayerBase>();
        if (player == null)
        {
            Debug.LogError("PlayerBase component not found on the GameObject.");
        }
        else
        {
            Debug.Log("PlayerBase component found and assigned.");
        }

        playerData = player.playerData; // Load playerData from PlayerBase

        InitializeActions();
        combatManager = FindAnyObjectByType<CombatManager>();
    }

    private void InitializeActions()
    {
        foreach (var actionData in playerData.availableActions)
        {
            switch (actionData.actionType)
            {
                case PlayerBase.ActionType.ACTIVE:
                    if (actionData.action == PlayerBase.ActionEnum.MOVE)
                    {
                        activeActions.Add(actionData.action, new MoveAction());
                    }
                    else if (actionData.action == PlayerBase.ActionEnum.SHOOT)
                    {
                        activeActions.Add(actionData.action, new ShootAction());
                    }
                    else if (actionData.action == PlayerBase.ActionEnum.MELEE)
                    {
                        activeActions.Add(actionData.action, new MeleeAction());
                    }
                    break;
                case PlayerBase.ActionType.PASSIVE:
                    if (actionData.action == PlayerBase.ActionEnum.HEAL)
                    {
                        passiveActions.Add(actionData.action, new HealAction());
                    }
                    break;
                    // Add other cases if you have SingleUse or other action types
            }
        }
    }

    public void UpdateAction(Vector3 newPos, float t)
    {
        if (combatManager != null && combatManager.allEnemiesDead)
        {
            return; // Do not execute any actions if victory condition is met
        }

        var currentAction = player.GetAction();

        if (currentAction.m_action == PlayerBase.ActionEnum.MOVE && (!player.GetComponent<OG_MovementByMouse>().isMoving|| isMoving))
        {
            isMoving = true;
            activeActions[PlayerBase.ActionEnum.MOVE].Execute(player, newPos);
        }

        if (currentAction.m_action == PlayerBase.ActionEnum.SHOOT && (!player.GetComponent<OG_MovementByMouse>().isMoving || isShooting))
        {
            if (!hasShot)
            {
                isShooting = true;
                hasShot = true; // Set the flag to indicate a shot has been fired
                StartCoroutine(AttackCoroutine(PlayerBase.ActionEnum.SHOOT, newPos));
            }
        }

        if (currentAction.m_action == PlayerBase.ActionEnum.MELEE && (!player.GetComponent<OG_MovementByMouse>().GetIsMoving() || isMoving))
        {
            isMoving = true;
            StartCoroutine(AttackCoroutine(PlayerBase.ActionEnum.MELEE, newPos));
        }

        if (currentAction.m_action == PlayerBase.ActionEnum.HEAL && isHealing)
        {
            passiveActions[PlayerBase.ActionEnum.HEAL].Execute(player, newPos);
        }

        if (t >= 1)
        {
            ResetFlags();
        }
    }

    public void ResetFlags()
    {
        
        hasShot = false; // Reset the flag when the player stops moving
        turnAdded = false;
    }

    public PlayerBase GetPlayer() { return player; }


    public IEnumerator AttackCoroutine(PlayerBase.ActionEnum action, Vector3 newPos)
    {
        this.GetComponent<Animator>().SetTrigger("attack");
        fx.SetTrigger("playFX");

        yield return new WaitForSeconds(0.5f);

        if(action == PlayerBase.ActionEnum.SHOOT)
        {
            ((ShootAction)activeActions[PlayerBase.ActionEnum.SHOOT]).bulletPrefab = player.activeStyle.prefab;
            activeActions[PlayerBase.ActionEnum.SHOOT].Execute(player, newPos);
        }
        else
        {
            activeActions[PlayerBase.ActionEnum.MELEE].Execute(player, newPos);
        }

        fx.ResetTrigger("playFX");
            
    }
}
