using UnityEngine;

public class PlayerAnims : MonoBehaviour
{
    void Update()
    {
        Animator animator = this.GetComponent<Animator>();

        animator.SetBool("player_isMoving", this.GetComponent<OG_MovementByMouse>().GetIsMoving());
        animator.SetBool("player_isDead", !this.GetComponent<PlayerBase>().GetIsAlive());
        animator.SetBool("player_isAttacking", this.GetComponent<OG_MovementByMouse>().GetIsMoving() && this.GetComponent<PlayerActionManager>().isShooting);
        //animator.SetBool("player_isHit", this.GetComponent<PlayerBase>().GetIsHit());
    }
}
