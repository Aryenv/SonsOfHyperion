using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecibirAtaqueTorreta : MonoBehaviour
{
    [SerializeField] float life;
    float maxLife;
    public int dañoPlayer = 10;// daño scriptable
    public string tipoEnemigo;

    private void Start()
    {
        maxLife = life;
    }

    void Update()
    {
        Muerte();
    }

    public void RecivirDañoTerrestre(GameObject torre, float damage)
    {
        if (torre.GetComponent<TorretaPadre>() != null)
        {
            if (torre.GetComponent<TorretaPadre>().tipoEnemigo.ToString() == "Terrestre" || torre.GetComponent<TorretaPadre>().tipoEnemigo.ToString() == "Todo")
            {
                life -= damage;
            }
        }
    }
    public void RecivirDañoAreo(GameObject torre, float damage)
    {
        if (torre.GetComponent<TorretaPadre>() != null)
        {
            if (torre.GetComponent<TorretaPadre>().tipoEnemigo.ToString() == "Aereo" || torre.GetComponent<TorretaPadre>().tipoEnemigo.ToString() == "Todo")
            {
                life -= damage;
            }
        }
    }
    void Muerte()
    {
        if (life < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
