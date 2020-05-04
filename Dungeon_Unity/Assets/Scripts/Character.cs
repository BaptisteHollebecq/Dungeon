using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int _life;
    public int Life { get { return _life; } set { _life = value <= 0 ? 0 : value; } }

    private int _strength;
    public int Strength { get { return _strength; } set { _strength = value <= 1 ? 1 : value; } }

    public List<Item> Inventory = new List<Item>();

    public Weapons weapon = new Weapons(0);
    public Armors armor = new Armors(0);


}
