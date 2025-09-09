using Newtonsoft.Json;
using SocketIOClient;
using System;
using UnityEngine;

// joinRoom/ createRoom �̺�Ʈ ������ �� ���޵Ǵ� ������ Ÿ��
public class RoomData
{
    [JsonProperty("roomId")]
    public string roomId {  get; set; }
}

// ������ �� ��Ŀ ��ġ
public class BlockData
{
    [JsonProperty("blockIndex")]
    public int blockIndex { get; set; }
}

public class MultiplayController : IDisposable
{
    private SocketIOUnity _socket;
    //  ROom ���� ��ȭ�� ���� ������ �Ҵ��ϴ� ����
    private Action<Constants.MultiplayControllerState, string> _onMultiplayStateChanged;
    // ���� ���� ��Ȳ���� Marker�� ��ġ�� ������Ʈ �ϴ� ����
    public Action<int> onBlockDataChanged;

    // 1. Delegate
    // �Լ��� ������ �����ϰ� �ִٰ� �ʿ��� ������ ����
    // �Լ��� Ÿ���� ���� > �ش� Ÿ������ ������ ���� > ������ �Լ��� ����
    // 2. Action/Func

    public MultiplayController(Action<Constants.MultiplayControllerState, string> onMultiplayStateChanged)
    {
        // �������� �̺�Ʈ�� �߻��ϸ� ó���� �޼��带 _onMultiplaytStateChanged�� ���
        _onMultiplayStateChanged = onMultiplayStateChanged;
        // Socket.io Ŭ���̾�Ʈ �ʱ�ȭ
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
    // Room�� ���� �� ȣ���ϴ� �ż���, Client -> Server
    public void LeaveRoom(string roomId)
    {
        _socket.Emit("leaveRoom", roomId);
    }

    // �÷��̾ Marker�� �θ� ȣ���ϴ� �ż���, Client -> Server
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
