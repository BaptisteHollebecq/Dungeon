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
        public int Index;

        public Items Loot;
        public Enemy Fight;
        public bool Exit;
        public bool Bed;
        public List<Room> SideRooms = new List<Room>();
        
    }

    class Dungeon : Room
    {
        public List<Room> Rooms = new List<Room>();
        public int PlayerIndex;

        public Dungeon()
        {

        }

    }
}
