﻿using Solun.Entities;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Solun.World
{
	class Sector
	{
		List<Room> rooms = new List<Room>();

		public List<Room> Rooms => rooms;

		public void LinkRooms(Room room1, Room room2)
		{
			if(rooms.Contains(room1) && rooms.Contains(room2))
			{
				room1.Entities.Add(new Door(room1, room2));
				room2.Entities.Add(new Door(room2, room1));
			}
		}

		public void LinkRooms(string name1, string name2) => LinkRooms(GetRoom(name1), GetRoom(name2));

		public Room GetRoom(string name) => rooms.Find(rm => rm.Name.ToLower() == name.ToLower());
	}
}
