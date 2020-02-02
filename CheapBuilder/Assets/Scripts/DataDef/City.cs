using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


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
            hp.transform.position = pt.transform.position;
            
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        GameState.m_city = this;
        CitySetup();
    }

    // Update is called once per frame
    void Update()
    {

    }
} 
