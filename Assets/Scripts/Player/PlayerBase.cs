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

    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
        isShoooting = false;
        oldRange = range;
        checkMovement = GetComponent<OG_MovementByMouse>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!checkMovement.GetIsMoving()) { 
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                isMoving = true;
                isShoooting = false;
                range = oldRange;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                isMoving = false;
                isShoooting = true;
                range = shootingRange;
            
            }
        }


    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<EnemyMovement>() != null)
        {
            Damage();
        }
    }

    public void Damage(int val = 1) { health -= val; Debug.Log("OOF"); }
    public float GetRange() { return range; }
    public void SetRange(float newRange) { range = newRange; }
    public float GetOldRange() { return oldRange; }
    public bool GetIsMoving() { return isMoving; }
    public bool GetIsShoooting() { return isShoooting; }
}
