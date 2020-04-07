using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    enum Direction { N, S, E, W };

    class Room
    {
        public int[,] Index;

        public Items Loot;
        public Enemy Fight;
        public bool Exit;
        public bool Bed;
        public List<Room> SideRooms;
        public List<Direction> SideDir;

        public Room(int[,] index, Items loot, Enemy fight, bool exit, bool bed)
        {
            Index = index;
            Loot = loot;
            Fight = fight;
            Exit = exit;
            Bed = bed;
            SideRooms = new List<Room>();
            SideDir = new List<Direction>();
        }
        
    }

    class Dungeon : Room
    {
        public List<Room> Rooms = new List<Room>();
        public int[,] PlayerIndex;


        public List<Room> Generate()
        {
            int roomNbr = 0;
            int[,] creatingIndex = new int[0,0];
            int[,] nextIndex;
            Room creatingRoom;
            Room nextRoom;
            Direction dir;
            PlayerIndex = new int[0, 0];

            //  First ROOM
            creatingRoom = new Room(creatingIndex, null, null, false, false);

            dir = RandomDir();                              //
            creatingRoom.SideDir.Add(dir);                  //  faire une methode de ca
            nextIndex = GetIndex(creatingIndex, dir);       //

            Rooms.Add(creatingRoom);
            while (true)
            {
                // calculer les item/enemy/sortie/bed



                nextRoom = new Room(nextIndex, null, null, false, false);
                creatingRoom.SideRooms.Add(nextRoom);

                creatingRoom = nextRoom;
                creatingIndex = nextIndex;

                if (creatingRoom.Exit == false)
                {
                    dir = RandomDir();
                    creatingRoom.SideDir.Add(dir);
                    nextIndex = GetIndex(creatingIndex, dir);
                }

                Rooms.Add(creatingRoom);
                if (creatingRoom.Exit == true)
                    break;

            }

            return Rooms;
        }

        private void Setup()
        {

        }

        private int[,] GetIndex(int[,] index, Direction dir)
        {
            switch (dir)
            {
                case Direction.N: index[0, 1]++; break;
                case Direction.S: index[0, 1]--; break;
                case Direction.E: index[1, 0]++; break;
                case Direction.W: index[1, 0]--; break;
            }
            return index;
        }

        private Direction RandomDir()
        {
            var rand = new Random();
            int tmp = rand.Next(4);
            switch (tmp)
            {
                case 0: return Direction.N; break;
                case 1: return Direction.S; break;
                case 2: return Direction.E; break;
                case 3: return Direction.W; break;
            }
        }
    }
}
