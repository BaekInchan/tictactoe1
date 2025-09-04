using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    public void OnClickBackButton()
    {
        //GameManager.Instance.ChangeToMainScene();

        GameManager.Instance.OpenConfirmPanel("게임을 종료하시겠습니까?", () => { GameManager.Instance.ChangeToMainScene(); });
        
    }


}
