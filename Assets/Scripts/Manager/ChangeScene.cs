using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI expValue;
    [SerializeField] CombatManager expObtained;
    public void GoToShop()
    {
        FindAnyObjectByType<PlayerData>().lastClearedLevel = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("ShopScene");
    }

    void Start()
    {
        
    }

    void Update()
    {
        expValue.text = "Exp: "+expObtained.playerParty[0].exp+"";
    }
}
