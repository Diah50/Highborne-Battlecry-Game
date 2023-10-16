using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            ChangeSceneToGameScene();
        }
    }

    public void ChangeSceneToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
