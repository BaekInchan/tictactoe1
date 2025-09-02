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

    public void Show(string message)
    {
        messageText.text = message;
        base.Show();
    }

    /// <summary>
    /// Ȯ�� ��ư Ŭ���� ȣ��Ǵ� �޼���
    /// </summary>
    
    public void OnClickConfirmButton()
    {
        Hide();
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// X ��ư Ŭ���� ȣ��Ǵ� �޼���
    /// </summary>

    public void OnClickCloseButton()
    {
        Hide();
    }
}
