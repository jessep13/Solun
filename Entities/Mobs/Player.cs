using System;
using System.Collections.Generic;
using System.Text;

namespace Solun.Entities.Mobs
{
	class Player : Mob
	{
		public Player()
		{
			name = "me";
			description = "You look like... you.";

			maxHealth = 100;
			health = maxHealth;
		}

		public override void Interact(Entity entity)
		{
			throw new NotImplementedException();
		}
	}
}
