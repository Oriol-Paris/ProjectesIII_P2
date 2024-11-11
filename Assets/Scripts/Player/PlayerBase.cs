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
    [SerializeField] public List<Actions> availableActions;
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
        shootingRange = GetComponent<PlayerActionManager>().bulletToInstantiate.GetComponent<BulletPrefab>().GetRange();
        Debug.Log(shootingRange);
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
                        action = availableActions[0];
                        
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        action = availableActions[1];

                    }
                    if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        action = availableActions[2];
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        if (availableActions.Count<4)
                        {
                            availableActions.Add(Actions.HEAL);
                        }
                        action = availableActions[3];
                    }
                }
            }
            else
            {
                action = Actions.NOTHING;
            }
            if(action == Actions.SHOOT)
            {
                range = shootingRange;
            }
            else
            {
                range = oldRange;
            }
            if(action == Actions.HEAL)
            {
                Heal(1); // Execute healing immediately
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

