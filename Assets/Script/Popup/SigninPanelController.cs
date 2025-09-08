using DG.Tweening;
using TMPro;
using UnityEngine;
public struct SigninData
{
    public string username;
    public string password;
}

public struct SigninResult
{
    public int result;
}

public class SigninPanelController : PanelController
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    // Confirm 클릭시 호출될 Delegate

    public override void Show()
    {
        base.Show();
    }

    /// <summary>
    /// 확인 버튼 클릭시 호출되는 메서드
    /// </summary>

    public void OnClickConfirmButton()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Shake();
            return;
        }

        var signinData = new SigninData();
        signinData.username = username;
        signinData.password = password;

        // TD: Signin 함수로 Username. password 전달 함수
        StartCoroutine(NetworkManager.Instance.Signin(signinData, () =>
        {
            Hide();
        }, 
        (result) =>
        {
            if(result == 0)
            {
                GameManager.Instance.OpenConfirmPanel("유저 네임이 유효하지 않습니다", () =>
                {
                    usernameInputField.text = "";
                    passwordInputField.text = "";
                });
            }
            else if(result == 1) 
            {
                GameManager.Instance.OpenConfirmPanel("패스워드가 유효하지 않습니다", () =>
                {
                    passwordInputField.text = "";
                });
            }
        }
        ));

    }

    public void OnClickSignupButton()
    {
        GameManager.Instance.OpenSignupPanel();
    }
}
