using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemigo : MonoBehaviour {

    private GameObject Player;
    private NavMeshAgent naveMesh;
    public int distanciaPers=15;
    public int distanciaAtac=3;
    public int daño = 10;
    public GameObject[] DestinosAleatorios;
    private int rand=0;
    private float tiempoAtaque = 0.0f;
    private float tiempo = 0.0f;

    // Use this for initialization
    void Start () {
        Player = GameObject.FindWithTag("Player");
        naveMesh = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.x > 90 && this.transform.position.x < 150 && this.transform.position.z > 265 && this.transform.position.z < 305)
        {
            if (naveMesh.remainingDistance < 3 && Vector3.Distance(Player.transform.position, this.transform.position) > distanciaPers)// && !naveMesh.pathPending
            {
                naveMesh.speed = 5;
                naveMesh.SetDestination(DestinosAleatorios[rand].transform.position);
                rand += 1;
                if (rand > 2) rand = 0;
            }
            if (Vector3.Distance(Player.transform.position, this.transform.position) < distanciaPers)
            {
                naveMesh.destination = Player.transform.position;
                naveMesh.speed = 8;
            }
        }
        else{
            naveMesh.speed = 5;
            naveMesh.destination = DestinosAleatorios[0].transform.position;
        }


        tiempoAtaque += Time.deltaTime;
        tiempo += Time.deltaTime;
        if (tiempo > 1.0f)
        {
            tiempo = 0;
        }
        if (Vector3.Distance(Player.transform.position, this.transform.position) < distanciaAtac && tiempoAtaque>2.0f)
        {
            tiempoAtaque = 0.0f;
            Jugador.esAtacado = true;
            Jugador.vida -= daño;
            Jugador.tiempoAtaque = 0f;
        }
        if (Jugador.vida <= 0)
        {
            naveMesh.isStopped = true;
        }
    }
}
