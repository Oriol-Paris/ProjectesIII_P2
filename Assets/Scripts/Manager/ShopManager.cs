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
        priceTexts[0].text = pricePool[0].ToString();
        priceTexts[1].text = pricePool[1].ToString();
        pricePool[0] = Random.Range(5, 10);
        pricePool[1] = Random.Range(5, 10);
        currentXP.text = player.playerData.exp + "";
    }
    public void Update()
    {
        currentXP.text = player.playerData.exp + "";
    }
    public void Reroll()
    {
        if(player.playerData.exp>=rerollPrice) { 
            
            player.playerData.exp -= rerollPrice;
            rerollPrice++;
            rerollText.text = rerollPrice+"";

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
                    if (player.playerData.exp >= pricePool[i])
                    {
                        player.playerData.exp -= pricePool[i];
                        player.playerData.availableActions.Add(new PlayerData.ActionData
                        (PlayerBase.ActionType.ACTIVE, PlayerBase.ActionEnum.SHOOT, KeyCode.Alpha3, player.playerData.shotgun));
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

            case "Heal":
                for (int i = 0; i < pricePool.Count; i++)
                {
                    if (player.playerData.exp >= pricePool[i])
                    {
                        player.playerData.exp -= pricePool[i];
                        player.AddNewAction(new PlayerBase.Action(PlayerBase.ActionType.PASSIVE, PlayerBase.ActionEnum.HEAL, KeyCode.Alpha4));
                        boughtItem.enabled = true;
                        boughtItem.text = "Just bought: Heal";

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
