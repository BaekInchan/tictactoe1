using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmPanelController : PanelController
{
    [SerializeField] private TMP_Text messageText;

    /// <summary>
    /// Confirm Panel�� ǥ��
    /// </summary>
    
    // Confirm Ŭ���� ȣ��� Delegate
    public delegate void OnConfirmButtonClicked();
    private OnConfirmButtonClicked _onConfirmButtonClicked;

    public void Show(string message, OnConfirmButtonClicked onConfirmButtonClicked)
    {
        messageText.text = message;
        _onConfirmButtonClicked = onConfirmButtonClicked;
        base.Show();
    }

    /// <summary>
    /// Ȯ�� ��ư Ŭ���� ȣ��Ǵ� �޼���
    /// </summary>
    
    public void OnClickConfirmButton()
    {
        Hide(() => _onConfirmButtonClicked?.Invoke());
    }

    /// <summary>
    /// X ��ư Ŭ���� ȣ��Ǵ� �޼���
    /// </summary>

    public void OnClickCloseButton()
    {
        Hide();
    }
}
