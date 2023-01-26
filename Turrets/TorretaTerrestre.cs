using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaTerrestre : TorretaPadre
{
    protected new void Start()
    {
        base.Start();
        m_eficacia = EficaciaObjetivo.Terrestre;
    }

    public override void DisparoTerrestre()
    {
        if (objetivo.GetComponent<UnitAI>().tipoEnemigo == "Terrestre" && objetivo.GetComponent<UnitDisplay>().life >= 0)
        {
            GameObject go = Instantiate(Bullet, puntoDeDisparo.transform.position, puntoDeDisparo.transform.rotation);
            objetivo.GetComponent<UnitAI>().RecieveDamage(dmg);
            sonidosTorretas.activar_sonido(id);
        }
        else
        {
            listaEnemigos.Remove(this.gameObject);
        } 
    }
}