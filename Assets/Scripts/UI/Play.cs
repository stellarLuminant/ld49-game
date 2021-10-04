using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public void OnButtonPressed()
    {
        GameManager.CurrentLevel = 0;
        UIManager.Instance.State = UIManager.GameState.Cutscene;
        SceneManager.LoadScene(GameManager.StoryStartScene);
    }
}
