using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {   //Esto iria en el ballsink en verda     
        if (other.gameObject.tag == "Bola")
        {
            GameManager.instance.contTemporal++;
            //GameManager.instance.Disparador.GetComponent<Disparador>().contBolas++;
            GameManager.instance.Disparador.SetContBolas();
            //other.GetComponent<Ball>().MoveTo(transform.position, 0.1f);

            if (GameManager.instance.contTemporal == 1)
            {
                GameManager.instance.Disparador.SetPosAux(new Vector3(other.gameObject.transform.position.x,
                    GameManager.instance.Disparador.transform.position.y,
                    GameManager.instance.Disparador.transform.position.z));
            }

            //other.GetComponent<Volver>().RetornoBola(GameManager.instance.Disparador.GetComponent<Disparador>().posicionFinal);
            //Destroy(other.gameObject);
        }
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }
}
