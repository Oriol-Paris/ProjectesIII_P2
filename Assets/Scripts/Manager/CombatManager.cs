using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public List<PlayerBase> playerParty = new List<PlayerBase>();  // Lista para jugadores
    public EnemyBase[] enemyParty;  // Array para enemigos
    bool allEnemiesDead;
    int turnNumber;
    [SerializeField] Canvas winCondition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Obtener todos los objetos de tipo PlayerBase en la escena
        PlayerBase[] players = GameObject.FindObjectsByType<PlayerBase>(FindObjectsSortMode.None);

        // Agregar los objetos encontrados a la lista playerParty
        foreach (PlayerBase player in players)
        {
            playerParty.Add(player);
        }
        winCondition.enabled = false;
        // Obtener todos los objetos de tipo EnemyMovement en la escena
        enemyParty = GameObject.FindObjectsByType<EnemyBase>(FindObjectsSortMode.None);
        allEnemiesDead = true;
    }

    // Update is called once per frame
    void Update()
    {
        allEnemiesDead = true;

        for (int i = 0; i<enemyParty.Length; i++)
        {
            if (enemyParty[i].isAlive)
                allEnemiesDead = false;
            winCondition.enabled = false;
        }
        if(allEnemiesDead)
        {
            winCondition.enabled = true;
            for(int i = 0; i<playerParty.Count; i++)
            {
                playerParty[i].victory = true;
            }
        }

    }
}
