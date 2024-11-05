using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private int health;
    private int actionPoints;
    [SerializeField] private float range;

    private bool isMoving;
    private bool isShoooting;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
        isShoooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isMoving = true;
            isShoooting = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isMoving = false;
            isShoooting = true;
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
    public bool GetIsMoving() { return isMoving; }
    public bool GetIsShoooting() { return isShoooting; }
}
