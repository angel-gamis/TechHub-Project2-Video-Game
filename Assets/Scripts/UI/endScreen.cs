using UnityEngine;
using UnityEngine.UI;

public class endScreen : MonoBehaviour
{
	[SerializeField] private Button restartBtn;
	[SerializeField] private Button quitBtn;

	// Start is called before the first frame update
	void Start()
    {
        restartBtn.onClick.AddListener(RestartGame);
		restartBtn.onClick.AddListener(QuitGame);
	}

	private void RestartGame()
	{
		scenesManager.Instance.StartGame();
	}

		private void QuitGame()
	{
		scenesManager.Instance.QuitGame();
	}
}
