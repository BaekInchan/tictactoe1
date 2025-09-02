using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // MainScene에서 선택한 
    private Constants.GameType _gameType;

    /// <summary>
    /// Main에서 Game Scene으로 전환시 호출될 메서드
    /// </summary>

    public void ChangeToGameScene(Constants.GameType gameType)
    {
        // 0 : single, 1 : dual, 2: Multi
        _gameType = gameType;
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Game에서 Main으로 전환시 호출될 메서드
    /// </summary>

    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");

    }

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        // TODO: 씬 전환시 처리할 함수
    }
}
