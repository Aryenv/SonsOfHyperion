using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TorretaPadre : MonoBehaviour
{
    public enum EficaciaObjetivo
    {
        Terrestre,
        Aereo,
        Todo
    }

    public EficaciaObjetivo m_eficacia;

    public int                      dmg;
    public string                   tipoEnemigo;
    public float                    velocidadDeDisparo;
    public float                    velocidadDeDisparoAlInicio;

    protected float                 rangoInicial;
    protected float                 dmgInicial;  
    protected bool                  haHechoDaño;
    protected int                   id;

    public bool                     enemigoFijado;
    public bool                     disparar;
    public bool                     haDisparado;

    public GameObject               objetivo;
    public GameObject               Bullet;
    public GameObject               puntoDeDisparo;
    public GameObject               reset;

    public Vector3                  direccion;
    public LayerMask                lmRaycastDisparo;

    public Rigidbody2D              rb;
    public UnitAI                   enemy;
    public CircleCollider2D         coll;
    protected SoundManager          sonido;
    public GameObject               imagenRango;

    [SerializeField] protected List<GameObject> listaEnemigos;

    public void Awake()
    {
        sonido                          =        FindObjectOfType<SoundManager>();
        rb                              =        GetComponent<Rigidbody2D>();
        velocidadDeDisparoAlInicio      =        velocidadDeDisparo;
        coll                            =        GetComponent<CircleCollider2D>();
    }

    public void Start()
    {        
        haDisparado     = true;
        dmgInicial      = dmg;
        rangoInicial    = coll.radius;
    }

    void Update()
    {
        velocidadDeDisparo -= Time.deltaTime;
        Torreta();
    }

    public void Torreta()
    {
        if (listaEnemigos.Count != 0)
        {
            if (objetivo == null)
            {
                disparar = true;

                BuscarObjetivo();
            }
        }

        if (objetivo != null && velocidadDeDisparo <= 0)
        {
            velocidadDeDisparo = velocidadDeDisparoAlInicio;
            DisparoTerrestre();
        }
    }

    public void BuscarObjetivo()
    {
        if (listaEnemigos[0] == null)
        {
            GameObject enemigoAEliminar = listaEnemigos[0];

            listaEnemigos.Remove(enemigoAEliminar);
        }

        objetivo = listaEnemigos[0];
    }

    public void OnMouseOver()
    {        
         imagenRango.SetActive(true);      
    }

    public void OnMouseExit()
    {
        imagenRango.SetActive(false);
    }
    //public void DispararRaycast(GameObject enemigo)
    //{
    //    //RotarHaciaEnemigo(enemigo);

    //    if (haDisparado)
    //    {
    //        velocidadDeDisparo -= Time.deltaTime;

    //        if (velocidadDeDisparo <= 0)
    //        {
    //            velocidadDeDisparo = velocidadDeDisparoAlInicio;
    //            haDisparado = false;                
    //        }
    //    }

    //    if (!haDisparado)
    //    {
    //        RaycastHit2D impacto = Physics2D.Raycast(puntoDeDisparo.transform.position, enemigo.transform.position - puntoDeDisparo.transform.position, 100, lmRaycastDisparo);

    //        //print(impacto.collider.gameObject.name);

    //        Debug.DrawLine(puntoDeDisparo.transform.position, enemigo.transform.position - puntoDeDisparo.transform.position, Color.blue, 3);

    //        if (impacto.transform.GetComponent<Rigidbody2D>() != null)
    //        {
    //            if (sonidoDisparo != null)
    //            {
    //                //sonidoDisparo.Play();
    //            }
    //            haDisparado = true;               
    //        }
    //        else
    //        {
    //            haDisparado = true;
    //        }
    //    }
    //}

    //public void RotarHaciaEnemigo(GameObject objetivo)
    //{
    //    Vector3 dir = objetivo.transform.position - transform.position;
    //    float angulo = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    //    rb.rotation = angulo;
    //}

    //public void AccionTorreta()
    //{
    //   Instantiate(Bullet, puntoDeDisparo.transform.position, puntoDeDisparo.transform.rotation);
    //    objetivo.GetComponent<RecibirAtaqueTorreta>().RecivirDañoTerrestre(this.gameObject, dmg);
    //}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<UnitDisplay>() != null)
        {
            listaEnemigos.Add(collision.gameObject);

            if (!haDisparado)
            {
                disparar = true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<UnitDisplay>() != null)
        {
            if (collision.gameObject == objetivo)
            {
                objetivo = reset;
            }

            listaEnemigos.Remove(collision.gameObject);
        }
    }
    public virtual void DisparoTerrestre()
    {

    }
    public virtual void DisparoAereo()
    {

    }
    public virtual void DisparoTerrestreYAereo()
    {

    }
}

public static class sonidosTorretas
{
    public static Action<int> activar_sonido;
}
