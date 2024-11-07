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
           
            if (collision.gameObject.CompareTag("Enemy"))
            {
                isHit = true;
                Destroy(this.gameObject);
                Destroy(this);
                Debug.Log(isHit);
                Destroy(collision.gameObject);
                
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
