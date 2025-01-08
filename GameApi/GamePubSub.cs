using Microsoft.AspNetCore.SignalR;

public class GamePubSub
{
  private readonly IHubContext<LobbyHub> context;

  public GamePubSub(IHubContext<LobbyHub> context)
  {
    this.context = context;
  }
}