using UnityEngine;


public class GameLogic
{
    public BlockController blockController; // Block을 처리할 객체

    private Constants.PlayerType[,] _board; // 보드의 상태 정보

    public BasePlayerState firstPlayerState; // Player A
    public BasePlayerState secondPlayerState; // Player B3
    
    public enum GameResult { None, Win, Lose, Draw}

    private BasePlayerState _currentPlayerState; // 현재턴 Player

    public GameLogic(BlockController blockController ,Constants.GameType gameType)
    {
        this.blockController = blockController;

        // 보드의 상태 정보 초기화
        _board = new Constants.PlayerType[Constants.BlockColumnCount, Constants.BlockColumnCount];

        // GameType 초기화
        switch(gameType)
        {
            case Constants.GameType.SinglePlay:
                break;
            case Constants.GameType.DualPlay:
                firstPlayerState = new PlayerState(true);
                secondPlayerState = new PlayerState(false);
                SetState(firstPlayerState);
                break;
            case Constants.GameType.MultiPlay:
                break;
        }
    }

    // 턴이 바뀔 때, 기존에 진행하던 상태를 나오고, 다음 턴의 상태를 _currentPlayerState에 할당 후 OnEnter호출
    public void SetState(BasePlayerState state)
    {
        _currentPlayerState?.OnExit(this);
        _currentPlayerState = state;
        _currentPlayerState?.OnEnter(this);
    }

    // _board 배열에 새로운 Marker 값을 할당
    public bool SetNewBoardValue(Constants.PlayerType playerType, int row, int col)
    {
        if (_board[row, col] != Constants.PlayerType.None)
            return false;

        if(playerType == Constants.PlayerType.PlayerA)
        {
            _board[row, col] = playerType;
            blockController.PlaceMaker(Block.MarkerType.O, row, col);
            return true;
        }
        else if (playerType == Constants.PlayerType.PlayerB)
        {
            _board[row, col] = playerType;
            blockController.PlaceMaker(Block.MarkerType.X, row, col);
            return true;
        }
        return false;
    }

    // GameOver 처리\
    public void EndGame(GameResult gameResult)
    {
        // TODO: GameLogic 정리
        SetState(null);
        firstPlayerState = null;
        secondPlayerState = null;

        // TODO: 유저에게 GameOver 표시
        Debug.Log("### Game Over ###");
    }

    public GameResult CheckGameResult()
    {
        if (CheckGameWin(Constants.PlayerType.PlayerA, _board))
        {
            return GameResult.Win;
        }
        if (CheckGameWin(Constants.PlayerType.PlayerB, _board))
        {
            return GameResult.Lose;
        }
        // 비겼는지 확인

        if (CheckGameDraw(_board))
            return GameResult.Draw;
        

        return GameResult.None;
    }

    // 비겼는지 확인

    public bool CheckGameDraw(Constants.PlayerType[,] board)
    {
        for (var row = 0; row < board.GetLength(0); row++)
        {
            for (var col = 0; col < board.GetLength(1); col++)
            {
                if (board[row, col] == Constants.PlayerType.None)
                    return false;
            }
        }
        return true;
    }

    // 게임 승리 확인
    private bool CheckGameWin(Constants.PlayerType playerType, Constants.PlayerType[,] board)
    {
        // Col체크 후 일자면 True
        for (var row = 0; row < board.GetLength(0); row++)
        {
            if (board[row, 0 ]== playerType && board[row, 1] == playerType &&board[row, 2] == playerType)
                return true;
            
        }
        
        // Row체크 후 일자면 True
        for ( var col = 0; col < board.GetLength(1); col++)
        {
            if (board[0, col] == playerType && board[1, col] == playerType && board[2, col] == playerType)
                return true;
        }
        // 대각선체크 후 일자면 True
        if (board[0, 0] == playerType && board[1, 1] == playerType && board[2, 2] == playerType)
            return true;

        if (board[0, 2] == playerType && board[1, 1] == playerType && board[2, 0] == playerType)
            return true;

        return false;
    }
}
