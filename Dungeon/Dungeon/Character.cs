using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    class Character
    {
        protected int _life;
        public int Life { get { return _life; } set { _life = value; } }

        protected int _strength;
        public int Strength { get { return _strength; } set { _strength = value; } }

        protected List<Items> _inventory;
        public List<Items> Inventory { get { return _inventory; } }

        public void AddItem(Items item)
        {
            _inventory.Add(item);
        }

        public void RemoveItem(Items item)
        {
            _inventory.Remove(item);
        }

        public void Attack(Character opponent)
        {
            opponent.Life -= _strength;
        }


        public Character(int life, int strength)
        {
            _life = life;
            _strength = strength;
            _inventory = new List<Items>();
        }
    }

    class Player : Character
    {
        protected int _level;
        public int Level { get { return _level; } }

        protected int _experience;
        public int Experience 
        { 
            get { return _experience; }
            set 
            { 
                _experience = value;
                if (_experience > _level * 10)
                {
                    _experience -= _level * 10;
                    _level++;
                }
            }
        }

        protected int _competencePoints;
        public int CompetencePoints { get { return _competencePoints; } set { _competencePoints = value; } }



        public Player(int life, int strength) : base(life, strength)
        {
                  
        }

    }

    class Enemy : Character
    {
        protected int _experience;
        public int Experience { get { return _experience; } set { _experience = value; } }

        public Enemy(int life, int strength, int experience) : base(life, strength)
        {
            _experience = experience;
        }
    }
}
