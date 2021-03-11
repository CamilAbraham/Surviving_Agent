using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class darDescanso : MonoBehaviour
{
    private float cantDescanso = 300;
    private int maxDescanso = 300;
    private GameObject Player;
    private float elapsed = 0.0f;
    private bool band = true;
    private GameObject objeto;
    public RectTransform healthbar;
    // Use this for initialization
    void Start()
    {
        objeto = this.gameObject;
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (Vector3.Distance(Player.transform.position, objeto.transform.position) < 10 && elapsed > 1.0f)
        {
            elapsed = 0.0f;
            if (Jugador.descanso < 100 && Jugador.descanso > 90)
            {
                cantDescanso -= 100 - Jugador.descanso;
                Jugador.descanso = 100;
            }
            else if (Jugador.descanso < 90)
            {
                cantDescanso -= 10;
                Jugador.descanso += 10;
            }
        }
        if (cantDescanso <= 0 && band)
        {
            Destroy(objeto);
            band = false;
            if (Jugador.lugarDescanso.IndexOf(transform.position) > -1) Jugador.lugarDescanso.RemoveAt(Jugador.lugarComida.IndexOf(transform.position));
        }
        healthbar.sizeDelta = new Vector2((cantDescanso / maxDescanso) * 200, healthbar.sizeDelta.y);
    }

}
