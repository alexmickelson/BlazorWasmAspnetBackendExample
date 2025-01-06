namespace GameLogic;

public class Game
{
  public GameStatus Status => GameStatus.Playing;
  public static event Action GameUpdated;
}

public enum GameStatus
{
  NotStarted,
  Playing,
  Ended,
}