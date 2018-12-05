using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparador : MonoBehaviour
{
    public Ball bolaPrefab;    
    public int fuerza = 5;

    public int contBolas = 5;
    public int bolasMax;

    public Vector3 posicionFinal; //Auxiliar para cuando la primera bola cambie la posicion

    Vector2 direccion;
    LineRenderer lineRenderer;

    public float grosorLinea = 0.2f;

    Color c1 = Color.grey;
    Color c2 = Color.black;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<TextMesh>().text = contBolas.ToString();
        //Propiedades de la linea-trayectoria
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.widthMultiplier = grosorLinea;

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
            );
        lineRenderer.colorGradient = gradient;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            //Dibujar linea
            lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 10));
            lineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));
        }
            
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(Disparar());

            //Borramos linea
            lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
            lineRenderer.SetPosition(1, new Vector3(transform.position.x, transform.position.y, 0));

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

            direccion = mousePosition - transform.position;
            direccion = direccion.normalized;
        }


    /*#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                /*if (touch.phase == TouchPhase.Began)
                {

                }

                if (touch.phase == TouchPhase.Ended)
                {
                    //Borramos linea
                    lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
                    lineRenderer.SetPosition(1, new Vector3(transform.position.x, transform.position.y, 0));
                }

                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    //Dibujar linea
                    lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
                    lineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)));
                }
            }
        }
        endif*/
    }

    IEnumerator Disparar()
    {
        yield return new WaitForSeconds(0.1f);
        if (contBolas > 0)
        {
            contBolas--;
            Ball aux = Instantiate(bolaPrefab);
            //Nos aseguramos z = 0
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, 0);
            //aux.GetComponent<Rigidbody2D>().velocity = direccion * fuerza * Time.deltaTime; //Antigua version fea
            aux.Shoot(pos, direccion * fuerza * Time.deltaTime);

            gameObject.GetComponent<TextMesh>().text = contBolas.ToString();
            yield return StartCoroutine(Disparar());
        }
        else
        {
            StopCoroutine(Disparar());
            //contBolas = GameManager.instance.contTemporal;
        }
        gameObject.GetComponent<TextMesh>().text = contBolas.ToString();
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void SetContBolas(int n = 0)
    {
        if(n != 0)
            gameObject.GetComponent<TextMesh>().text = contBolas.ToString();

        gameObject.GetComponent<TextMesh>().text = n.ToString();
    }

    public void SetPosAux(Vector3 aux)
    {
        posicionFinal = aux;
    }



}
