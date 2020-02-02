using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class City : MonoBehaviour
{
    protected List<CityLot> m_lots;
    public List<CityLot> CityLots => m_lots;


    [SerializeField]
    protected List<GameObject> m_allHousePrefabs; // if we were ambitious, prefabs would have values and such, but for now they're all the same


    [SerializeField]
    protected GameObject[] m_cityPoints; //this should be generated instead of set



    private void CitySetup()
    {
        m_cityPoints = GameObject.FindGameObjectsWithTag("GridSpot");



        int num = m_allHousePrefabs.Count;
        foreach(GameObject pt in m_cityPoints)
        {
            int h = UnityEngine.Random.Range(0, num - 1);
            GameObject hp = Instantiate(m_allHousePrefabs[h]) as GameObject;
            //hp.transform.SetPositionAndRotation(pt.transform.position, Quaternion.LookRotation( ((h > (num / 2))? Vector3.forward: Vector3.right), Vector3.up));

            hp.transform.position = pt.transform.position;

            Vector2 rv = UnityEngine.Random.insideUnitCircle;
            Vector3 rv2 = Vector3.zero;
            rv2.x = rv.x;
            //rv2.y

            //hp.transform.LookAt();

            Building b = new Building();
            b.Init(UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100)); //building values
            CityLot cl = new CityLot(b, hp.transform.position, Vector3.one);
            GameState.Cityscape.Add(b);
            m_lots.Add(cl);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        GameState.m_city = this;
        m_lots = new List<CityLot>();
        CitySetup();

        GamePlayManager.GenerateStarterRepairs();
    }

    // Update is called once per frame
    void Update()
    {

    }
} 
