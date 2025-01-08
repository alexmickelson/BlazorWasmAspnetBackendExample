using System.Collections.Concurrent;
using System.Security.Cryptography;
using GameLogic;
using Microsoft.AspNetCore.SignalR;


public class LobbyHub : Hub
{
  public async Task SendMessage(string user, string message)
  {
    await Clients.All.SendAsync("ReceiveMessage", user, message);
  }

  public async Task CreateGame(string name)
  {

    var game = Lobby.CreateGame(name);
    Console.WriteLine($"created game: {name}");
    
    var playerId = game.JoinGame();
    await Clients.Client(Context.ConnectionId).SendAsync(Messages.CreatedGame, game.Name, playerId);
  }
  public async Task JoinGame(string gameName)
  {
    var game = Lobby.Games.First(g => g.Name == gameName);
    var playerId = game.JoinGame();
    await Clients.Client(Context.ConnectionId).SendAsync(Messages.JoinedGame, game.Name, playerId);
  }

  public async Task GetGames()
  {
    var games = Lobby.Games.Select(g => g.GetGameState()).ToArray();
    Console.WriteLine("got request for games");

    await Clients.Client(Context.ConnectionId).SendAsync(Messages.GameList, games);
  }


  // track all clients...
  private readonly ConcurrentBag<string> ConnectedClients = new();

  public override async Task OnConnectedAsync()
  {
    string connectionId = Context.ConnectionId;
    Console.WriteLine($"received connection ${connectionId}");

    ConnectedClients.Add(connectionId);
    await base.OnConnectedAsync();
  }

  public override async Task OnDisconnectedAsync(Exception? exception)
  {
    string? connectionId = Context.ConnectionId;
    Console.WriteLine($"removing connection ${connectionId}");

    if (ConnectedClients.TryTake(out connectionId))
    {
      Console.WriteLine($"Removed connection: {connectionId}");
    }

    await base.OnDisconnectedAsync(exception);
  }

  public Task<string[]> GetConnectedClients()
  {
    return Task.FromResult(ConnectedClients.ToArray());
  }

}