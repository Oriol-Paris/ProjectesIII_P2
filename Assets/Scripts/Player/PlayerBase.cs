using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    #region VARIABLES

    public enum Actions { MOVE, SHOOT, HEAL, SELECTING, NOTHING };

    [SerializeField] private int health;
    private int actionPoints;
    [SerializeField] private float range;
    [SerializeField] private float oldRange;
    [SerializeField] private float shootingRange;
    [SerializeField] OG_MovementByMouse checkMovement;
    Actions action;

    private bool isInAction;
    private bool isAlive;
    public bool victory;

    #endregion

    void Start()
    {
        action = Actions.MOVE;
        isAlive = true;
        victory = false;
        isInAction = false;
        oldRange = range;
        checkMovement = GetComponent<OG_MovementByMouse>();
    }

    void Update()
    {
        if (!victory)
        {
            if (isAlive)
            {
                if (!checkMovement.GetIsMoving())
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        action = Actions.MOVE;
                        range = oldRange;
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        action = Actions.SHOOT;
                        range = shootingRange;
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        action = Actions.HEAL;
                        range = oldRange;
                        Heal(1); // Execute healing immediately
                    }
                }
            }
            else
            {
                action = Actions.NOTHING;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyMovement>() != null)
        {
            if (health > 0)
            {
                Damage();
            }
            else
            {
                isAlive = false;
                Debug.Log("YOU DIED");
            }
        }
    }

    #region GETTERS

    public float GetOldRange() { return oldRange; }
    public float GetRange() { return range; }
    public Actions GetAction() { return action; }
    public bool GetInAction() { return isInAction; }

    #endregion


    #region SETTERS

    public void Damage(int val = 1) { health -= val; Debug.Log("OOF"); }
    public void Heal(int amount) { health += amount; Debug.Log("Healed by " + amount); action = Actions.NOTHING; } // New heal method
    public void SetRange(float newRange) { range = newRange; }
    public void SetInAction(bool newVal) { isInAction = newVal; }

    #endregion
}

