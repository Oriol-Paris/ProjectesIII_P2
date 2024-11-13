using UnityEngine;
using TMPro;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI rerollText;
    [SerializeField] public List<TextMeshProUGUI> itemTexts;
    [SerializeField] public List<TextMeshProUGUI> pricePool;
    public int rerollPrice;

    public void Start()
    {
        itemTexts[0].text = "Shotgun";
        itemTexts[1].text = "Heal";

        pricePool[0].text = Random.Range(50, 100)+"";
        pricePool[1].text = Random.Range(50, 100)+"";
    }

    public void Reroll()
    {
        rerollPrice++;
        rerollText.text = rerollPrice+"";

        for(int i = 0; i < pricePool.Count; i++)
        {
            pricePool[i].text = Random.Range(50, 100).ToString();
        }

        /*
        foreach (Button button in FindObjectsOfType<Button>())
        {
            button.GetComponent<GameObject>().SetActive(true);
        }
        */
    }

    public void BuyItem(TextMeshProUGUI tmp)
    {
        PlayerBase player = GetComponent<PlayerBase>();

        switch(tmp.text)
        {
            case "Shotgun":
                player.AddNewAction(new PlayerBase.Action(PlayerBase.ActionType.ACTIVE, PlayerBase.ActionEnum.SHOOT, KeyCode.Alpha3, player.playerData.shotgun));
                break;

            case "Heal":
                player.AddNewAction(new PlayerBase.Action(PlayerBase.ActionType.PASSIVE, PlayerBase.ActionEnum.HEAL, KeyCode.Alpha4));
                break;

            default: 
                break;
        }
    }
}
