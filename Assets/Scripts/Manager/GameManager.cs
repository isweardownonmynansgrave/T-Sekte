using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    #region Instanzvariablen
    // Verwaltung
    public bool DebugMode = true;

    // Crafting
    public int MaximaleZutaten = 2;
    public static int Index_Produkt_ProdsAlkoholfrei = 6;
    public static int Index_Produkt_Nudeln = 24;
    public static int Index_Zutaten_Tee = 0;
    public static int Index_Zutaten_Bubbles = 3;
    public static int Index_Zutaten_AlkoholBubbles = 9;
    public static int Index_Zutaten_Nudeln = 11;

    #region Vordefinierte-Resourcen
    // Zutaten
    private static string[] zutat_teeNamen =
    {
        "Pfirsich Tee", // zutatenListe index 0
        "Litschi Tee",
        "Mango Tee"
    };
    private static string[] zutat_bubbleNamen =
    {
        "Blaubeer Bubbles", // zutatenListe index 3
        "Gruener Apfel Bubbles",
        "Erdbeer Bubbles",
        "Himbeer Bubbles",
        "Pfirsich Bubbles",
        "Schwarztee Bubbles",
        
    };
    private static string[] zutat_alkoholBubbleNamen =
    {
        "Tequila Bubbles", // zutatenListe index 9 -> Index_Zutaten_AlkoholBubbles
        "Vodka Bubbles" // zutatenListe index 10
    };
    private static string[] zutat_nudeln =
    {
        "Rohe Nudeln" // zutatenListe index 11
    };
    // Produkte
    private static string[] produkt_produktNamen =
    {
        // Alkoholhaltige Sorten
        "Pfirsich Tee mit Tequila Bubbles",
        "Pfirsich Tee mit Vodka Bubbles",
        "Litschi Tee mit Tequila Bubbles",
        "Litschi Tee mit Vodka Bubbles",
        "Mango Tee mit Tequila Bubbles",
        "Mango Tee mit Vodka Bubbles", // 6, index 5

        // Alkoholfreie Sorten
        "Pfirsich Tee mit Blaubeer Bubbles", // index 6 -> Index_Produkt_ProdsAlkoholfrei
        "Pfirsich Tee mit Grüner Apfel Bubbles",
        "Pfirsich Tee mit Erdbeer Bubbles",
        "Pfirsich Tee mit Himbeer Bubbles",
        "Pfirsich Tee mit Pfirsich Bubbles",
        "Pfirsich Tee mit Schwarztee Bubbles", // 12

        "Litschi Tee mit Blaubeer Bubbles",
        "Litschi Tee mit Grüner Apfel Bubbles",
        "Litschi Tee mit Erdbeer Bubbles",
        "Litschi Tee mit Himbeer Bubbles",
        "Litschi Tee mit Pfirsich Bubbles",
        "Litschi Tee mit Schwarztee Bubbles",

        "Mango Tee mit Blaubeer Bubbles",
        "Mango Tee mit Grüner Apfel Bubbles",
        "Mango Tee mit Erdbeer Bubbles",
        "Mango Tee mit Himbeer Bubbles",
        "Mango Tee mit Pfirsich Bubbles",
        "Mango Tee mit Schwarztee Bubbles",

        // Nudel-Stuff
        "Gebratene Nudeln" // 25, index 24 -> Index_Produkt_Nudeln
    };
    // Durch Init befüllte Arrays/Listen
    private static Zutat[] zutatenListe;
    private static List<Rezept> rezeptListe;
    public static Dictionary<string, Sprite> juiceSprites;
    public static Dictionary<string, Sprite> bubbleSprites;
    public static Dictionary<string, Sprite> nudelSprites;
    #endregion
    #endregion

    #region Init
    private void InitZutatenArray()
    {
        int l = zutat_teeNamen.Length + zutat_bubbleNamen.Length + zutat_alkoholBubbleNamen.Length + zutat_nudeln.Length;
        zutatenListe = new Zutat[l];
        int count = 0;

        // Liste befüllen
        foreach (string s in zutat_teeNamen)
        {
            zutatenListe[count] = new Zutat(s);
            count++;
        }
        ;
        foreach (string s in zutat_bubbleNamen)
        {
            zutatenListe[count] = new Zutat(s);
            count++;
        }
        ;
        foreach (string s in zutat_alkoholBubbleNamen)
        {
            zutatenListe[count] = new Zutat(s);
            count++;
        }
        ;
        foreach (string s in zutat_nudeln)
        {
            zutatenListe[count] = new Zutat(s);
            count++;
        }
        ;
    }
    private void InitRezeptArray()
    {
        rezeptListe = new List<Rezept>();
        int count = 0;

        // Alkoholische Getränke
        for (int i = Index_Zutaten_Tee; i < Index_Zutaten_Bubbles; i++)
        {
            for (int j = Index_Zutaten_AlkoholBubbles; j < Index_Zutaten_Nudeln; j++)
            {
                List<Zutat> temp = new List<Zutat> { zutatenListe[i], zutatenListe[j] };
                rezeptListe.Add(new Rezept(temp,
                                            new Produkt(produkt_produktNamen[count]))
                );
                count++;
            }
        }

        // BubbleTea
        for (int i = Index_Zutaten_Tee; i < Index_Zutaten_Bubbles; i++)
        {
            for (int j = Index_Zutaten_Bubbles; j < Index_Zutaten_AlkoholBubbles; j++)
            {
                List<Zutat> temp = new List<Zutat> { zutatenListe[i], zutatenListe[j] };
                rezeptListe.Add(new Rezept(temp,
                                            new Produkt(produkt_produktNamen[count]))
                );
                count++;
            }
        }

        // Nudeln - To be discussed
    }
    private void InitSpriteDicts()
    {
        string teePfad = "TeeSprites";
        string bubblesPfad = "BubbleSprites";
        string nudelPfad = "NudelSprites";
        // Tee/Saft Sprites
         // Alle Sprites im angegebenen Ordner laden
        Sprite[] sprites = Resources.LoadAll<Sprite>(teePfad);

        // Nullcheck & Sortierung
        if (sprites == null || sprites.Length == 0)
        {
            Debug.LogWarning($"[GM/SpriteLoader] Keine Sprites gefunden unter 'Resources/{teePfad}'!");
        }
        if (sprites.Length != zutat_teeNamen.Length)
        {
            Debug.Log("[GM/SpriteLoader] Arrays für Tee-Sprites und Tee-Namen haben nicht dieselbe Länge!");
        }
        if (DebugMode) Debug.Log($"[GM/SpriteLoader] {sprites.Length} Sprites geladen aus 'Resources/{teePfad}'.");
        
        // Juice-Dict aus Array befüllen
        for (int i = 0; i < sprites.Length; i++)
        {
            juiceSprites.Add(zutat_teeNamen[i], sprites[i]);
        }


        // Bubble Sprites
        sprites = Resources.LoadAll<Sprite>(bubblesPfad);

        // Nullcheck & Sortierung
        if (sprites == null || sprites.Length == 0)
        {
            Debug.LogWarning($"[GM/SpriteLoader] Keine Sprites gefunden unter 'Resources/{bubblesPfad}'!");
        }
        if (sprites.Length != bubblesPfad.Length)
        {
            Debug.LogWarning("[GM/SpriteLoader] Arrays für Tee-Sprites und Tee-Namen haben nicht dieselbe Länge!");
        }
        if (DebugMode) Debug.Log($"[GM/SpriteLoader] {sprites.Length} Sprites geladen aus 'Resources/{bubblesPfad}'.");

        // Bubble-Dict aus Array befüllen
        for (int i = 0; i < sprites.Length; i++)
        {
            bubbleSprites.Add(zutat_bubbleNamen[i], sprites[i]);
        }

        // Nudel Sprites
        sprites = Resources.LoadAll<Sprite>(nudelPfad);
        
        bubbleSprites.Add(zutat_nudeln[0], sprites[0]);
        bubbleSprites.Add(produkt_produktNamen[24], sprites[1]);

    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        // Crafting
        InitZutatenArray();
        InitRezeptArray(); // Basierend auf ZutatenArray
        InitSpriteDicts(); // Via Ressources.LoadAll
    }
    private void Update()
    {

    }
    #endregion

    #region Produkte
    public static Produkt ErstelleProdukt(int _typ)
    {
        // Produkt erstellen, via Typ statt Switch
        Produkt produkt = new Produkt(produkt_produktNamen[_typ]);

        // Das Produkt zurückgeben
        return produkt;
    }

    #endregion
}