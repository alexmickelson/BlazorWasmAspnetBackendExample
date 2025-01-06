using GameLogic;

public static class Lobby
{
  public static List<Game> Games { get; set; } = new();
  public static event Action GamesUpdated;
}