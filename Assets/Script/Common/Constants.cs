
public static class Constants
{
    public const string ServerURL = "http://localhost:3000";
    public const string SocketServerURL = "ws://localhost:3000";
    
    public enum MultiplayManagerState {  CreateRoom, JoinRoom, StartGame, ExitGame, EndGame }
    public enum GameType { SinglePlay, DualPlay, MultiPlay }
    public enum PlayerType { None, PlayerA, PlayerB }

    public const int BlockColumnCount = 3;

}
