namespace GameLogic.Game;

public class Game
{
  public GameStatus Status => GameStatus.Playing;
  public event Action OnUpdate;
  public string Name { get; init; }
  public IEnumerable<Tank> Tanks { get; internal set; } = [];
  public CancellationTokenSource CancellationTokenSource { get; set; } = new CancellationTokenSource();
  public GameLoopRunner loopRunner { get; set; }
  public Game()
  {
    loopRunner = new(this);
  }

  public async Task BroadcastUpdate()
  {
    OnUpdate?.Invoke();
  }

  public Guid JoinGame()
  {
    var newTank = new Tank();
    Tanks = Tanks.Append(newTank);
    return newTank.Id;
  }

  public void ReceiveUserInput(PlayerInputRequest request)
  {
    Tanks = Tanks.Select(t =>
      {
        return t.Id == request.PlayerId
          ? t with
          {
            MovingForward = request.Forward,
            MovingLeft = request.Left,
            MovingRight = request.Right
          }
          : t;
      })
      .ToArray();
  }

}

public enum GameStatus
{
  NotStarted,
  Playing,
  Ended,
}