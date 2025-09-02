using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmPanelController : PanelController
{
    [SerializeField] private TMP_Text messageText;

    /// <summary>
    /// Confirm Panel을 표시
    /// </summary>

    public void Show(string message)
    {
        messageText.text = message;
        base.Show();
    }

    /// <summary>
    /// 확인 버튼 클릭시 호출되는 메서드
    /// </summary>
    
    public void OnClickConfirmButton()
    {
        Hide();
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// X 버튼 클릭시 호출되는 메서드
    /// </summary>

    public void OnClickCloseButton()
    {
        Hide();
    }
}
