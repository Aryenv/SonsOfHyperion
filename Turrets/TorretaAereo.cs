using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaAereo : TorretaPadre
{
    protected new void Start()
    {
        base.Start();
        m_eficacia = EficaciaObjetivo.Aereo;
    }

    void Update()
    {
        CheckFailDistance();
    }

    public override void DisparoAereo()
    {
        if (objetivo.GetComponent<UnitAI>().tipoEnemigo == "Aereo" && objetivo.GetComponent<UnitDisplay>().life > 0)
        {
            Instantiate(Bullet, puntoDeDisparo.transform.position, puntoDeDisparo.transform.rotation);
            //sonido.PlayTurretAttack();
            objetivo.GetComponent<UnitAI>().RecieveDamage(dmg);
            Debug.Log("Hago daño");
        }
        else
        {
            listaEnemigos.Remove(this.gameObject);
        }
    }

    public void CheckFailDistance()
    {
        if (coll.radius < rangoInicial / 3)
        {
            disparar = false;
        }
        else
        {
            disparar = true;
        }
    }
}
