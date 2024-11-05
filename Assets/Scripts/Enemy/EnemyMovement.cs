using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    //[SerializeField]private MovementByMouse Player;
    [SerializeField]private OG_MovementByMouse Player;
    private Vector3 PlayerPos;
    float moveTime;
    [SerializeField] private GameObject bulletShot;
    [SerializeField] private float velocity;
    [SerializeField] private float range;
    // Start is called before the first frame update
    void Start()
    {
        
        Player = FindAnyObjectByType<OG_MovementByMouse>();
    }

    // Update is called once per frame
    void Update()
    {
        moveTime = Time.deltaTime * velocity;


        if (Vector3.Distance(PlayerPos, transform.position) < range && Player.GetIsMoving()) 
        {
            PlayerPos = Player.GetPosition();
        }
        else
        {
            if (Player.GetIsMoving() == true)
            {
                FollowPlayer(transform.position, PlayerPos, moveTime);

            }
            if (Player.GetIsMoving() == false)
            {
                PlayerPos = Player.GetPosition();
            }
        }
    }
    private void FollowPlayer(Vector3 origin, Vector3 target, float speed)
    {
        transform.position = Vector3.MoveTowards(origin,target,speed);
    }
}
