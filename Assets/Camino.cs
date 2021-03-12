using UnityEngine;
using System.Collections;

public class Camino : MonoBehaviour {

	public GameObject[] zonesExplorables;
	public float velocidaCaminar = 1;
	public int zoneSelecione = 0;
	public enum tipoMovimiento {loop, inversa, random};
	public tipoMovimiento tipoDeMovimientos;



	void Caminar () {

		if (zoneSelecione >0){

			transform.LookAt(zonesExplorables[zoneSelecione].transform.position);
			transform.position = Vector3.MoveTowards(transform.position, zonesExplorables[zoneSelecione].transform.position, Time.deltaTime * velocidaCaminar);
			if(getDistance() < 0.5f){

				elegirNuevaZona();
				print ("Ya llegue");
			}
		}
	}

	float getDistance(){
		return Vector3.Distance(transform.position, zonesExplorables[zoneSelecione].transform.position);
	}

    void elegirNuevaZona()
    {
        if (tipoDeMovimientos == tipoMovimiento.loop)
        {
            zoneSelecione = zoneSelecione + 1;
            if (zoneSelecione >= zonesExplorables.Length)
            {
                zoneSelecione = 0;
            }
        }
        else if (tipoDeMovimientos == tipoMovimiento.inversa)
        {
            zoneSelecione = zoneSelecione - 1;
            if (zoneSelecione < 0)
            {
                zoneSelecione = zonesExplorables.Length - 1;
            }
        }
        else
        {
            zoneSelecione = Random.Range(0, zonesExplorables.Length - 1);
        }

    }

    /*void elegirNuevaZona(){
		zoneSelecione = zoneSelecione + 1;
		if (zoneSelecione >= zonesExplorables.Length){
		    zoneSelecione = 0;
		}
	}*/


    void Update () {

		Caminar();

	}
}
