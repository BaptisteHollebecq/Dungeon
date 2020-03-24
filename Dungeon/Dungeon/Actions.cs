using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    public abstract class Actions
    {
        public abstract void Execute();
    }

    public class Observe : Actions
    {
        public override void Execute()
        {
            // obeserve the room
        }
    }
}
