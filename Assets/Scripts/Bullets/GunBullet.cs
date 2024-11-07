using UnityEngine;

public class GunBullet : BulletPrefab
{
    public bool isHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (isFromPlayer)
        {
           
            if (collision.gameObject.GetComponent<EnemyBase>()!=null)
            {
                if (collision.gameObject.GetComponent<EnemyBase>().GetHealth() > 0) { 
                collision.gameObject.GetComponent<EnemyBase>().Damage(damage);
                isHit = true;
                Destroy(this.gameObject);
                Destroy(this);
                Debug.Log(isHit);
                }

            }
            if (isHit)
            {
                
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
