using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    abstract class Items
    {
        protected int _value;
        public int Value { get { return _value; } set { _value = value; } }

        public abstract void Use();
    }
}
