using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI rerollText;
    [SerializeField] private GameObject buttonPrefab; // Reference to the button prefab
    public Transform buttonContainer; // Reference to the container where buttons will be instantiated
    [SerializeField] public PlayerBase player;
    [SerializeField] public TextMeshProUGUI boughtItem;
    [SerializeField] public TextMeshProUGUI currentXP;

    public int rerollPrice;

    private List<PlayerData.ActionData> actionPool;
    private List<int> pricePool;
    private List<GameObject> buttons;

    public void Start()
    {
        InitializeShop();
        boughtItem.enabled = false;
        currentXP.text = player.playerData.exp + "";
    }

    public void Update()
    {
        currentXP.text = "Current EXP: " + player.playerData.exp + "";
    }

    public void Reroll()
    {
        if (player.playerData.exp >= rerollPrice)
        {
            player.playerData.exp -= rerollPrice;
            rerollPrice++;
            rerollText.text = rerollPrice + "";
            boughtItem.enabled = false;

            for (int i = 0; i < pricePool.Count; i++)
            {
                int randomPrice = Random.Range(10, 100); // Random price between 10 and 100
                pricePool[i] = randomPrice;
                buttons[i].transform.Find("Price").GetComponent<TextMeshProUGUI>().text = randomPrice.ToString();
            }
        }
    }

    public void BuyItem(TextMeshProUGUI itemText)
    {
        string itemName = itemText.text;
        PlayerData.ActionData actionData = actionPool.Find(action => GetActionDisplayName(action) == itemName);

        if (actionData != null)
        {
            // Check if the action already exists in the player's available actions
            bool actionExists = player.playerData.availableActions.Exists(action => action.action == actionData.action && action.style == actionData.style);

            if (actionExists)
            {
                boughtItem.enabled = true;
                boughtItem.text = "Action already equipped";
                return;
            }

            int index = buttons.FindIndex(button => button.transform.Find("Item Name").GetComponent<TextMeshProUGUI>().text == itemName);
            if (index != -1 && player.playerData.exp >= pricePool[index])
            {
                player.playerData.exp -= pricePool[index];

                EquipNewAction(actionData);

                boughtItem.enabled = true;
                boughtItem.text = "Just bought: " + itemName;
            }
            else
            {
                boughtItem.enabled = true;
                boughtItem.text = "Not enough experience";
            }
        }
    }

    private void EquipNewAction(PlayerData.ActionData actionData)
    {
        actionData.key = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + (player.playerData.availableActions.Count + 1));
        player.playerData.availableActions.Add(actionData);
    }

    private void IncreaseStat(PlayerData.ActionData actionData)
    {
        switch (actionData.action)
        {
            case PlayerBase.ActionEnum.SHOOT:
                actionData.style.damage += 1; // Increase bullet damage
                actionData.style.range += 3; // Increase bullet range
                break;
            case PlayerBase.ActionEnum.HEAL:
                player.playerData.healAmount += 5; // Increase healing value
                break;
            case PlayerBase.ActionEnum.MOVE:
                actionData.style.range += 1;
                player.playerData.baseRange += 1; // Increase move range
                break;
        }
    }

    private void InitializeShop()
    {
        actionPool = new List<PlayerData.ActionData>
        {
            new PlayerData.ActionData(PlayerBase.ActionType.ACTIVE, PlayerBase.ActionEnum.SHOOT, KeyCode.None, player.playerData.shotgun),
            new PlayerData.ActionData(PlayerBase.ActionType.ACTIVE, PlayerBase.ActionEnum.SHOOT, KeyCode.None, player.playerData.gun),
            new PlayerData.ActionData(PlayerBase.ActionType.PASSIVE, PlayerBase.ActionEnum.HEAL, KeyCode.None, player.playerData.healStyle),
            new PlayerData.ActionData(PlayerBase.ActionType.ACTIVE, PlayerBase.ActionEnum.MOVE, KeyCode.None, player.playerData.moveStyle),
            // Add other actions here
        };

        pricePool = new List<int>(4);
        buttons = new List<GameObject>(4);

        // Randomize actions and prices
        for (int i = 0; i < 4; i++) // Assuming there are only four buttons
        {
            int randomIndex = Random.Range(0, actionPool.Count);
            PlayerData.ActionData actionData = actionPool[randomIndex];

            // Instantiate button
            GameObject button = Instantiate(buttonPrefab, buttonContainer);
            buttons.Add(button);

            // Set item text
            TextMeshProUGUI itemText = button.transform.Find("Item Name").GetComponent<TextMeshProUGUI>();
            itemText.text = GetActionDisplayName(actionData);

            // Set price text
            int randomPrice = Random.Range(10, 100); // Random price between 10 and 100
            pricePool.Add(randomPrice);
            TextMeshProUGUI priceText = button.transform.Find("Price").GetComponent<TextMeshProUGUI>();
            priceText.text = randomPrice.ToString();

            // Add button click listener
            button.GetComponent<Button>().onClick.AddListener(() => BuyItem(itemText));
        }
    }

    private string GetActionDisplayName(PlayerData.ActionData actionData)
    {
        if (actionData.action == PlayerBase.ActionEnum.SHOOT)
        {
            if (actionData.style == player.playerData.gun)
            {
                return "Gun";
            }
            else if (actionData.style == player.playerData.shotgun)
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
