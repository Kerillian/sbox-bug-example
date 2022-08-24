namespace Sandbox
{
	public class BugCamera : CameraMode
	{
		public override void Activated()
		{
			if (Local.Pawn is BugPlayer player)
			{
				Position = player.Position;
				Rotation = player.Rotation;
			}
			
			base.Activated();
		}

		public override void Update()
		{
			FieldOfView = 90;
		}
	}
}