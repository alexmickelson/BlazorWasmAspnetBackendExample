using GameLogic;

public static class Lobby
{
  public static List<Game> Games { get; set; } = new();
  public static event Action OnLobbyUpdate;

  public static Game CreateGame(string name)
  {
    var newGame = new Game()
    {
      Name = name
    };

    Games.Add(newGame);
    OnLobbyUpdate?.Invoke();
    return newGame;
  }
}