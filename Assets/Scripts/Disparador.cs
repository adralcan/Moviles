using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparador : MonoBehaviour {

    public GameObject bolaPrefab;
    GameObject sprite;
    public int fuerza = 5;

    public int contBolas = 5;

    Vector2 direccion;
    LineRenderer lineRenderer;

    public float grosorLinea = 0.2f;

    Color c1 = Color.grey;
    Color c2 = Color.black;

    // Use this for initialization
    void Start () {
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
            lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
            lineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)));
        }

        //Version PC        
        if (Input.GetMouseButtonUp(0))
        {
            //Borramos linea
            lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
            lineRenderer.SetPosition(1, new Vector3(transform.position.x, transform.position.y, 0));

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            //Debug.Log(mousePosition);
            //Debug.DrawLine(transform.position, (transform.position + mousePosition) * 10, Color.red, Mathf.Infinity);
            
            direccion = mousePosition - transform.position;
            direccion = direccion.normalized;  
            
            Disparar();                      
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
        }
    }

    void Disparar()
    {        
        gameObject.GetComponent<TextMesh>().text = contBolas.ToString();

        if (contBolas > 0)
        {
            contBolas--;
            GameObject aux = Instantiate(bolaPrefab);
            //Nos aseguramos z = 0
            aux.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            //aux.GetComponent<Rigidbody2D>().AddForce(direccion * fuerza * Time.deltaTime);
            aux.GetComponent<Rigidbody2D>().velocity = direccion * fuerza * Time.deltaTime;

            if (contBolas > 0)
                Invoke("Disparar", 0.1f);
        }
    }


}
