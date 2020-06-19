using Solun.Entities.Items;
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
			allNames = new NameHolder("me");
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

		public Entity FindEntity(string name)
		{
			// Check inventory
			if(inventory.Count != 0)
			{
				Item item = inventory.Find(item => item.IsNamed(name));
				if(inventory.Contains(item)) return item;
			}

			// Check in room
			if(currentRoom.Entities.Count != 0)
			{
				Entity entity = currentRoom.FindEntity(name);
				if(currentRoom.Entities.Contains(entity)) return entity;
			}

			return null;
		}
	}
}
