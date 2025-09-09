using TMPro;
using UnityEngine;

public struct SignupData
{
    public string username;
    public string password;
    public string nickname;
}

public class SignupPanelController : PanelController
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_InputField confirmPasswordInputField;
    [SerializeField] private TMP_InputField nicknameInputField;

    public void OnClickConfirmButton()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;
        string nickname = nicknameInputField.text;
        string confirmPassword = confirmPasswordInputField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(nickname))
        {
            Shake();
            return;
        }

        if (password.Equals(confirmPassword))
        {
            var signupData = new SignupData();
            signupData.username = username;
            signupData.password = password;
            signupData.nickname = nickname;
            StartCoroutine(NetworkManager.Instance.Signup(signupData, () =>
            {
                GameManager.Instance.OpenConfirmPanel("회원가입에 성공했습니다", () =>
                {
                    Hide();
                });
            },
            (result) =>
            {
                if (result == 0)
                {
                    GameManager.Instance.OpenConfirmPanel("이미 존재하는 닉네임 입니다", () =>
                    {
                        usernameInputField.text = "";
                        passwordInputField.text = "";
                        confirmPasswordInputField.text = "";
                        nicknameInputField.text = "";
                    });
                }
            }
            ));
        }
        else
        {

        }


            // TD: Signin 함수로 Username. password 전달 함수
            

    }

    public void OnClickCancelButton()
    {
        Hide();
    }
}
