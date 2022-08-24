namespace Sandbox
{
	public partial class BugPlayer : Entity
	{
		[Net]
		public CustomEntity Held { get; set; }

		public override void Spawn()
		{
			base.Spawn();
			EnableDrawing = true;
		}

		public BugCamera Camera
		{
			get => Components.Get<BugCamera>();
			set
			{
				if (Camera == value)
				{
					return;
				}

				Components.RemoveAny<BugCamera>();
				Components.Add(value);
			}
		}

		public void SpawnOwnedEntity()
		{
			Held = new CustomEntity()
			{
				Position = this.Position + this.Rotation.Forward * 100,
				Rotation = this.Rotation,
				Owner = this
			};
		}

		private void DumpHeldInfo()
		{
			if (IsServer)
			{
				DebugOverlay.ScreenText("~~~~ SERVER ~~~~", 0, 0.1f);
				DebugOverlay.ScreenText($"Predictable -> {Held.Predictable}", 1, 0.1f);
				DebugOverlay.ScreenText($"IsAuthority -> {Held.IsAuthority}", 2, 0.1f);
				DebugOverlay.ScreenText($"IsLocalPawn -> {Held.IsLocalPawn}", 3, 0.1f);
				DebugOverlay.ScreenText($"PhysicsBody -> {Held.PhysicsBody}", 4, 0.1f);
				DebugOverlay.ScreenText($"     Client -> {Held.Client}", 5, 0.1f);
				DebugOverlay.ScreenText($"      Owner -> {Held.Owner}", 6, 0.1f);
			}
			else
			{
				DebugOverlay.ScreenText("~~~~ CLIENT ~~~~", 8, 0.1f);
				DebugOverlay.ScreenText($"Predictable -> {Held.Predictable}", 9, 0.1f);
				DebugOverlay.ScreenText($"IsAuthority -> {Held.IsAuthority}", 10, 0.1f);
				DebugOverlay.ScreenText($"IsLocalPawn -> {Held.IsLocalPawn}", 11, 0.1f);
				DebugOverlay.ScreenText($"PhysicsBody -> {Held.PhysicsBody}", 12, 0.1f);
				DebugOverlay.ScreenText($"     Client -> {Held.Client}", 13, 0.1f);
				DebugOverlay.ScreenText($"      Owner -> {Held.Owner}", 14, 0.1f);
			}
		}
		
		public override void Simulate(Client cl)
		{
			if (Held.IsValid())
			{
				DumpHeldInfo();
			}
			
			if (Input.Pressed(InputButton.PrimaryAttack))
			{
				Held?.PhysicsClear();
			}

			if (Input.Pressed(InputButton.SecondaryAttack))
			{
				Held?.SetupPhysics();
			}
			
			Held?.Simulate(cl);
			base.Simulate(cl);
		}
	}
}
