using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSystem : MonoBehaviour
{
    public GameObject RootPrefab;
    public float spawnCircleRadius = 10;

    public List<GameObject> RootList;

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

    // Start is called before the first frame update
    void Start()
    {
        RootList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
