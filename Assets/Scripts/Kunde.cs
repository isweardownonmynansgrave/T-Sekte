using UnityEngine;
using System.Collections;
public class Kunde : MonoBehaviour
{
    #region Instanzvariablen
    private float timer;
    private Produkt[] bestellungen;
    private int anzahlBestellungen;

    // Mood-Alkohol-Mechanics??
    [Range(0, 1f)]
    private float stimmung;
    private int alter;

    // Verwaltung
    public bool DebugMode = false;
    public int ANZAHL_UNIQUE_REZEPTE = 10; // Später aus Liste fetchen?
    private float timerZuBeginn;

    // Coroutinen
    private Coroutine moodCoroutine;      // Referenz zur laufenden Coroutine
    #endregion

    #region Accessoren
    public float Timer
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
        moodCoroutine = StartCoroutine(AktualisiereMood());

        // Bestellungen
        anzahlBestellungen = Random.Range(1, 4); // 1 = inklusive, 4 = exklusive -> ergibt 1, 2 oder 3
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
    private void OnDisable()
    {
        StopMoodCoroutine();
    }

    private void OnDestroy()
    {
        StopMoodCoroutine();
    }
    #endregion
    public void SetRandomBestellungen(Produkt[] _bestellungen, bool NutzeAlkoholRezepte = true)
    {
        int untergrenzeRandom = NutzeAlkoholRezepte ? 0 : 1; // Wenn Alkohol ja, dann Alkoholrezept auf 0 mit einbeziehen, Konzept WIP
        //Random.Range(untergrenzeRandom, ANZAHL_UNIQUE_REZEPTE)

        for (int i = 0; i < anzahlBestellungen; i++)
        {
            // Random Bestellung auf Basis des integers definieren
            _bestellungen[i] = GameManager.ErstelleProdukt(Random.Range(untergrenzeRandom, ANZAHL_UNIQUE_REZEPTE));
        }
    }

    #region Coroutines
    private IEnumerator AktualisiereMood()
    {
        while (true)
        {
            // mood = aktueller Timerwert relativ zur Startzeit (0 bis 1)
            stimmung = Mathf.Clamp01(timer / timerZuBeginn);

            // falls du magst, hier eine kleine Debug-Ausgabe:
            // Debug.Log($"Mood: {mood:F2}");

            // Wenn Timer abgelaufen ist, brich Schleife oder pausiere
            if (timer <= 0)
            {
                timer = 0;
                stimmung = 0;
                yield break; // Coroutine endet hier
            }

            // warte bis zum nächsten Frame
            yield return null;
        }
    }
    private void StopMoodCoroutine()
    {
        if (moodCoroutine != null)
        {
            StopCoroutine(moodCoroutine);
            moodCoroutine = null;
        }
    }
    #endregion
}