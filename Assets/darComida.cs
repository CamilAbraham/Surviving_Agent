using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class darComida : MonoBehaviour {
    private float cantComida = 400;
    private int maxComida = 400;
    private GameObject Player;
    private float elapsed = 0.0f;
    private bool band = true;
    private GameObject objeto;
    public RectTransform healthbar;
    // Use this for initialization
    void Start () {
        objeto = this.gameObject;
        Player = GameObject.FindWithTag("Player");
	}

    // Update is called once per frame
    void Update() {
        elapsed += Time.deltaTime;
        if (Vector3.Distance(Player.transform.position, objeto.transform.position) < 10 && elapsed>1.0f)
        {
            elapsed = 0.0f;
            if (Jugador.comida < 100 && Jugador.comida > 90)
            {
                cantComida -= 100 - Jugador.comida;
                Jugador.comida = 100;
            }
            else if(Jugador.comida<90)
            {
                cantComida -= 10;
                Jugador.comida += 10;
            }
        }
        if (cantComida <= 0 && band)
        {
            Destroy(objeto);
            band = false;
            if (Jugador.lugarComida.IndexOf(transform.position)>-1) Jugador.lugarComida.RemoveAt(Jugador.lugarComida.IndexOf(transform.position));
        }
        healthbar.sizeDelta = new Vector2((cantComida/maxComida)*200,healthbar.sizeDelta.y);
    }
    
}
