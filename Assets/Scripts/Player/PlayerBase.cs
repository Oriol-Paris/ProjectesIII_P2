using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private int health;
    private int actionPoints;
    [SerializeField] private float range;
    [SerializeField] private float oldRange;
    [SerializeField] private float shootingRange;
    [SerializeField] OG_MovementByMouse checkMovement;
    private bool isMoving;
    private bool isShoooting;
    private bool isAlive;
    public bool victory;
    public bool isHealing;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        isMoving = true;
        isShoooting = false;
        oldRange = range;
        checkMovement = GetComponent<OG_MovementByMouse>();
    }

    // Update is called once per frame
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
                        isMoving = true;
                        isShoooting = false;
                        isHealing = false;
                        range = oldRange;
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        isMoving = false;
                        isShoooting = true;
                        isHealing = false;
                        range = shootingRange;
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        isMoving = false;
                        isShoooting = false;
                        isHealing = true;
                        range = oldRange;
                        Heal(1); // Execute healing immediately
                    }
                }
            }
            else
            {
                isMoving = false;
                isShoooting = false;
                isHealing = false;
                checkMovement.enabled = false;
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

    public void Damage(int val = 1) { health -= val; Debug.Log("OOF"); }
    public void Heal(int amount) { health += amount; Debug.Log("Healed by " + amount); } // New heal method
    public float GetRange() { return range; }
    public void SetRange(float newRange) { range = newRange; }
    public float GetOldRange() { return oldRange; }
    public bool GetIsMoving() { return isMoving; }
    public bool GetIsShoooting() { return isShoooting; }
}

