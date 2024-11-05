using UnityEngine;

public abstract class BulletPrefab : MonoBehaviour
{
    [SerializeField] public int range;
    [SerializeField] public float speed;
    [SerializeField] public float damage;
    [SerializeField] public bool isFromPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public abstract void Shoot(Vector3 origin, Vector3 target);

    
}
