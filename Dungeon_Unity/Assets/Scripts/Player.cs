using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    #region GameVariables

    private List<Direction> possibleMove;

    private bool _canMove = false;
    private bool _canAttack = false;
    private bool _canPickUp = false;
    private bool _canSleep = false;
    private bool _canUse = false;
    private bool _canLook = true;

    private bool _moving = false;
    private string _attackmove = "";
    private bool _attacking = false;
    private bool _escaping = false;
    private bool _did = false;

    public InputField entry;
    public InputField attack;
    public TEXTS hud;
    private string text;

    Rooms currentRoom;
    Vector3 lastpos;
    #endregion

    #region PlayerVariable

    private int _competencePoints = 1;
    private int _gold = 1;
    private int _experience = 0;
    public int Experience { get { return _experience; }
        set 
        {
            if (value >= _level * 10)
            {
                _level++;
                value -= _level * 10;
            }
            _experience = value;
        } 
    }
    private int _level = 1;


    #endregion

    private void Start()
    {
        Life = 50;
        Strength = 10;
    }

    private void Update()
    {
        if (Inventory.Count <= 0)
            _canUse = false;
        else
            _canUse = true;


        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }

        if (_moving)
        {
            if (Input.GetButtonDown("Up"))
            {
                Direction wanted = Direction.top;
                foreach (Direction obj in currentRoom.possibleMove)
                {
                    if (obj == wanted)
                    {
                        lastpos = transform.position;
                        transform.position = new Vector3(transform.position.x, transform.position.y + 1.279f, transform.position.z);
                        StartCoroutine(hud.DoSmthing(0.5f));
                        _moving = false;
                        break;
                    }
                }
            }
            if (Input.GetButtonDown("Down"))
            {
                Direction wanted = Direction.bottom;
                foreach (Direction obj in currentRoom.possibleMove)
                {
                    if (obj == wanted)
                    {
                        lastpos = transform.position;
                        transform.position = new Vector3(transform.position.x, transform.position.y - 1.279f, transform.position.z);
                        StartCoroutine(hud.DoSmthing(0.5f));
                        _moving = false;
                        break;
                    }
                }
            }
            if (Input.GetButtonDown("Left"))
            {
                Direction wanted = Direction.left;
                foreach (Direction obj in currentRoom.possibleMove)
                {
                    if (obj == wanted)
                    {
                        lastpos = transform.position;
                        transform.position = new Vector3(transform.position.x - 1.279f, transform.position.y, transform.position.z);
                        StartCoroutine(hud.DoSmthing(0.5f));
                        _moving = false;
                        break;
                    }
                }
            }
            if (Input.GetButtonDown("Right"))
            {
                Direction wanted = Direction.right;
                foreach (Direction obj in currentRoom.possibleMove)
                {
                    if (obj == wanted)
                    {
                        lastpos = transform.position;
                        transform.position = new Vector3(transform.position.x + 1.279f, transform.position.y, transform.position.z);
                        StartCoroutine(hud.DoSmthing(0.5f));
                        _moving = false;
                        break;
                    }
                }
            }
        }
    }

    public void CheckEntry()
    {
        text = entry.text;
        switch(text)
        {
            case "observe":
                {
                    Observe();
                    break;
                }
            case "move":
                {
                    Move();
                    break;
                }
            case "attack":
                {
                    Attack();
                    break;
                }
            case "pickup":
                {
                    PickUp();
                    break;
                }
            case "sleep":
                {
                    Sleep();
                    break;
                }
            case "use":
                {
                    hud.Print("Not Implemented yet");
                    StartCoroutine(hud.DoSmthing(1));
                    break;
                }
            default:
                {
                    hud.Print("Invalid Entry Please Try Again...");
                    StartCoroutine(hud.DoSmthing(1));
                    break;
                }
        }

    }

    public void CheckEntryAttack()
    {
        _attackmove = attack.text;
        switch (_attackmove)
        {
            case "attack":
                {
                    _attacking = true;
                    _did = true;
                    break;
                }
            case "spell":
                {
                    hud.Print("Sorry not implemented yet");
                    StartCoroutine(hud.AttackTexts(1));
                    break;
                }
            case "escape":
                {
                    _escaping = true;
                    _did = true;
                    break;
                }
            default:
                {
                    hud.Print("Invalid Entry Please Try Again...");
                    StartCoroutine(hud.AttackTexts(1));
                    break;
                }
        }
    }

    public void Sleep()
    {
        if (!_canSleep || currentRoom.Enemies.Count != 0)
        {
            hud.Print("You can't sleep here");
            StartCoroutine(hud.DoSmthing(2));
        }
        else
        {
            hud.Print("You regain some Hp but you broke the bed ");
            currentRoom.bed = false;
            Life += 5;
            StartCoroutine(hud.DoSmthing(2));
        }
    }

    public void PickUp()
    {
        if (!_canPickUp)
        {
            hud.Print("There is nothing you can take here");
            StartCoroutine(hud.DoSmthing(2));
        }
        else
        {
            Inventory.Add(currentRoom.Items[0]);

            hud.Print("You picked up a " + currentRoom.Items[0].name);
            currentRoom.Items.RemoveAt(0);
            StartCoroutine(hud.DoSmthing(2));
        }
    }

    IEnumerator Fight(Enemy enemy)
    {
        while (Life > 0 && enemy.Life > 0)
        {
            yield return new WaitUntil(() => _did);
            if (_attacking)
            {
                _attacking = false;
                _did = false;
                int damage = Strength;
                if (weapon != null)
                    damage += weapon.value;
                if (enemy.armor != null)
                    damage -= enemy.armor.value;
                enemy.Life -= damage;
                if (enemy.Life > 0)
                {
                    int enemydamage = enemy.Strength;
                    if (enemy.weapon != null)
                        enemydamage += enemy.weapon.value;
                    if (armor != null)
                        enemydamage -= armor.value;
                    Life -= enemydamage;
                    hud.Print("You inflicted " + damage.ToString() + " damage to the enemy and he did " + enemydamage.ToString() + " to you");
                    if (Life > 0 && enemy.Life > 0)
                        StartCoroutine(hud.AttackTexts(2));
                }
                else
                    hud.Print("You inflicted " + damage.ToString() + " damage to the enemy and he died");
            }
            else if (_escaping)
            {
                _escaping = false;
                _did = false;
                if (Random.Range(0, 11) % 2 == 0)
                {
                    transform.position = lastpos;
                    hud.Print("You Escaped");
                    StartCoroutine(hud.DoSmthing(2));
                }
                else
                {
                    int damage = enemy.Strength;
                    if (enemy.weapon != null)
                        damage += enemy.weapon.value;
                    if (armor != null)
                        damage -= armor.value;
                    Life -= damage;
                    hud.Print("Escape Failed\nThe enemy attacked and inflicted you " + damage.ToString() + " damage");
                    if (Life <= 0 )
                        StartCoroutine(hud.Loose(0));
                    else
                        StartCoroutine(hud.AttackTexts(2));
                }
            }
        }
        if (Life <= 0)
        {
            StartCoroutine(hud.Loose(0));
            yield return null;
        }
        else
        {
            Debug.Log("sorry");
            _gold += enemy.Gold;
            Experience += enemy.Experience;
            StartCoroutine(hud.DelayPrint("You win the fight, " + enemy.Gold+ " gold et " + enemy.Experience+ " points d'experiences\nYou are now Level" + _level, 1));
            currentRoom.Enemies.RemoveAt(0);
            StartCoroutine(hud.DoSmthing(5));
            yield return null;
        }
        yield return null;
    }

    private void Attack()
    {
        if (!_canAttack)
        {
            hud.Print("There is nothing to attack here");
            StartCoroutine(hud.DoSmthing(2));
        }
        else
        {
            StartCoroutine(Fight(currentRoom.Enemies[0]));
            StartCoroutine(hud.AttackTexts(1));
        }
    }

    private void Move()
    {
        if (!_canMove)
        {
            hud.Print("You can't move for now");
            StartCoroutine(hud.DoSmthing(2));
        }
        else
        {
            _moving = true;
            string list = "to the ";
            int i = 0;
            foreach (Direction obj in currentRoom.possibleMove)
            {
                list += obj.ToString();
                if (i != currentRoom.possibleMove.Count-1)
                {
                    list += ", to the ";
                }
                i++;
            }
            hud.Print("You can move " + list + "\nPress the corresponding Arrow Key");
        }
    }

    void Observe()
    {
        if (currentRoom.Items.Count <= 0)
        {
            hud.Print("There's nothing to pick up in this room");
            StartCoroutine(hud.DoSmthing(2));
        }
        else
        {
            string list = "";
            foreach(Item obj in currentRoom.Items)
            {
                list += obj.name + ", ";
            }
            hud.Print("in this room you can find\n"+ list);
            StartCoroutine(hud.DoSmthing(2));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Rooms")
        {
            currentRoom = collision.GetComponent<Rooms>();

            possibleMove = currentRoom.possibleMove;
            if (currentRoom.Enemies.Count == 0)
            {
                _canMove = true;
                _canAttack = false;
            }
            else
            {
                _canMove = false;
                _canAttack = true;
            }

            if (currentRoom.Items.Count != 0)
            {
                _canPickUp = true;
            }

            if (currentRoom.bed == true)
            {
                if (currentRoom.Enemies.Count == 0)
                    _canSleep = true;
            }
            else
            {
                _canSleep = false;
            }
            if (currentRoom.exit == true)
            {
                StartCoroutine(hud.Win(0));
            }
            /*Debug.Log("currentRoom ennemies ->"+ currentRoom.Enemies.Count);
            Debug.Log("currentRoom Items ->" + currentRoom.Items.Count);*/
        }
    }
}
