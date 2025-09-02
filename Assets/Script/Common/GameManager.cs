using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject confirmPanel;

    // MainScene���� ������ 
    private Constants.GameType _gameType;

    // Panel�� ���� ���� Canvas ����
    private Canvas _canvas;
   
    /// <summary>
    /// Main���� Game Scene���� ��ȯ�� ȣ��� �޼���
    /// </summary>



    public void ChangeToGameScene(Constants.GameType gameType)
    {
        // 0 : single, 1 : dual, 2: Multi
        _gameType = gameType;
        SceneManager.LoadScene("Game");
    }


    /// <summary>
    /// Game���� Main���� ��ȯ�� ȣ��� �޼���
    /// </summary>

    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");

    }

    /// <summary>
    /// Confirm Panel�� ���� �ż���
    /// </summary>
    /// <param name="message"></param>

    public void OpenConfirmPanel(string message)
    {
        if(_canvas != null)
        {
            var confirmPanelObject = Instantiate(confirmPanel, _canvas.transform);
            confirmPanelObject.GetComponent<ConfirmPanelController>().Show(message);
        }
    }

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        _canvas = FindFirstObjectByType<Canvas>();
    }
}
