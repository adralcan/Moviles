using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using System.IO;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    //Objetos escena
    public GameObject Disparador;
    [HideInInspector] public Canvas Canvas;

    //Prefabs
    public GameObject muro;
    public GameObject bloque;


    public float distanciaX = 0.2f;
    public float distanciaY = 0.2f;

    // Use this for initialization
    void Start()
    {
        instance = this;
        Canvas = new Canvas();
        colocarObjetos();
        ReadLevel("mapdata0.txt");
    }    

    void colocarObjetos()
    {
            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, Camera.main.nearClipPlane));
            Disparador.transform.position = new Vector3(pos.x, pos.y, 0);

            GameObject muroIzquierdo = Instantiate(muro);
            pos = Camera.main.ViewportToWorldPoint(new Vector3(-0.01f, 0, Camera.main.nearClipPlane));
            muroIzquierdo.transform.position = new Vector3(pos.x, pos.y, 0);
            muroIzquierdo.transform.localScale = new Vector3(-0.1f, 50, 3);

            GameObject muroDerecho = Instantiate(muro);
            pos = Camera.main.ViewportToWorldPoint(new Vector3(1.01f, 0, Camera.main.nearClipPlane));
            muroDerecho.transform.position = new Vector3(pos.x, pos.y, 0);
            muroDerecho.transform.localScale = new Vector3(-0.1f, 50, 3);         

            GameObject muroArriba = Instantiate(muro);
            pos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1, Camera.main.nearClipPlane));
            muroArriba.transform.position = new Vector3(pos.x, pos.y, 0);
            muroArriba.transform.localScale = new Vector3(50, 2f, 3);                
    }

    void WriteString()
    {
        string path = "Assets/Resources/test.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Test");
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
        TextAsset asset = (TextAsset)Resources.Load("test");

        //Print the text from the file
        Debug.Log(asset.text);
    }

    void CrearBloque(int x, int y, int golpes)
    {
        GameObject nuevoBloque = Instantiate(bloque);

        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(x*distanciaX, 1 - (y*distanciaY), Camera.main.nearClipPlane));
        nuevoBloque.transform.position = new Vector3(pos.x, pos.y, 0);


        //nuevoBloque.transform.position = new Vector3(x, y, 0);
        nuevoBloque.GetComponent<ComportamientoBloque>().contGolpes = golpes; 
    }

    
    public void ReadLevel(string nivel)
    {
        string path = "Assets/Levels/" + nivel;

        //Read the text from directly from the test.txt file
        StreamReader archivo = new StreamReader(path);
        int j = 0;

        while (archivo != null)
        {
            string fila = archivo.ReadLine();
            if (fila.Length > 0 && j >= 3)
            {
                for (int i = 0; i < fila.Length; i++)
                {
                    //Si existe bloque
                    if ((int)System.Char.GetNumericValue(fila[i]) > 0)
                    {
                        CrearBloque(i, j, (int)System.Char.GetNumericValue(fila[i]));
                    }
                }
            }
            j++;            
        }
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
