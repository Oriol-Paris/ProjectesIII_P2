using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToFight : MonoBehaviour
{
    public void NextRound()
    {
        if (FindAnyObjectByType<PlayerBase>().playerData.levelCompleted)
        {
            switch (FindAnyObjectByType<PlayerBase>().playerData.lastLevel)
            {
                case "Level1":
                    SceneManager.LoadScene("Level2");
                    break;
                case "Level2":
                    SceneManager.LoadScene("Level3");
                    break;
                case "Level3":
                    SceneManager.LoadScene("Level4");
                    break;
                case "Level4":
                    SceneManager.LoadScene("Level5");
                    break;
                case "Level5":
                    SceneManager.LoadScene("Level1");
                    break;
            }
        }
        else
            SceneManager.LoadScene(FindAnyObjectByType<PlayerBase>().playerData.lastLevel);
    }
}
