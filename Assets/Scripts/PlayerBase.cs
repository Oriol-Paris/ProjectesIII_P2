using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private int health;
    private int actionPoints;
    [SerializeField] private float range;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<EnemyMovement>() != null)
        {
            health--;
            Debug.Log("OOF");
        }
    }
    public float GetRange() { return range; }
}
