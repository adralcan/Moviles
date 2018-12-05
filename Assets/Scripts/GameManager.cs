using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Objetos escena
    public Disparador Disparador;
    public DeadZone deadZone; 

    //Prefabs
    public GameObject muro;
    public Bloque bloque;
    public List<Bloque> listaBloques;

    public float distanciaX = 0.2f;
    public float distanciaY = 0.2f;

    [HideInInspector] public int contTemporal = 0;
    int nivel = 1;

    struct infoBloque
    {
        public int x;
        public int y;
        public int tipo;
    }  

    // Use this for initialization
    void Start()
    {
        listaBloques = new List<Bloque>();
        instance = this;
        colocarObjetos();
        ReadLevel("mapdata" + nivel + ".txt");
    }

    public void SiguienteNivel()
    {
        nivel++;
        ReadLevel("mapdata" + nivel + ".txt");
    }

    void colocarObjetos()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, Camera.main.nearClipPlane));
        Disparador.SetPosition(new Vector3(pos.x, pos.y, 0));

        pos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0f, Camera.main.nearClipPlane));
        deadZone.SetPosition(new Vector3(pos.x, pos.y, 0));
        deadZone.SetScale(new Vector3(50, 0.2f, 0.2f));

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
        if (golpes > 0)
        {
            //Debug.Log("CREA x: " + x + " y: " + y);
            Bloque nuevoBloque = Instantiate(bloque);

            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(x * distanciaX, 1.05f - (y * distanciaY), Camera.main.nearClipPlane));
            nuevoBloque.transform.position = new Vector3(pos.x, pos.y, 0);

            //nuevoBloque.transform.position = new Vector3(x, y, 0);
            nuevoBloque.contGolpes = golpes;
            listaBloques.Add(nuevoBloque);
        }
    }

    public void ReadLevel(string nivel)
    {
        string path = "Assets/Resources/" + nivel;

        //Read the text from directly from the test.txt file
        StreamReader archivo = new StreamReader(path);
        int j = 0;        
        int indiceInfoBloques = 0;
        char[] delimiterChar = { ',', '.'};
        string fila;
        List<infoBloque> infoBloques = new List<infoBloque>(); //Almacenamos info primera vuelta, en la segunda, creamos los bloques          

        while ((fila = archivo.ReadLine()) != null)
        {
            string[] split = fila.Split(delimiterChar);
            
            for (int i = 0; i < split.Length-1; i++)
            {
                if (j >= 3 && j < 14)
                {                    
                    infoBloque aux;                    
                    aux.x = i; aux.y = j; aux.tipo = Convert.ToInt32(split[i]);
                    infoBloques.Add(aux);                    
                }
                if (j > 16)
                {                  
                    CrearBloque(infoBloques[indiceInfoBloques].x, infoBloques[indiceInfoBloques].y, infoBloques[indiceInfoBloques].tipo,
                         Convert.ToInt32(split[i]));
                    indiceInfoBloques++;
                }                               
            } //Fin del for

            j++;
        }       

    }

    /*void WriteTxt()
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
    }*/

}

/*
 //string path = "Assets/Resources/" + nivel;

        //Read the text from directly from the test.txt file

        TextAsset archivo = Resources.Load(nivel) as TextAsset; //Resources.Load("glass") as Texture;
        
        //Debug.Log(archivo);
        //string cosa = archivo.text;
        //Debug.Log(cosa);
        int j = 0;        
        int indiceInfoBloques = 0;
        char[] delimiterChar = { ',', '.'};
        string[] cosa = archivo.text.Split(delimiterChar);
        string fila;
        List<infoBloque> infoBloques = new List<infoBloque>(); //Almacenamos info primera vuelta, en la segunda, creamos los bloques          
        for(int i = 0; i < cosa.Length; i++)
            Debug.Log(cosa[i]);

        /*while ((fila = archivo.ReadLine()) != null)
        {
            string[] split = fila.Split(delimiterChar);
            
            for (int i = 0; i < split.Length-1; i++)
            {
                if (j >= 3 && j < 14)
                {                    
                    infoBloque aux;                    
                    aux.x = i; aux.y = j; aux.tipo = Convert.ToInt32(split[i]);
                    infoBloques.Add(aux);                    
                }
                if (j > 16)
                {                  
                    CrearBloque(infoBloques[indiceInfoBloques].x, infoBloques[indiceInfoBloques].y, infoBloques[indiceInfoBloques].tipo,
                         Convert.ToInt32(split[i]));
                    indiceInfoBloques++;
                }                               
            } //Fin del for

            j++;
        }*/
     
