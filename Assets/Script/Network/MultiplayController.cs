using Newtonsoft.Json;
using SocketIOClient;
using System;
using UnityEngine;

// joinRoom/ createRoom 이벤트 전달할 때 전달되는 정보의 타입
public class RoomData
{
    [JsonProperty("roomId")]
    public string roomId {  get; set; }
}

// 상대방이 둔 마커 위치
public class BlockData
{
    [JsonProperty("blockIndex")]
    public int blockIndex { get; set; }
}

public class MultiplayController : IDisposable
{
    private SocketIOUnity _socket;
    //  ROom 상태 변화에 따른 동작을 할당하는 변수
    private Action<Constants.MultiplayControllerState, string> _onMultiplayStateChanged;
    // 게임 진행 상황에서 Marker의 위치를 업데이트 하는 변수
    public Action<int> onBlockDataChanged;

    // 1. Delegate
    // 함수를 변수에 저장하고 있다가 필요한 시점에 실행
    // 함수의 타입을 선언 > 해당 타입으로 변수를 선언 > 변수에 함수를 저장
    // 2. Action/Func

    public MultiplayController(Action<Constants.MultiplayControllerState, string> onMultiplayStateChanged)
    {
        // 서버에서 이벤트가 발생하면 처리할 메서드를 _onMultiplaytStateChanged에 등록
        _onMultiplayStateChanged = onMultiplayStateChanged;
        // Socket.io 클라이언트 초기화
        var uri = new Uri(Constants.SocketServerURL);
        _socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });

        _socket.OnUnityThread("createRoom", CreateRoom);
        _socket.OnUnityThread("joinRoom", Joinroom);
        _socket.OnUnityThread("startGame", StartGame);
        _socket.OnUnityThread("exitRoom", ExitRoom);
        _socket.OnUnityThread("endGame", EndGame);
        _socket.OnUnityThread("doOpponent", DoOpponent);
        _socket.Connect();


    }

    private void CreateRoom(SocketIOResponse response)
    {
        var data = response.GetValue<RoomData>();
        _onMultiplayStateChanged?.Invoke(Constants.MultiplayControllerState.CreateRoom, data.roomId);
    }

    private void Joinroom(SocketIOResponse response)
    {
        var data = response.GetValue<RoomData>();
        _onMultiplayStateChanged?.Invoke(Constants.MultiplayControllerState.JoinRoom, data.roomId);
    }

    private void StartGame(SocketIOResponse response)
    {
        var data = response.GetValue<RoomData>();
        _onMultiplayStateChanged?.Invoke(Constants.MultiplayControllerState.StartGame, data.roomId);
    }

    private void ExitRoom(SocketIOResponse response)
    {
        _onMultiplayStateChanged?.Invoke(Constants.MultiplayControllerState.ExitRoom, null);
    }

    private void EndGame(SocketIOResponse response)
    {
        _onMultiplayStateChanged?.Invoke(Constants.MultiplayControllerState.EndGame, null);
    }

    private void DoOpponent(SocketIOResponse response)
    {
        var data = response.GetValue<BlockData>();
        onBlockDataChanged?.Invoke(data.blockIndex);
    }

    #region Client => Server
    // Room을 나올 때 호출하는 매서드, Client -> Server
    public void LeaveRoom(string roomId)
    {
        _socket.Emit("leaveRoom", roomId);
    }

    // 플레이어가 Marker를 두면 호출하는 매서드, Client -> Server
    public void DoPlayer(string roomId, int blockIndex)
    {
        _socket.Emit("doPlayer", new { roomId, blockIndex });
    }

    #endregion
    public void Dispose()
    {
        if(_socket != null)
        {
            _socket.Disconnect();
            _socket.Dispose();
            _socket = null;
        }
    }

}
