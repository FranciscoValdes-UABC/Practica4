using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plano : MonoBehaviour
{
    public float angulo;
    public float friccion;
    public float masaCubo;
    public GameObject cubo;

    public Text txt_Friccion;
    public Text txt_Fuerza;
    public Text txt_Peso;
    public Text txt_pos;
    public Text txt_vel;
    public Text txt_acel;

    private float peso, t;
    private Vector3 posIn;
    private Vector3 W;
    private Vector3 Normal;
    private Vector3 Ffriccion;
    private Vector3 Fuerza;
    private Vector3 aceleracion;
    private Vector3 velocidad;
    private Vector3 posicion;

    // Start is called before the first frame update
    void Start()
    {
        posIn = cubo.transform.position;
        if(angulo < 0)
        {
            friccion = -friccion;
        }
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angulo);
        cubo.transform.position = transform.position + transform.up * 0.65f;
        cubo.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angulo);

        peso = masaCubo * -9.81f;
        W = new Vector3( peso * Mathf.Sin((angulo*Mathf.PI)/180) , peso * Mathf.Cos((angulo * Mathf.PI) / 180) , 0);
        Normal = new Vector3(0, -W.y, 0);
        Ffriccion = new Vector3(friccion * Normal.y, 0, 0);
        Fuerza = Normal + Ffriccion + W;

        if (angulo < 0)
        {
            if (friccion < Mathf.Tan((angulo * Mathf.PI) / 180))
            {
                Fuerza = new Vector3(0, Fuerza.y, 0);
            }
            aceleracion = Fuerza / masaCubo;
            aceleracion = new Vector3(aceleracion.x * Mathf.Cos((angulo * Mathf.PI) / 180), aceleracion.x * Mathf.Sin((angulo * Mathf.PI) / 180), 0);
        }
        else
        {
            if (friccion > Mathf.Tan((angulo * Mathf.PI) / 180))
            {
                Fuerza = new Vector3(0, Fuerza.y, 0);
            }
            aceleracion = Fuerza / masaCubo;
            aceleracion = new Vector3(aceleracion.x * Mathf.Sin((angulo * Mathf.PI) / 180), aceleracion.x * Mathf.Cos((angulo * Mathf.PI) / 180), 0);
        }

        txt_Friccion.text = "Friccion: " + Ffriccion;
        txt_Fuerza.text = "Fuerza: " + Fuerza;
        txt_Peso.text = "Peso: " + W;
        txt_pos.text = "Posicion: " + posicion;
        txt_vel.text = "Velocidad: " + velocidad;
        txt_acel.text = "Aceleracion: " + aceleracion;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        velocidad = aceleracion * t;
        posicion = posIn + 0.5f * (aceleracion * Mathf.Pow(t, 2));

        cubo.transform.position += posicion;

        txt_pos.text = "Posicion: " + posicion;
        txt_vel.text = "Velocidad: " + velocidad;
    }
}
