using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public void OnButtonPressed()
    {
        GameManager.CurrentLevel = 0;
        SceneManager.LoadScene(GameManager.StoryStartScene);
        UIManager.Instance.State = UIManager.GameState.Cutscene;
    }
}
