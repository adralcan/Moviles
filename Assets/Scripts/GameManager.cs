using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using System.IO;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    //Objetos escena
    public GameObject Disparador;
    public GameObject Recolector;

    //Prefabs
    public GameObject muro;
    public GameObject bloque;

    public float distanciaX = 0.2f;
    public float distanciaY = 0.2f;

    struct infoBloque
    {
        public int x;
        public int y;
        public int tipo;
    }

    // Use this for initialization
    void Start()
    {
        instance = this;
        colocarObjetos();
        ReadLevel("mapdata1.txt");
    }

    void colocarObjetos()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, Camera.main.nearClipPlane));
        Disparador.transform.position = new Vector3(pos.x, pos.y, 0);

        pos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0f, Camera.main.nearClipPlane));
        Recolector.transform.position = new Vector3(pos.x, pos.y, 0);
        Recolector.transform.localScale = new Vector3(50, 0.2f, 0.2f);

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

    void CrearBloque(int x, int y, int tipo, int golpes)
    {
        GameObject nuevoBloque = Instantiate(bloque);

        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(x * distanciaX, 1 - (y * distanciaY), Camera.main.nearClipPlane));
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
        int contPuntos = 0;

        List<infoBloque> infoBloques = new List<infoBloque>(); //Almacenamos info primera vuelta, en la segunda, creamos los bloques  

        while (archivo != null)
        {
            string fila = archivo.ReadLine();

            //Layer 1
            if (j >= 3 && j < 14)
            {
                for (int i = 0; i < fila.Length; i++)
                {
                    if (fila[i] == '.')
                        contPuntos++;

                    //Si existe bloque
                    if (fila[i] != ',' && contPuntos < 1 && fila[i] != '0' && fila[i] != '.')
                    {
                        //CrearBloque(i, j, (int)System.Char.GetNumericValue(fila[i]));
                        infoBloque aux;
                        aux.x = i; aux.y = j; aux.tipo = (int)System.Char.GetNumericValue(fila[i]);
                        infoBloques.Add(aux);
                    }

                }//Fin del for
            }

            //Layer 2
            if (j > 16)
            {
                for (int i = 0; i < fila.Length; i++)
                {
                    if (fila[i] == '.')
                        contPuntos++;

                    //Matriz que da valor a los bloques, creacion
                    if (fila[i] != ',' && contPuntos >= 1 && fila[i] != '0' && fila[i] != '.')
                    {
                        CrearBloque(infoBloques[i].x, infoBloques[i].y, infoBloques[i].tipo, (int)System.Char.GetNumericValue(fila[i]));
                    }

                }//Fin del for
            }

            j++;

            if (contPuntos > 1) //Final del archivo
                break;
        }

    }

    void WriteTxt()
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

}
