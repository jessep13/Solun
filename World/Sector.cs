using Solun.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Solun.World
{
	class Sector
	{
		List<Room> rooms = new List<Room>();

		public List<Room> Rooms => rooms;

		public void AddRoom(NameHolder names, string description) => rooms.Add(new Room(
			names,
			description,
			this));

		public void LinkRooms(Room room1, Room room2, Entity unlockEntity = null, bool isLocked = false)
		{
			if(rooms.Contains(room1) && rooms.Contains(room2))
			{
				Lock doorLock = null;
				
				if(unlockEntity != null)
				{
					doorLock = new Lock(
						new NameHolder($"{room1.Name}|{room2.Name} lock"),
						$"A lock for the door connecting {room1.Name} and {room2.Name}",
						unlockEntity,
						isLocked);
				}

				room1.Entities.Add(new Door(room1, room2, doorLock));
				room2.Entities.Add(new Door(room2, room1, doorLock));
			}
		}

		public void LinkRooms(string name1, string name2, Entity unlockEntity = null, bool isLocked = false) 
			=> LinkRooms(FindRoom(name1), FindRoom(name2), unlockEntity, isLocked);

		public Room FindRoom(string name) => Program.CheckMulti<Room>(rooms.FindAll(room => room.IsNamed(name)));
	}
}
