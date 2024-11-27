using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour
{
    [SerializeField] OG_MovementByMouse player;
    [SerializeField] PlayerActionManager changeDialogueConditions;
    public TextMeshProUGUI textComponent;
    public string[] intro;
    public string[] postWalk;
    public string[] attack;
    public string[] rest;
    public List<string[]> tutorialBoxes;
    public float textSpeed;

    private int index;
    private int globalIndex;

    void Start()
    {
        // Agregamos las secciones de diálogo al tutorialBoxes
        tutorialBoxes = new List<string[]>();
        tutorialBoxes.Add(intro);
        tutorialBoxes.Add(postWalk);
        tutorialBoxes.Add(attack);
        tutorialBoxes.Add(rest);

        textComponent.text = string.Empty;
        StartDialogue();  // Comienza el diálogo
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))  // Si se hace click con el mouse
        {
            if (textComponent.text == tutorialBoxes[globalIndex][index])  // Si el texto está completo
            {
                NextLine();  // Avanza a la siguiente línea
            }
            else
            {
                // Si no está completo, se detiene la corutina y se muestra el texto completo
                StopAllCoroutines();
                textComponent.text = tutorialBoxes[globalIndex][index];
            }
        }
       
    }

    // Inicia el diálogo desde la primera línea
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());  // Comienza a escribir el primer texto
        player.enabled = false;  // Deshabilita el movimiento del jugador
    }

    // Corutina para escribir el texto una letra a la vez
    IEnumerator TypeLine()
    {
        foreach (char c in tutorialBoxes[globalIndex][index].ToCharArray())
        {
            textComponent.text += c;  // Añade una letra a la vez al texto
            yield return new WaitForSeconds(textSpeed);  // Espera según el tiempo definido por textSpeed
        }
    }

    // Avanza a la siguiente línea de texto
    void NextLine()
    {
        if (index < tutorialBoxes[globalIndex].Length - 1)  // Si hay más líneas en el diálogo actual
        {
            index++;  // Avanza al siguiente índice
            textComponent.text = string.Empty;  // Limpia el texto actual
            StartCoroutine(TypeLine());  // Inicia la corutina para escribir la siguiente línea
        }
        else
        {
            // Si se han mostrado todas las líneas del diálogo actual
            gameObject.SetActive(false);  // Desactiva el objeto del diálogo
            player.enabled = true;  // Vuelve a habilitar el movimiento del jugador
        }
    }

    
}
