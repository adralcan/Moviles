using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Canvas y text

public class Bloque : MonoBehaviour {    

    public int contGolpes = 7;
    bool vulnerable = true;

	// Use this for initialization
	void Start () {        
        AddText(contGolpes.ToString());
    }

    public void AddText(string cadena)
    {
        TextMesh textAux = gameObject.GetComponentInChildren<TextMesh>();
        textAux.text = cadena;
    }    

    void toggleVulnerable() {
        vulnerable = !vulnerable;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bola" && vulnerable)
        {
            vulnerable = !vulnerable;
            Invoke("toggleVulnerable", 0.15f);
            contGolpes--;
            AddText(contGolpes.ToString());
            //Debug.Log(contGolpes);
            if (contGolpes <= 0)
            {
                GameManager.instance.listaBloques.Remove(this);
                Destroy(gameObject);
                Debug.Log("Tamanio lista bloques: " + GameManager.instance.listaBloques.Count);
                if (GameManager.instance.listaBloques.Count <= 1) {
                    //Borrar bolas y cambiar de nivel
                    //Provisional
                    GameObject[] bolas = GameObject.FindGameObjectsWithTag("Bola");
                    for (int i = 0; i < bolas.Length; i++) {
                        Destroy(bolas[i]);
                    }
                    GameManager.instance.SiguienteNivel();
                }

            }
        }
    }

}
