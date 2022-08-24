namespace Sandbox
{
	public partial class MyGame : Game
	{
		public override void ClientJoined(Client client)
		{
			base.ClientJoined(client);
			
			BugPlayer player = new BugPlayer()
			{
				Position = Vector3.Zero.WithZ(50)
			};

			client.Pawn = player;
			
			player.SpawnOwnedEntity();
		}
	}
}
