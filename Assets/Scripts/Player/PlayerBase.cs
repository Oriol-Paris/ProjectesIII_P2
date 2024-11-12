using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class PlayerBase : MonoBehaviour
{
    

    public enum ActionEnum { MOVE, SHOOT, HEAL, NOTHING };
    public enum ActionType { ACTIVE, PASSIVE, SINGLE_USE };

    [System.Serializable]
    public struct Action
    {
        public Action(ActionType type, ActionEnum action, KeyCode key, BulletStyle style = null) 
        {
            m_action = action;
            m_key = key;
            m_style = style;
        }

        public static Action nothing { get { return new Action(ActionType.ACTIVE, ActionEnum.NOTHING, KeyCode.Numlock); } }

        public ActionEnum m_action {  get; private set; }
        public KeyCode m_key {  get; private set; }
        public BulletStyle m_style { get; private set; }

        public void ChangeKey(KeyCode newKey) { m_key = newKey; }
    }


    [System.Serializable]
    public class BulletStyle
    {
        public GameObject m_prefab;
        public int m_range;
    }

    #region VARIABLES

    public BulletStyle gun; //Range 6
    public BulletStyle shotgun; //Range 3
    public BulletStyle activeStyle { get; private set; }

    [SerializeField] private int health;
    private int actionPoints;
    public float range;
    OG_MovementByMouse checkMovement;

    public Action activeAction {  get; private set; }
    private List<Action> availableActions = new List<Action>();

    private bool isInAction;
    private bool isAlive;
    public bool victory;

    #endregion


    void Start()
    {
        availableActions.Add(new Action(ActionType.ACTIVE, ActionEnum.MOVE, KeyCode.Alpha1));
        availableActions.Add(new Action(ActionType.ACTIVE, ActionEnum.SHOOT, KeyCode.Alpha2, gun));
        availableActions.Add(new Action(ActionType.ACTIVE, ActionEnum.SHOOT, KeyCode.Alpha3, shotgun));
        availableActions.Add(new Action(ActionType.ACTIVE, ActionEnum.HEAL, KeyCode.Alpha4));

        activeAction = availableActions[0];

        isAlive = true;
        victory = false;
        isInAction = false;

        checkMovement = GetComponent<OG_MovementByMouse>();
    }


    void Update()
    {
        if (!victory && isAlive)
        {
            if (!checkMovement.GetIsMoving())
            {
                foreach (Action action in availableActions)
                {
                    if (Input.GetKeyDown(action.m_key))
                    {
                        activeAction = action;

                        if(action.m_style != null)
                            activeStyle = action.m_style;
                    }
                }
            }
            

            if (activeAction.m_style != null)
            {
                range = activeAction.m_style.m_range;
            }
            else
            {
                range = shotgun.m_range;
            }

            if (activeAction.m_action == ActionEnum.HEAL)
            {
                Heal(1); // Execute healing immediately
            }
        }
        else
        {
            activeAction = Action.nothing;
            Debug.Log("Doing jackshit");
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

    public float GetRange() { return range; }
    public Action GetAction() { return activeAction; }
    public bool GetInAction() { return isInAction; }

    #endregion


    #region SETTERS

    public void Damage(int val = 1) { health -= val; Debug.Log("OOF"); }
    public void Heal(int amount) { health += amount; Debug.Log("Healed by " + amount); activeAction = Action.nothing; } // New heal method
    public void SetRange(float newRange) { range = newRange; }
    public void SetInAction(bool newVal) { isInAction = newVal; }
    public void AddNewAction(Action action) { availableActions.Add(action); }
    public void SetRange(int newRange) { range = newRange; }

    #endregion


}