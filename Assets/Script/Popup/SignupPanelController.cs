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
                GameManager.Instance.OpenConfirmPanel("ȸ�����Կ� �����߽��ϴ�", () =>
                {
                    Hide();
                });
            },
            (result) =>
            {
                if (result == 0)
                {
                    GameManager.Instance.OpenConfirmPanel("�̹� �����ϴ� �г��� �Դϴ�", () =>
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


            // TD: Signin �Լ��� Username. password ���� �Լ�
            

    }

    public void OnClickCancelButton()
    {
        Hide();
    }
}
