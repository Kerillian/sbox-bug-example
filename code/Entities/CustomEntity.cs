namespace Sandbox
{
	public class CustomEntity : ModelEntity
	{
		private static readonly Model model = Model.Load("models/citizen_props/crate01.vmdl");

		public override void Spawn()
		{
			base.Spawn();
			
			Model = model;
			Predictable = true;
			RenderColor = Color.Green;
		}

		public void SetupPhysics()
		{
			SetupPhysicsFromOBB(PhysicsMotionType.Dynamic, model.Bounds.Mins, model.Bounds.Maxs);
			PhysicsBody.Enabled = false;
			RenderColor = Color.Red;
		}

		public override void PhysicsClear()
		{
			base.PhysicsClear();
			RenderColor = Color.Green;
		}

		public override void Simulate(Client cl)
		{
			Vector3 movement = new Vector3(Input.Forward, Input.Left, 0).Normal;
			Position += movement;
			base.Simulate(cl);
		}
	}
}