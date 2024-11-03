using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]private MovementByMouse Player;
    [SerializeField]private OG_MovementByMouse OGPlayer;
    private Vector3 PlayerPos;
    float moveTime;
    [SerializeField] private float velocity;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindAnyObjectByType<MovementByMouse>();
        OGPlayer = FindAnyObjectByType<OG_MovementByMouse>();
    }

    // Update is called once per frame
    void Update()
    {
        moveTime = Time.deltaTime * velocity;
       
        
        
        if (Player.GetIsMoving() == true)
        {
          
           transform.position = Vector3.MoveTowards(transform.position, PlayerPos, moveTime);
        }
        if(Player.GetIsMoving() == false)
        {
            PlayerPos = Player.GetPosition();
        }
    }
}
