using UnityEngine;

public class PlayerAnims : MonoBehaviour
{
    void Update()
    {
        Animator animator = this.GetComponent<Animator>();

        animator.SetBool("isMoving", this.GetComponent<OG_MovementByMouse>().GetIsMoving());
        animator.SetBool("isDead", !this.GetComponent<PlayerBase>().GetIsAlive());
    }
}
