using UnityEngine;
using System.Collections.Generic;

public class CoinManager : MonoBehaviour
{
    private class Point
    {
        private Vector3 positionValue;
        private bool haveCoinValue = false;

        public Point(Vector3 pos)
        {
            positionValue = pos;
        }

        public Vector3 Position { get { return positionValue;}}
        public bool HaveCoin { get { return haveCoinValue;} set { haveCoinValue = value;}}
    }
    private List<GameObject> points = new List<GameObject>();
    public GameObject coinPref;
    public GameObject obj;
    private Vector3 height = new Vector3(0f, 0.5f, 0f);
    void Start()
    {
        //int count = 2;
        int index;

        //points.AddRange(GameObject.FindGameObjectsWithTag("Point"));
        //points.AddRange(GameObject.Find("arena").transform.gameObject.)

        Transform parent = GameObject.Find("arena").transform;

        foreach (Transform child in parent)
        {
            if (child.CompareTag("Point"))
            {
                points.Add(child.gameObject);
            }
        }
        
        /*
        points.Add(new Point(new Vector3(-2.53f, 0.3f, 2.35f)));
        points.Add(new Point(new Vector3(-2.53f, 0.3f, -3.2f)));
        points.Add(new Point(new Vector3(3.82f, 0.3f, -3.2f)));
        points.Add(new Point(new Vector3(2.45f, 0.3f, 1.91f)));
        */

        for (int count = 2; count > 0; count--)
        {
            index = Random.Range(0, points.Count);
            Instantiate(coinPref, points[index].transform.position + height, Quaternion.AngleAxis(Random.Range(-180, 180), Vector3.up));    
            points.RemoveAt(index);
            //Quaternion()
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
