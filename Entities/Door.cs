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

		static Converter<Entity, Door> entityToDoor = new Converter<Entity, Door>(entity => (Door)entity);
		public static Converter<Entity, Door> EntityToDoor => entityToDoor;

		public Room StartRoom => startRoom;
		public Room EndRoom => endRoom;

		public Door(Room startRoom, Room endRoom)
		{
			name = $"{endRoom} Door";
			description = $"A door leading to {endRoom}.";

			this.startRoom = startRoom;
			this.endRoom = endRoom;
		}

		public override void Interact(Entity entity)
		{
			if(entity is Mob)
			{
				Mob mob = (Mob)entity;
				if(mob.CurrentRoom == startRoom) mob.MoveToRoom(endRoom, this);
			}
		}
	}
}
