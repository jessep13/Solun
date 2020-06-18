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

		public void AddRoom(string name, string description) => rooms.Add(new Room(
			name,
			description,
			this));

		public void AddRoom(List<string> names, string description) => rooms.Add(new Room(
			names,
			description,
			this));

		public void AddRoom(string[] names, string description) => rooms.Add(new Room(
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
						$"{room1}|{room2} lock",
						$"A lock for the door connecting {room1} and {room2}",
						unlockEntity,
						isLocked);
				}

				room1.Entities.Add(new Door(room1, room2, doorLock));
				room2.Entities.Add(new Door(room2, room1, doorLock));
			}
		}

		public void LinkRooms(string name1, string name2, Entity unlockEntity = null, bool isLocked = false) 
			=> LinkRooms(FindRoom(name1), FindRoom(name2), unlockEntity, isLocked);

		public Room FindRoom(string name) //=> rooms.Find(rm => rm.IsNamed(name));
		{
			List<Room> validRooms = rooms.FindAll(room => room.IsNamed(name));
			switch(validRooms.Count)
			{
				case 0:
					return null;
				case 1:
					return validRooms.First();
				default:
					throw new Exception("Multiple Objects Found");
			}
		} 
	}
}
