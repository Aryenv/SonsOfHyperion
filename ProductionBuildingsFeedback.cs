using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProductionBuildingsFeedback : MonoBehaviour
{
    public GameObject FloatingAmountPrefab;

    public void ShowFloatingText(CurrencyType currency, int amount, Vector2 node, Tilemap tilemap)
    {
        Vector2 pos = tilemap.GetCellCenterWorld(new Vector3Int((int)node.x, (int)node.y, 0));
        pos += new Vector2(1,0);
        InstantiatePrefab(FloatingAmountPrefab,pos,amount, currency);
    }

    private void InstantiatePrefab(GameObject InstaceObjet, Vector2 pos, int amount, CurrencyType currency)
    {
        GameObject go = Instantiate(InstaceObjet, pos, Quaternion.identity);
        var img = Resources.Load<Sprite>("_Sprites/" + currency.ToString() + "") as Sprite;
        go.GetComponentInChildren<SpriteRenderer>().sprite = img;
        go.GetComponentInChildren<TextMeshPro>().text = amount.ToString();
    }
}
