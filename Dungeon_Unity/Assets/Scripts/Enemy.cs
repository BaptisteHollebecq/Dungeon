using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private int _experience;
    public int Experience { get { return _experience; } }

    private int _gold;
    public int Gold { get { return _gold; } }


    public Enemy(int life, int strength)
    {
        Life = life;
        Strength = strength;
        _experience = Random.Range(5, 15);
        _gold = Random.Range(5, 15);

        if (Random.Range(0, 100) % 2 == 0)
        {
            weapon = new Weapons(Random.Range(1, 11));
        }
        if (Random.Range(0, 100) % 2 == 1)
        {
            armor = new Armors(Random.Range(1, 11));
        }

    }
}
