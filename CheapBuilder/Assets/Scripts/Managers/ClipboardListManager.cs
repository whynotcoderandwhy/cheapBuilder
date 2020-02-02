using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class ClipboardListManager : MonoBehaviour
{
    [SerializeField]
    protected GameObject m_dropdownItemPrefab;

    protected int m_currentWorkOrderViewed;
    protected List<List<GameObject>> m_dropdownListGroups;





    protected bool AddItemToList(ProductOrder po)
    {
        GameObject g = Instantiate(m_dropdownItemPrefab) as GameObject;
        if (g == default)
        {
            return false;
        }
        else
        {
            g.SetActive(true);
            g.transform.SetParent(gameObject.transform, false);

            if(m_dropdownListGroups==default) //needed to start
            {
                m_dropdownListGroups = new List<List<GameObject>>();
                m_dropdownListGroups.Add(new List<GameObject>());
                m_currentWorkOrderViewed = 0;
            }

            m_dropdownListGroups[m_currentWorkOrderViewed].Add(g);

            MaterialSlotPrefab prefabsetting = g.GetComponent<MaterialSlotPrefab>();
            prefabsetting.Init(po);

            return true;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //GameState.ActiveJobs
    }

    // Update is called once per frame
    void Update()
    {
    }
}
