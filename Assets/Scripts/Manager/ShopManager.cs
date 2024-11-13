using UnityEngine;
using TMPro;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public enum ShopItems { SHOTGUN, HEAL }

    public TextMeshProUGUI rerollText;
    [SerializeField] public List<TextMeshProUGUI> itemTexts;
    [SerializeField] public List<TextMeshProUGUI> pricePool;
    public List<ShopItems> itemPool;
    public int rerollPrice;

    public void Start()
    {
        itemPool = new List<ShopItems>();

        itemPool.Add(ShopItems.SHOTGUN);
        itemPool.Add(ShopItems.HEAL);

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

        /*foreach (Button button in FindObjectsOfType<Button>())
        {
            button.GetComponent<GameObject>().SetActive(true);
        }*/
    }

    public void BuyItem()
    {
        //Do something
    }
}
