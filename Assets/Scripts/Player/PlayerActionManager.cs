using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    PlayerBase player;
    public GameObject bulletToInstantiate;

    [SerializeField]
    public GameObject bulletPrefab;

    public bool isMoving = true;
    public bool isShooting = true;
    public bool isHealing = true; // New flag for healing

    private ActiveAction moveAction;
    private ActiveAction shootAction;
    private PassiveAction healAction; // New action

    private CombatManager combatManager;

    private void Start()
    {
        player = GetComponent<PlayerBase>();
        moveAction = gameObject.AddComponent<MoveAction>();
        shootAction = gameObject.AddComponent<ShootAction>();
        healAction = gameObject.AddComponent<HealAction>(); // Instantiate the new action
        ((ShootAction)shootAction).bulletToInstantiate = bulletToInstantiate;

        combatManager = FindObjectOfType<CombatManager>();
    }

    public void UpdateAction(Vector3 newPos, float t)
    {
        if (combatManager != null && combatManager.allEnemiesDead)
        {
            return; // Do not execute any actions if victory condition is met
        }

        if (player.GetIsMoving() && (!player.GetComponent<OG_MovementByMouse>().GetIsMoving() || isMoving))
        {
            isMoving = true;
            moveAction.Execute(player, newPos);
        }
        else if (player.GetIsShoooting() && (!player.GetComponent<OG_MovementByMouse>().GetIsMoving() || isShooting))
        {
            isShooting = true;
            shootAction.Execute(player, newPos);
        }

        if (!player.GetComponent<OG_MovementByMouse>().GetIsMoving())
        {
            isMoving = false;
            isShooting = false;
            isHealing = false; // Reset healing flag
        }
    }
}