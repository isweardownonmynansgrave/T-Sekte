using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    #region Instanzvariablen
    // Crafting
    public int MaximaleZutaten = 2;


    // Produkte
    private static string[] produktNamen = {"Alkohol1", "Name1", "Name2", "usw.."};
    #endregion

    #region MonoBehaviour
    private void Awake()
    {

    }
    private void Update()
    {

    }
    #endregion

    #region Produkte
    public static Produkt ErstelleProdukt(int _typ)
    {
        // Produkt erstellen, via Typ statt Switch
        Produkt produkt = new Produkt(produktNamen[_typ]);

        // Das Produkt zurückgeben
        return produkt;
    }

    #endregion
}