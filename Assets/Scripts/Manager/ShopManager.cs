using UnityEngine;
using TMPro;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI rerollText;
    [SerializeField] public List<TextMeshProUGUI> itemTexts;
    [SerializeField] public List<int> pricePool;
    [SerializeField] public List<TextMeshProUGUI> priceTexts;
    [SerializeField] public PlayerBase player;
    [SerializeField] public TextMeshProUGUI boughtItem;
    [SerializeField] public TextMeshProUGUI currentXP;

    public int rerollPrice;

    public void Start()
    {
        boughtItem.enabled = false;
        itemTexts[0].text = "Shotgun";
        itemTexts[1].text = "Heal";
        pricePool[0] = Random.Range(5, 10);
        pricePool[1] = Random.Range(5, 10);
        priceTexts[0].text = pricePool[0].ToString();
        priceTexts[1].text = pricePool[1].ToString();
        
        currentXP.text = player.playerData.exp + "";
    }
    public void Update()
    {
        currentXP.text = "Current EXP: "+player.playerData.exp + "";
    }
    public void Reroll()
    {
        if(player.playerData.exp>=rerollPrice) { 
            
            player.playerData.exp -= rerollPrice;
            rerollPrice++;
            rerollText.text = rerollPrice+"";
            boughtItem.enabled = false;

            for(int i = 0; i < pricePool.Count; i++)
            {
            pricePool[i] = Random.Range(5, 10);
            priceTexts[i].text = pricePool[i]+"";
            }
        }
        
    }

    public void BuyItem(TextMeshProUGUI tmp)
    {
       

        switch(tmp.text)
        {
            case "Shotgun":
                for (int i = 0; i < pricePool.Count; i++) {
                    if (itemTexts[i].text == "Shotgun")
                    if (player.playerData.exp >= pricePool[i])
                    {
                        player.playerData.exp -= pricePool[i];
                            player.playerData.availableActions.Add(new PlayerData.ActionData
                                                            (PlayerBase.ActionType.PASSIVE, PlayerBase.ActionEnum.SHOOT,
                                                            (KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + (player.playerData.availableActions.Count + 1)),
                                                            player.playerData.shotgun)); boughtItem.enabled = true;
                        boughtItem.text = "Just bought: Shotgun";
                    }
                    else
                    {
                        boughtItem.enabled = true;
                        boughtItem.text = "Not enough experience";
                    }
                    
                }
                break;

            case "Heal":
                for (int i = 0; i < pricePool.Count; i++)
                {
                    if (itemTexts[i].text == "Heal")
                        if (player.playerData.exp >= pricePool[i])
                        {
                            player.playerData.exp -= pricePool[i];
                            player.playerData.availableActions.Add(new PlayerData.ActionData
                                (PlayerBase.ActionType.PASSIVE, PlayerBase.ActionEnum.HEAL, 
                                (KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + (player.playerData.availableActions.Count + 1)), 
                                player.playerData.gun));
                            boughtItem.enabled = true;
                            boughtItem.text = "Just bought: Shotgun";
                        }
                        else
                        {
                            boughtItem.enabled = true;
                            boughtItem.text = "Not enough experience";
                        }
                }
                break;

            default: 
                break;
        }
    }
}
