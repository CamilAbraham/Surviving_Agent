using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Jugador : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    Vector3 random;
    private float elapsed = 0.0f;
    private NavMeshPath path;
    public Terrain terrain;
    private int terrainWidth; 
    private int terrainLength;
    private int terrainPosX; 
    private int terrainPosZ;
    bool tienePunto = false;
    private float espera;

    public float Velocidad = 4.0f;

    public RectTransform healthbar;
    public RectTransform foodbar;
    public RectTransform restbar;

    public static float vida = 100;
    public static float descanso = 100;
    public static float comida = 100;
    public float tiempoQuitarComidayDescanso = 4.0f;
    float tiempoDescanso = 0f;
    float tiempoComida = 0f;
    public static float tiempoAtaque = 0f;
    public static List <Vector3> lugarComida = new List<Vector3>();
    public static List <Vector3> lugarDescanso = new List<Vector3>();
    List <Vector3> lugarPeligro = new List<Vector3>();
    public static List <Vector3> lugarVida = new List<Vector3>();
    bool estaComiendo = false;
    bool estaDescansando = false;
    bool estaVida = false;
    public static bool esAtacado = false;
    Vector3 aux;

    public static float elapsed2=0.0f;

    public static float tiempoCollider = 0f;
    public float tiempoEsperaCollider = 3.0f;
    public int tamanoEsfera = 15;
    Collider[] cerca;
    string nomTag;
    Vector3 puntoTag;
    bool band;

    GUIStyle style;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        elapsed = 0.0f;
        path = new NavMeshPath();
        terrainWidth = (int)terrain.terrainData.size.x;
        terrainLength = (int)terrain.terrainData.size.z;
        terrainPosX = (int)terrain.transform.position.x;
        terrainPosZ = (int)terrain.transform.position.z;
        band = false;
        agent.speed = Velocidad;
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        elapsed2 += Time.deltaTime;
        if (esAtacado)
        {
            tiempoAtaque += Time.deltaTime;
            agent.speed = Velocidad * 3;
            if (!lugarPeligro.Contains(transform.position)) lugarPeligro.Add(transform.position);
            if (tiempoAtaque > 3.0f)
            {
                esAtacado = false;
                tiempoAtaque = 0.0f;
                tienePunto = false;
                agent.speed = Velocidad;
            }
        }
        if (elapsed2 > 3.0f)
        {
            elapsed2 = 0.0f;
            /*Debug.Log("LUGAR COMIDA COUNT: " + lugarComida.Count.ToString());
            Debug.Log("LUGAR DESCANSO COUNT: " + lugarDescanso.Count.ToString());
            Debug.Log("LUGAR VIDA COUNT: " + lugarVida.Count.ToString());
            Debug.Log("LUGAR PELIGRO COUNT: " + lugarPeligro.Count.ToString());*/
        }
        if (agent.remainingDistance < 2 && !agent.pathPending)
        {
            tienePunto = false;
        }
        if (!tienePunto && vida > 0)
        {
            band = true;
            random = new Vector3(Random.Range(terrainPosX, terrainPosX + terrainWidth), 10, Random.Range(terrainPosZ, terrainPosZ + terrainLength));
            bool band2 = false;
            for (int i = 0; i < lugarPeligro.Count; i++)
            {
                if ((random.x<lugarPeligro[i].x+6) && (random.x > lugarPeligro[i].x - 6) && (random.z < lugarPeligro[i].z + 6) && (random.z > lugarPeligro[i].z + 6))
                {
                    band2 = true;
                }
            }
            if (NavMesh.CalculatePath(transform.position, random, NavMesh.AllAreas, path) && !band2)
            {
                tienePunto = true;
                agent.destination = random;
                for (int i = 0; i < path.corners.Length - 1; i++) Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, 50.0f, false);
            }
        }
        tiempoDescanso += Time.deltaTime;
        if (tiempoDescanso >= tiempoQuitarComidayDescanso && vida > 0)
        {
            if (descanso <= 0)
            {
                descanso = 0;
                vida -= 3;
            }
            else
            {
                descanso -= Random.Range(1, 5);
            }
            tiempoDescanso = 0.0f;
        }
        tiempoComida += Time.deltaTime;
        if (tiempoComida >= tiempoQuitarComidayDescanso && vida > 0)
        {
            if (comida <= 0)
            {
                comida = 0;
                vida -= 4;
            }
            else
            {
                comida -= Random.Range(2, 8);
            }
            tiempoComida = 0.0f;
        }



        tiempoCollider += Time.deltaTime;
        if (tiempoCollider >= tiempoEsperaCollider && vida > 0)
        {
            tiempoCollider = 0.0f;
            cerca = Physics.OverlapSphere(transform.position, tamanoEsfera);
            for(int i = 0; i < cerca.Length; i++)
            {
                nomTag = cerca[i].tag;
                //Debug.Log(cerca[i].tag);
                aux = cerca[i].transform.position;
                if (nomTag.Equals("Comida") && !lugarComida.Contains(aux))
                {
                    lugarComida.Add(aux);
                }
                if (nomTag.Equals("Descanso") && !lugarDescanso.Contains(aux))
                {
                    lugarDescanso.Add(aux);
                }
                if (nomTag.Equals("Vida") && !lugarVida.Contains(aux))
                {
                    lugarVida.Add(aux);
                }
            }
        }
        if (vida <= 0 && band)
        {
            vida = 0;
            anim.SetTrigger("Die");
            agent.isStopped = true;
            band = false;
        }

        if (comida <= 30 && lugarComida.Count>0 && !(comida>=90))
        {
            float aux2=9999;
            int pos=0;
            estaComiendo = true;
            for (int i = 0; i < lugarComida.Count; i++)
            {
                if (Vector3.Distance(lugarComida[i], transform.position) < aux2)
                {
                    aux2 = Vector3.Distance(lugarComida[i], transform.position);
                    pos = i;
                }
            }
            agent.speed = Velocidad*2;
            agent.destination = lugarComida[pos];
        }
        if (comida > 90 && estaComiendo)
        {
            agent.speed = Velocidad;
            tienePunto = false;
            estaComiendo = false;
        }
        if (descanso <= 30 && lugarDescanso.Count > 0 && !(descanso >= 90) && (comida>30 || lugarComida.Count==0))
        {
            float aux2 = 9999;
            int pos = 0;
            estaDescansando = true;
            for (int i = 0; i < lugarDescanso.Count; i++)
            {
                if (Vector3.Distance(lugarDescanso[i], transform.position) < aux2)
                {
                    aux2 = Vector3.Distance(lugarDescanso[i], transform.position);
                    pos = i;
                }
            }
            agent.speed = Velocidad*2;
            agent.destination = lugarDescanso[pos];
        }
        if (descanso > 90 && estaDescansando)
        {
            agent.speed = Velocidad;
            tienePunto = false;
            estaDescansando = false;
        }
        if (vida <= 30 && lugarVida.Count > 0 && !(vida >= 90) && (comida > 30 || lugarComida.Count == 0) && (descanso > 30 || lugarDescanso.Count == 0))
        {
            float aux2 = 9999;
            int pos = 0;
            estaVida = true;
            for (int i = 0; i < lugarVida.Count; i++)
            {
                if (Vector3.Distance(lugarVida[i], transform.position) < aux2)
                {
                    aux2 = Vector3.Distance(lugarVida[i], transform.position);
                    pos = i;
                }
            }
            agent.speed = Velocidad * 2;
            agent.destination = lugarVida[pos];
        }
        if (vida > 90 && estaVida)
        {
            agent.speed = Velocidad;
            tienePunto = false;
            estaVida = false;
        }

        healthbar.sizeDelta = new Vector2((vida / 100) * 200, healthbar.sizeDelta.y);
        foodbar.sizeDelta = new Vector2((comida / 100) * 200, foodbar.sizeDelta.y);
        restbar.sizeDelta = new Vector2((descanso / 100) * 200, restbar.sizeDelta.y);


        /*espera = 0.0f;
                while (espera < .2f) espera += Time.deltaTime;
                
                Debug.Log(espera);
         * 
         * if (elapsed >= 1.0f)
        {
            elapsed = -0.5f;
            Debug.Log(agent.remainingDistance);
        }*/



        /*if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(Input.mousePosition);
            Ray ray;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                agent.destination = hitInfo.point;
        }*/
    }


    void OnGUI()
    {
        if (vida <= 0)
        {
            style = new GUIStyle(GUI.skin.box);
            style.alignment = TextAnchor.MiddleCenter;
            GUI.Box(new Rect(0,0,Screen.width,Screen.height), "MORISTE", style);
        }
    }

}