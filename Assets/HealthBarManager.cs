using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private PlayerBase m_Player;
    [SerializeField] private Slider health;
    [SerializeField] private Slider actionPoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health.value =(float)(m_Player.health/m_Player.maxHealth);
        actionPoints.value = (float)(m_Player.actionPoints / m_Player.maxActionPoints);
    }
}
