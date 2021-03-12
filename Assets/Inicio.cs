using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class Inicio : MonoBehaviour {

    public GameObject myPrefab;
    public GameObject myPrefab2;
    public GameObject myPrefab3;
    public Terrain terrain;
    private int terrainWidth;
    private int terrainLength;
    private int terrainPosX;
    private int terrainPosZ;
    private Vector3 rand;
    private GameObject NewPrefab;
    public GameObject agents;
    private NavMeshAgent agent;
    private NavMeshPath path;

    // This script will simply instantiate the Prefab when the game starts.
    void Start()   
    {
        terrainWidth = (int)terrain.terrainData.size.x;
        terrainLength = (int)terrain.terrainData.size.z;
        terrainPosX = (int)terrain.transform.position.x;
        terrainPosZ = (int)terrain.transform.position.z;
        agent = agents.GetComponent<NavMeshAgent>();
        path = new NavMeshPath();


        for (var i = 0; i < 3; i++)
        {
            rand = new Vector3(Random.Range(terrainPosX, terrainPosX + terrainWidth), 10, Random.Range(terrainPosZ, terrainPosZ + terrainLength));
            while (!(NavMesh.CalculatePath(agents.transform.position, rand, NavMesh.AllAreas, path)) || (rand.x>90 && rand.x<150 && rand.z>265 && rand.z<305))
            {
                rand = new Vector3(Random.Range(terrainPosX, terrainPosX + terrainWidth), 10, Random.Range(terrainPosZ, terrainPosZ + terrainLength));
            }
            rand.y += 60;
            NewPrefab = (GameObject)Instantiate(myPrefab, rand, Quaternion.identity);
            NewPrefab.name = "Bed" + i.ToString();
            rand = new Vector3(Random.Range(terrainPosX, terrainPosX + terrainWidth), 10, Random.Range(terrainPosZ, terrainPosZ + terrainLength));
            while (!(NavMesh.CalculatePath(agents.transform.position, rand, NavMesh.AllAreas, path)) || (rand.x > 90 && rand.x < 150 && rand.z > 265 && rand.z < 305))
            {
                rand = new Vector3(Random.Range(terrainPosX, terrainPosX + terrainWidth), 10, Random.Range(terrainPosZ, terrainPosZ + terrainLength));
            }
            rand.y += 60;
            NewPrefab = (GameObject)Instantiate(myPrefab2, rand, Quaternion.identity);
            NewPrefab.name = "Mesa" + i.ToString();
            rand = new Vector3(Random.Range(terrainPosX, terrainPosX + terrainWidth), 10, Random.Range(terrainPosZ, terrainPosZ + terrainLength));
            while (!(NavMesh.CalculatePath(agents.transform.position, rand, NavMesh.AllAreas, path)) || (rand.x > 90 && rand.x < 150 && rand.z > 265 && rand.z < 305))
            {
                rand = new Vector3(Random.Range(terrainPosX, terrainPosX + terrainWidth), 10, Random.Range(terrainPosZ, terrainPosZ + terrainLength));
            }
            rand.y += 60;
            NewPrefab = (GameObject)Instantiate(myPrefab3, rand, Quaternion.identity);
            NewPrefab.name = "pref_Fountain" + i.ToString();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
