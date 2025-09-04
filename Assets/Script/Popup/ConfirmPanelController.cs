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
    
    // Confirm 클릭시 호출될 Delegate
    public delegate void OnConfirmButtonClicked();
    private OnConfirmButtonClicked _onConfirmButtonClicked;

    public void Show(string message, OnConfirmButtonClicked onConfirmButtonClicked)
    {
        messageText.text = message;
        _onConfirmButtonClicked = onConfirmButtonClicked;
        base.Show();
    }

    /// <summary>
    /// 확인 버튼 클릭시 호출되는 메서드
    /// </summary>
    
    public void OnClickConfirmButton()
    {
        Hide(() => _onConfirmButtonClicked?.Invoke());
    }

    /// <summary>
    /// X 버튼 클릭시 호출되는 메서드
    /// </summary>

    public void OnClickCloseButton()
    {
        Hide();
    }
}
