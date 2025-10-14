using UnityEngine;

public class Wok : MonoBehaviour
{
    // Crafting
    public Produkt Ergebnis;

    // Timing
    public float TimerZuBeginn = 30f;
    [HideInInspector]
    public float Timer;

    #region MonoBehaviour
    private void Awake()
    {
        Timer = TimerZuBeginn;
    }
    private void Update()
    {
        if (Timer <= 0)
        {
            // Nudeln fertig - WIP
        }    
        else
            Timer -= Time.deltaTime;
    }
    #endregion
}