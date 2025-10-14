using UnityEngine;
public class Produkt // Ggf. vererben zu TeeProdukt
{
    // Eigenschaften
    [HideInInspector]
    public string Name;

    // Gesammelte Infos
    private string[] namen = {"Nudeln", "Name1", "Name2", "usw.."};
    //private 
    #region Konstruktor
    public Produkt(int _typ)
    {
        Name = namen[_typ];
        // Für weitere Properties wiederholen
    }
    #endregion
}