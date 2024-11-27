using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PlayerData;

public class HotbarManager : MonoBehaviour
{
    public PlayerActionManager playerActionManager;
    public PlayerBase playerData;
    public GameObject hotbarPanel;
    public GameObject actionSlotPrefab;

    private List<GameObject> actionSlots = new List<GameObject>();
    private List<PlayerData.ActionData> actionsDisplayed = new List<PlayerData.ActionData>();
    float originalCount;
    void Start()
    {
        InitializeHotbar();
        originalCount = actionSlots.Count;
    }

    void InitializeHotbar()
    {
        playerData = playerActionManager.GetPlayer();
        if (playerData == null)
        {
            Debug.LogError("Player is null in HotbarManager.");
            return;
        }

        foreach (var action in playerData.playerData.availableActions)
        {
            if (SceneManager.GetActiveScene().name == "ShopScene")
            {
                GameObject slot = Instantiate(actionSlotPrefab, hotbarPanel.transform);
                slot.transform.Find("Texts").transform.Find("Action Name").GetComponent<TextMeshProUGUI>().text = FindAnyObjectByType<ShopManager>().GetActionDisplayName(action);
                slot.transform.Find("Action Image").GetComponent<Image>().overrideSprite = FindAnyObjectByType<ShopManager>().GetActionImage(action);
                slot.transform.Find("Action Image").GetComponent<Image>().preserveAspect = true;
                slot.transform.Find("Texts").transform.Find("Action Type").GetComponent<TextMeshProUGUI>().text = action.actionType.ToString();
                if (action.actionType == PlayerBase.ActionType.ACTIVE)
                {
                    slot.transform.Find("Texts").transform.Find("Action Type").gameObject.SetActive(false);
                }
                if (action.actionType == PlayerBase.ActionType.PASSIVE || action.actionType == PlayerBase.ActionType.SINGLE_USE)
                    slot.transform.Find("Texts").transform.Find("Action Stats").gameObject.SetActive(false);
                else
                {
                    slot.transform.Find("Texts").transform.Find("Action Stats").GetComponent<TextMeshProUGUI>().text =
                        "Range: " + action.style.range + "\nDamage: " + action.style.damage;
                }
                
                actionSlots.Add(slot);
                actionsDisplayed.Add(action);
                Debug.Log(action.actionType);
            }
            else
            {
                GameObject slot = Instantiate(actionSlotPrefab, hotbarPanel.transform);

                slot.transform.Find("Action Image").GetComponent<Image>().enabled = false;

                slot.transform.Find("Texts").transform.Find("Action Name").GetComponent<TextMeshProUGUI>().text = GetActionName(action);
                slot.transform.Find("Texts").transform.Find("Action Name").position =
                    new Vector3(slot.transform.Find("Texts").transform.position.x + 70,
                        slot.transform.Find("Texts").transform.Find("Action Name").position.y,
                        slot.transform.Find("Texts").transform.Find("Action Name").position.z);

                slot.transform.Find("Texts").transform.Find("Action Type").GetComponent<TextMeshProUGUI>().text = action.actionType.ToString();
                slot.transform.Find("Texts").transform.Find("Action Type").position =
                    new Vector3(slot.transform.Find("Texts").transform.position.x + 70,
                        slot.transform.Find("Texts").transform.Find("Action Type").position.y,
                        slot.transform.Find("Texts").transform.Find("Action Type").position.z);

                if (action.actionType == PlayerBase.ActionType.PASSIVE || action.actionType == PlayerBase.ActionType.SINGLE_USE)
                {
                    slot.transform.Find("Texts").transform.Find("Action Stats").gameObject.SetActive(false);
                }
                else
                {
                    slot.transform.Find("Texts").transform.Find("Action Stats").GetComponent<TextMeshProUGUI>().text =
                        "Range: " + action.style.range + "\nDamage: " + action.style.damage;
                    slot.transform.Find("Texts").transform.Find("Action Stats").position =
                        new Vector3(slot.transform.Find("Texts").transform.position.x + 70,
                        slot.transform.Find("Texts").transform.Find("Action Stats").position.y,
                        slot.transform.Find("Texts").transform.Find("Action Stats").position.z);
                }
                TextMeshProUGUI inputText = slot.transform.Find("Texts").transform.Find("Action Input").GetComponent<TextMeshProUGUI>();
                if (inputText != null)
                {
                    string inputKey = action.key.ToString();  // Accede a la clave de la acci�n, que es el input asignado
                    inputText.text = inputKey;  // Muestra el input asignado a la acci�n
                }
                actionSlots.Add(slot);
                actionsDisplayed.Add(action);
            }
        }
    }

