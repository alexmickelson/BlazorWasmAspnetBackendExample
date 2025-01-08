using GameLogic.Game;

namespace GameLogic;

public record GameState
{
  public GameStatus Status { get; init; }
  public string Name { get; init; }
  public IEnumerable<TankState> Tanks { get; init; }
}

public record TankState
{
  public Guid Id { get; init; }
  public (int X, int Y) Position { get; init; }
  public int Angle { get; init; }
}