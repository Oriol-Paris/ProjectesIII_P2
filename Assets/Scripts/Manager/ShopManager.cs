using UnityEngine;
using TMPro;
public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI rerollText;
    public int rerollPrice;
    public void Reroll()
    {
        rerollPrice++;
        rerollText.text = rerollPrice+"";
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
