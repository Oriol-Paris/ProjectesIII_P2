using UnityEngine;

public class GunBullet : BulletPrefab
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFromPlayer)
        {
            if (collision.gameObject.GetComponent<EnemyMovement>() != null)
            {
                //Pupa a enemy
                Debug.Log("Bullet Hit Enemy");
                Destroy(this);
            }
        } 
        else
        {
            if (collision.gameObject.GetComponent<PlayerBase>() != null)
            {
                //Pupa
                collision.gameObject.GetComponent<PlayerBase>().Damage();
                Destroy(this);
            }
        }
    }
}
