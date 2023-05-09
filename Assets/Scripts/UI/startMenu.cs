using UnityEngine;
using UnityEngine.UI;

public class startMenu : MonoBehaviour
{

    [SerializeField] private Button startBtn;
	[SerializeField] private Button quitBtn;

	// Start is called before the first frame update
	void Start()
    {
        // When "start" button is clicks used StartGame() Method
        startBtn.onClick.AddListener(StartGame);
		// When "quit" button is clicks used QuitGame() Method
		quitBtn.onClick.AddListener(QuitGame);
	}

    private void StartGame()
    {
        scenesManager.Instance.StartGame();
        //scenesManager.Instance.LoadScene(scenesManager.Scene.Level00);
    }

    private void QuitGame()
    {
        scenesManager.Instance.QuitGame();
    }
}
