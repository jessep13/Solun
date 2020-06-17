using Solun.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solun.Entities.Mobs
{
	class Player : Mob
	{
		public Player(Room currentRoom)
		{
			name = "me";
			description = "You look like... you.";

			maxHealth = 100;
			health = maxHealth;

			maxWeight = 100;

			this.currentRoom = currentRoom;
			currentRoom.Entities.Add(this);
		}

		public override void Interact(Entity entity)
		{
			throw new NotImplementedException();
		}
	}
}
