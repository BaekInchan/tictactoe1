using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameObject playerATurnPanel;
    [SerializeField] private GameObject playerBTurnPanel;

    public enum GameTurnPanelType { None, ATurn, BTurn }

    public void OnClickBackButton()
    {
        //GameManager.Instance.ChangeToMainScene();

        GameManager.Instance.OpenConfirmPanel("게임을 종료하시겠습니까?", () => { GameManager.Instance.ChangeToMainScene(); });
        
    }

    public void SetGameTurnPanel(GameTurnPanelType gameTrunPanelType)
    {
        switch (gameTrunPanelType)
        {
            case GameTurnPanelType.None:
                playerATurnPanel.SetActive(false);
                playerBTurnPanel.SetActive(false);
                break;
            case GameTurnPanelType.ATurn:
                playerATurnPanel.SetActive(true);
                playerBTurnPanel.SetActive(false);
                break;
            case GameTurnPanelType.BTurn:
                playerATurnPanel.SetActive(false);
                playerBTurnPanel.SetActive(true);
                break;
        }

        
    }
}
