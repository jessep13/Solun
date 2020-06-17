using Solun.Entities.Mobs;
using Solun.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solun.Entities
{
	class Door : Entity
	{
		Room startRoom;
		Room endRoom;

		Lock doorLock;

		static Converter<Entity, Door> entityToDoor = new Converter<Entity, Door>(entity => (Door)entity);
		public static Converter<Entity, Door> EntityToDoor => entityToDoor;

		public Room StartRoom => startRoom;
		public Room EndRoom => endRoom;

		public Lock DoorLock => doorLock;

		public Door(Room startRoom, Room endRoom, Lock doorLock = null)
		{
			name = $"{endRoom.Name} Door";
			description = $"A door leading to {endRoom.Name}.";

			this.startRoom = startRoom;
			this.endRoom = endRoom;

			this.doorLock = doorLock;
		}

		public override void Interact(Entity entity)
		{
			// Is a mob using the door?
			if(entity is Mob)
			{
				// Is said mob in the start room? Also, Is there not a lock, or if there is one, is it unlocked? 
				Mob mob = (Mob)entity;
				if(mob.CurrentRoom == startRoom && (doorLock == null || !doorLock.IsLocked))
				{
					mob.MoveToRoom(this); 
				}
			}
		}
	}
}
