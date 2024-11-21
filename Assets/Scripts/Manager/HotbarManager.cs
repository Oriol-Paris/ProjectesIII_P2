using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class HotbarManager : MonoBehaviour
{
    public PlayerActionManager playerActionManager;
    public PlayerBase playerData;
    public GameObject hotbarPanel;
    public GameObject actionSlotPrefab;

    private List<GameObject> actionSlots = new List<GameObject>();
    float originalCount;
    void Start()
    {
        InitializeHotbar();
        originalCount = actionSlots.Count;
    }

    void InitializeHotbar()
    {
        
       //playerData = playerActionManager.GetPlayer();
        if (playerData == null)
        {
            Debug.LogError("Player is null in HotbarManager.");
            return;
        }

        foreach (var action in playerData.playerData.availableActions)
        {
            GameObject slot = Instantiate(actionSlotPrefab, hotbarPanel.transform);
            slot.GetComponentInChildren<Text>().text = action.action.ToString();
            actionSlots.Add(slot);
        }
    }

    void Update()
    {
        UpdateHotbar();
        if(originalCount != actionSlots.Count) { 
            foreach(var action in actionSlots)
            {
                Destroy(action);
            }
            actionSlots.Clear();
            InitializeHotbar();
            originalCount = actionSlots.Count;
        }
    }

    void UpdateHotbar()
    {
        if (playerActionManager == null)
        {
            Debug.LogError("Player is null in HotbarManager.");
            return;
        }

        var player = playerActionManager.GetPlayer();

        var currentAction = player.GetAction();

        for (int i = 0; i < actionSlots.Count; i++)
        {
            var actionData = player.playerData.availableActions[i];
            var slot = actionSlots[i];

            if (currentAction.m_action == actionData.action)
            {
                slot.GetComponent<Image>().color = Color.yellow; // Highlight selected action
            }
            else
            {
                slot.GetComponent<Image>().color = Color.white; // Default color
            }
        }
    }
}