    void Update()
    {
        var availableActions = FindAnyObjectByType<PlayerBase>().playerData.availableActions;
        if (availableActions.Count > actionsDisplayed.Count && actionSlots.Count < 4)
        {
            foreach (PlayerData.ActionData action in availableActions)
            {
                bool exists = false;

                // Asegúrate de que la acción no exista en actionsDisplayed antes de agregarla
                foreach (PlayerData.ActionData da in actionsDisplayed)
                {
                    if (action.action == da.action && action.style == da.style)
                    {
                        exists = true;
                        break; // Si ya existe, no la añadimos
                    }
                }

                if (!exists)
                {
                    GameObject slot = Instantiate(actionSlotPrefab, hotbarPanel.transform);
                    slot.transform.Find("Texts").transform.Find("Action Name").GetComponent<TextMeshProUGUI>().text = FindAnyObjectByType<ShopManager>().GetActionDisplayName(action);
                    slot.transform.Find("Action Image").GetComponent<Image>().overrideSprite = FindAnyObjectByType<ShopManager>().GetActionImage(action);
                    slot.transform.Find("Action Image").GetComponent<Image>().preserveAspect = true;
                    slot.transform.Find("Texts").transform.Find("Action Type").GetComponent<TextMeshProUGUI>().text = action.actionType.ToString();

                    if (action.actionType == PlayerBase.ActionType.PASSIVE || action.actionType == PlayerBase.ActionType.SINGLE_USE)
                        slot.transform.Find("Texts").transform.Find("Action Stats").gameObject.SetActive(false);
                    else
                    {
                        slot.transform.Find("Texts").transform.Find("Action Stats").GetComponent<TextMeshProUGUI>().text =
                            "Range: " + action.style.range + "\nDamage: " + action.style.damage;
                    }

                    actionSlots.Add(slot);
                    actionsDisplayed.Add(action);  // Agregar a la lista de acciones mostradas
                }
            }
        }

        UpdateHotbar();
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

        for (int i = 0; i < player.playerData.availableActions.Count; i++)
        {
            var actionData = player.playerData.availableActions[i];

            var slot = actionSlots[i];
            if (actionData.actionType == PlayerBase.ActionType.ACTIVE)
            {
                slot.transform.Find("Texts").transform.Find("Action Type").gameObject.SetActive(false);
            }
            if (actionData.actionType == PlayerBase.ActionType.PASSIVE || actionData.actionType == PlayerBase.ActionType.SINGLE_USE)
                slot.transform.Find("Texts").transform.Find("Action Stats").gameObject.SetActive(false);
            else
            {
                slot.transform.Find("Texts").transform.Find("Action Stats").GetComponent<TextMeshProUGUI>().text =
                    "Range: " + actionData.style.range + "\nDamage: " + actionData.style.damage;
            }

            if (currentAction.m_action == actionData.action && currentAction.m_style == actionData.style && SceneManager.GetActiveScene().name != "ShopScene")
            {
                slot.GetComponent<Image>().color = Color.yellow; // Highlight selected action
            }
            else
            {
                slot.GetComponent<Image>().color = Color.white; // Default color
            }
        }
    }

    string GetActionName(PlayerData.ActionData actionData)
    {
        if (actionData.action == PlayerBase.ActionEnum.SHOOT)
        {
            if (actionData.style.prefab == FindAnyObjectByType<PlayerBase>().playerData.gun.prefab)
            {
                return "Gun";
            }
            else if (actionData.style.prefab == FindAnyObjectByType<PlayerBase>().playerData.shotgun.prefab)
            {
                return "Shotgun";
            }
        }
        else if (actionData.action == PlayerBase.ActionEnum.HEAL)
        {
            return "Heal";
        }
        else if (actionData.action == PlayerBase.ActionEnum.MOVE)
        {
            return "Move";
        }
        return actionData.action.ToString();
    }
}