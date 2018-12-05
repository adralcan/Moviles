using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float velocidad = 500;
    private Rigidbody2D rigidbody_;

    public delegate void CallBack();
    //MyDelegate myDelegate;

    private void Awake()
    {
        rigidbody_ = GetComponent<Rigidbody2D>();
        if (rigidbody_ == null)
            Debug.Log("La bola no tiene rigidbody");
    }

    public void Shoot(Vector3 posIni, Vector3 direVelocity)
    {
        transform.position = posIni;
        rigidbody_.velocity = direVelocity;
    }

    //Retorno de la bola
    public void MoveTo(Vector3 pos, float step, CallBack delegado = null)
    {
        StartCoroutine(MoveToCoroutine(pos, step, delegado));
    }

    IEnumerator MoveToCoroutine(Vector3 pos, float step, CallBack delegado = null)
    {
        if (delegado != null)
            StopCoroutine(MoveToCoroutine(pos, step, delegado));

        transform.position = Vector3.MoveTowards(transform.position, pos, step);

        //yield return new WaitForSeconds(0.1f);
        yield return new WaitForFixedUpdate();
                      
    }
}
