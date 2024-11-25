using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerBase : MonoBehaviour
{
    public PlayerData playerData; // Reference to the ScriptableObject containing player data

    public enum ActionEnum { MOVE, SHOOT, HEAL, MELEE, NOTHING };
    public enum ActionType { ACTIVE, PASSIVE, SINGLE_USE };

    [System.Serializable]
    public struct Action
    {
        public Action(ActionType type, ActionEnum action, KeyCode key, int cost, PlayerData.BulletStyle style = null)
        {
            m_action = action;
            m_key = key;
            m_style = style;
            m_cost = cost;
        }

        public static Action nothing { get { return new Action(ActionType.ACTIVE, ActionEnum.NOTHING, KeyCode.None,0); } }

        public ActionEnum m_action { get; private set; }
        public KeyCode m_key { get; private set; }
        public PlayerData.BulletStyle m_style { get; private set; }

        public int m_cost { get;private set; }

        public void ChangeKey(KeyCode newKey) { m_key = newKey; }
    }

    #region VARIABLES

    public PlayerData.BulletStyle activeStyle { get; private set; }

    public int health;
    public int actionPoints;
    public float range;
    public int exp = 0;
    private OG_MovementByMouse checkMovement;
    public PlayerActionManager turnsDone;

    public Action activeAction { get; private set; }
    private List<Action> availableActions = new List<Action>();

    private bool isInAction;
    private bool isAlive;
    public bool victory;
    public bool defeat;

    #endregion

    void Start()
    {
        LoadPlayerData();

        if (availableActions.Count > 0)
        {
            activeAction = availableActions[0];
        }
        isAlive = playerData.isAlive;
        victory = playerData.victory;
        isInAction = false;
        turnsDone = GetComponent<PlayerActionManager>();
        checkMovement = GetComponent<OG_MovementByMouse>();
    }

    private void LoadPlayerData()
    {
        // Load health, range, and other properties from the ScriptableObject
        health = playerData.health;
        actionPoints = playerData.actionPoints;
        exp = playerData.exp;

        // Load available actions from playerData and populate availableActions list
        foreach (var actionData in playerData.availableActions)
        {
            availableActions.Add(new Action(
                actionData.actionType,
                actionData.action,
                actionData.key,
                actionData.cost,
                actionData.style
            ));
        }

        range = playerData.baseRange;  // Set initial range from playerData
    }

    void Update()
    {
        if (!victory && isAlive && !defeat)
        {
            if (!checkMovement.GetIsMoving())
            {
                foreach (Action action in availableActions)
                {
                    if (Input.GetKeyDown(action.m_key))
                    {
                        activeAction = action;

                        if (action.m_style != null)
                            activeStyle = action.m_style;
                    }
                }
            }

            range = activeAction.m_style != null ? activeAction.m_style.range : playerData.baseRange;

            if (activeAction.m_action == ActionEnum.HEAL)
            {
                Heal(playerData.healAmount); // Use healAmount from playerData
            }
        }
        else
        {
            activeAction = Action.nothing;
            Debug.Log("Doing nothing");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyMovement>() != null)
        {
            collision.gameObject.GetComponent<EnemyMovement>().Attack();
            this.GetComponent<Animator>().SetTrigger("hit");

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

    IEnumerator DeathCoroutine()
    {
        this.GetComponent<Animator>().SetBool("isDead", true);
        activeAction = Action.nothing;
        isAlive = false;

        yield return new WaitForSeconds(1);
    }

    #region GETTERS

    public float GetRange() { return range; }
    public bool GetIsAlive() { return isAlive; }
    public Action GetAction() { return activeAction; }
    public bool GetInAction() { return isInAction; }

    #endregion

    #region SETTERS

    public void Damage(int val = 1) { health -= val; Debug.Log("OOF"); }
    public void Heal(int amount) { health += amount; Debug.Log("Healed by " + amount); activeAction = Action.nothing; }
    public void SetRange(float newRange) { range = newRange; }
    public void SetInAction(bool newVal) { isInAction = newVal; }
    public void AddNewAction(Action action) { availableActions.Add(action); }

    #endregion
}
