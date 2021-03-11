using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class darVida : MonoBehaviour
{
    private float cantVida = 250;
    private int maxVida = 250;
    private GameObject Player;
    private float elapsed = 0.0f;
    private bool band = true;
    private GameObject objeto;
    public RectTransform healthbar;
    private int aux=0;
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
            if (Jugador.vida < 100 && Jugador.vida > 90)
            {
                cantVida -= 100 - Jugador.vida;
                Jugador.vida = 100;
            }
            else if (Jugador.vida < 90)
            {
                cantVida -= 10;
                Jugador.vida += 10;
            }
        }
        if (this.cantVida <= 0 && this.band)
        {
            Destroy(objeto);
            this.band = false;
            if ((aux=Jugador.lugarVida.IndexOf(transform.position)) > -1 && this.cantVida<=0)
                Jugador.lugarVida.RemoveAt(aux);
        }
        healthbar.sizeDelta = new Vector2((cantVida / maxVida) * 200, healthbar.sizeDelta.y);
    }

}

