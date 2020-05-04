using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEXTS : MonoBehaviour
{
    public string text = "";
    public Text HudText;
    public InputField inputFieldbase;
    public InputField inputFieldAttack;

    private void Awake()
    {
        Manager.ExitSelected += Intro;
    }
    private void OnDestroy()
    {
        Manager.ExitSelected -= Intro;
    }
    
    public void Print(string txt)
    {
        HudText.text = txt;
    }

    public IEnumerator DelayPrint(string s, float n)
    {
        yield return new WaitForSeconds(n);
        Print(s);
    }

    void Intro()
    {
        Print("Welcome in the Dungeon");
        StartCoroutine(DoSmthing(1));
    }

    public IEnumerator DoSmthing(float n)
    {
        yield return new WaitForSeconds(n);
        Print("What do you want to do ?\n(observe, move, attack, pickup, sleep, use)");
        inputFieldbase.gameObject.SetActive(true);
        inputFieldAttack.gameObject.SetActive(false);
    }

    public IEnumerator AttackTexts(float n)
    {
        yield return new WaitForSeconds(n);
        Print("What do you want to do ?\n(attack, spell, escape)");
        inputFieldbase.gameObject.SetActive(false);
        inputFieldAttack.gameObject.SetActive(true);
    }

    public IEnumerator Win(float n)
    {
        yield return new WaitForSeconds(n);
        Print("You Win this game");
        inputFieldbase.gameObject.SetActive(false);
        inputFieldAttack.gameObject.SetActive(false);
    }

    public IEnumerator Loose(float n)
    {
        yield return new WaitForSeconds(n);
        Print("You Loose this game");
        inputFieldbase.gameObject.SetActive(false);
        inputFieldAttack.gameObject.SetActive(false);
    }
}
