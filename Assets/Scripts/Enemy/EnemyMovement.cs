using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    #region VARIABLES

    //[SerializeField]private MovementByMouse Player;
    [SerializeField]private OG_MovementByMouse Player;
    private Vector3 PlayerPos;
    float moveTime;
    EnemyBase enemyStats;
    [SerializeField] private GameObject bulletShot;
    [SerializeField] private float velocity;
    [SerializeField] private float range;

    #endregion

    public enum ActionEnum { MOVE, SHOOT, HEAL, MELEE, NOTHING };

    void Start()
    {
        Player = FindAnyObjectByType<OG_MovementByMouse>();
        enemyStats = GetComponent<EnemyBase>();
    }

    void Update()
    {
        moveTime = Time.deltaTime * velocity;

        if (enemyStats.isAlive)
        {
            if (Vector3.Distance(PlayerPos, transform.position) < range && Player.isMoving)
            {
                PlayerPos = Player.GetPosition();
                transform.position = Vector3.MoveTowards(transform.position, PlayerPos, moveTime);
            }

            if (Player.GetComponent<PlayerBase>().GetInAction())
            {
                transform.position = Vector3.MoveTowards(transform.position, PlayerPos, moveTime);

            }

            if (Player.GetComponent<PlayerBase>().GetInAction())
            {
                PlayerPos = Player.GetPosition();
            }
        }
    }
}

