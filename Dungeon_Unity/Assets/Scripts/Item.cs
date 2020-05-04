using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string name = "Item";
    public int value;
}

public class Weapons : Item
{
    public Weapons(int strength)
    {
        value = strength;
    }
}

public class Armors : Item
{
    public Armors(int strength)
    {
        value = strength;
    }
}