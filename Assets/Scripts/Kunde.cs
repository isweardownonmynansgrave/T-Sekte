public class Kunde : MonoBehaviour
{
    private float timer;
    private Produkt[] bestellungen;
    private int anzahlBestellungen;

    // Mood-Alkohol-Mechanics??
    [Range(0,1f)]
    private float stimmung;
    private int alter;

    // Verwaltung
    public bool DebugMode = false;
    public int ANZAHL_UNIQUE_REZEPTE = 10;
    private float timerZuBeginn;

    #region Accessoren
    public float timer
    {
        get => timer;
        private set => timer = value;
    }
    public Produkt[] Bestellungen
    {
        get => bestellungen;
        private set => bestellungen = value;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    { 
        // Zeit-Management
        timer = 0;

        // Mood..
        stimmung = 1f;


        // Bestellungen
        Random rand = new Random();
        anzahlBestellungen = rand.Next(1, 4); // 1 = inklusive, 4 = exklusive -> ergibt 1, 2 oder 3
        bestellungen = new Produkt[anzahlBestellungen];

    }
    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            if (DebugMode)
                Debug.Log("Kunde - Timer < 0!!");
        }
    }
    #endregion
    public void SetRandomBestellungen(bool NutzeNudelRezepte = false)
    {
        int[] randomProduktZahlen = new int[anzahlBestellungen];
        Random rand = new Random();
        int untergrenzeRandom = NutzeNudelRezepte ? 0 : 1; // Wenn Nudel ja, dann Nudelrezept auf 0 mit einbeziehen, Konzept WIP
        for (int i = 0; i < anzahlBestellungen; i++)
            randomProduktZahlen[i] = rand.Next(untergrenzeRandom, ANZAHL_UNIQUE_REZEPTE);

        for (int i = 0; i < anzahlBestellungen; i++)
        {
            // Random Bestellung auf Basis des integers definieren
            bestellungen[i] = new Produkt(randomProduktZahlen[i]);
        }
    }
}