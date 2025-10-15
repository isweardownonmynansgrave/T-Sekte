using UnityEngine;

public class Wok : MonoBehaviour
{
    // Crafting
    [HideInInspector]
    public Produkt Ergebnis;
    private bool istEssenFertig;
    private bool istWokBefuellt;


    // Timing
    public float TimerZuBeginn = 30f;
    [HideInInspector]
    public float Timer;
    public float TimerAnbrennen;

    #region MonoBehaviour
    private void Awake()
    {
        Timer = 0;
        TimerAnbrennen = 0;
        Ergebnis = GameManager.ErstelleProdukt(GameManager.Index_Produkt_Nudeln);
        istEssenFertig = false;
        istWokBefuellt = false;
    }
    private void Update()
    {
        if (istWokBefuellt && !istEssenFertig)
        {
            if (Timer <= 0)
            {
                // Nudeln fertig - WIP
                istEssenFertig = true;
                // Weitere Dinge tun?
            }
            else
                Timer -= Time.deltaTime;
        }
        else if (istWokBefuellt && istEssenFertig)
        {
            TimerAnbrennen += Time.deltaTime;
        }
        else
        {
            
        }
    }
    #endregion
    
    #region Kochen
    public void BeginneKochen()
    {
        // Ausgangsstatus definieren
        istWokBefuellt = true;
        istEssenFertig = false;
        Timer = TimerZuBeginn;
    }
    #endregion
}