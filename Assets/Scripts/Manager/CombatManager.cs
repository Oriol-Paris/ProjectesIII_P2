using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public List<PlayerBase> playerParty = new List<PlayerBase>();  // Lista para jugadores
    public EnemyMovement[] enemyParty;  // Array para enemigos

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

        // Obtener todos los objetos de tipo EnemyMovement en la escena
        enemyParty = GameObject.FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        // Lógica de combate u otros eventos
    }
}
