using UnityEngine;
public class CraftingTee
{
    [HideInInspector]
    public bool HatJuice = false;
    [HideInInspector]
    public bool HatBubbles = false;

    Sprite spriteJuice;
    Sprite spriteBubbles;

    public bool SetJuice(string _name)
    {
        if (!HatJuice)
        {
            spriteJuice = GameManager.juiceSprites[_name]; // WIP
            return true;
        }
        else
        {
            Debug.Log("Hatte schon Saft, kann keinen weiteren einfüllen!");
            return false; 
        }
    }
    public bool SetBubbles()
    {
        if (HatJuice)
        {
            // Bubbles als Layer hinzufügen - WIP (Jason)

            // Am Ende Abschließen
            HatBubbles = true;
            return true;
        }
        else
        {
            Debug.Log("Hat keinen Saft. Kann keine Bubbles hinzufügen!");
            return false;
        }
    }
    public bool IstFertig()
    {
        return HatJuice && HatBubbles;
    }
}