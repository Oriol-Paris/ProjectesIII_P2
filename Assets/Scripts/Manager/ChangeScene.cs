using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI expValue;
    [SerializeField] CombatManager expObtained;
    public void GoToShop()
    {
        SceneManager.LoadScene("ShopScene");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        expValue.text = "Exp: "+expObtained.playerParty[0].exp+"";
    }
}
