using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSystem : MonoBehaviour
{
    public GameObject RootPrefab;
    public float spawnCircleRadius = 10;

    private List<GameObject> RootList;

    public void CreateRoot(Root rootData, RangedFloat AngleOfAttack)
    {
        //get the angle
        float angle = Random.Range(AngleOfAttack.minValue, AngleOfAttack.maxValue);
        //calculate Position
        Vector2 pos = MathTools.PosInCircleEdge(angle, spawnCircleRadius, Vector2.zero);
        //instantiate
        GameObject o = Instantiate(RootPrefab, pos, Quaternion.identity, this.transform);
        //give the data
        o.GetComponent<RootAnimation>().rootData = rootData;
        RootList.Add(o);
    }

    public void RemoveRoot(RootAnimation rootElement)
    {
        if(RootList.Contains(rootElement.gameObject))
        {
            RootList.Remove(rootElement.gameObject);
        }else
        {
            Debug.Log("Cant removed a non existent Root u dummy");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RootList = new List<GameObject>();
    }

    public int RootCount()
    {
        return RootList.Count;
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero,spawnCircleRadius);
    }

}
