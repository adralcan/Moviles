using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Canvas y text

public class ComportamientoBloque : MonoBehaviour {    

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
                Destroy(gameObject);
        }
    }

}
