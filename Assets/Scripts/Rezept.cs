using System.Collections.Generic;

public class Rezept
{
    public List<Zutat> zutaten;
    public Produkt ergebnis;

    public Rezept(List<Zutat> _zutaten, Produkt _ergebnis)
    {
        zutaten = _zutaten;
        ergebnis = _ergebnis;
    }

    public bool Matches(List<Zutat> inputItems)
    {
        if (inputItems.Count != zutaten.Count)
            return false;

        // Prüfen, ob alle Zutaten enthalten sind (Reihenfolge egal)
        return false;//!zutaten.Except(inputItems).Any();
    }
}