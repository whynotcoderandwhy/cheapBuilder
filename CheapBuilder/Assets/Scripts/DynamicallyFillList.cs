using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this class exists to instantiate a list of prefabs into a scrollable list
/// </summary>
public class DynamicallyFillList : MonoBehaviour
{

    [SerializeField]
    protected List<GameObject> m_prefabs;

    // Start is called before the first frame update
    void Start()
    {
        int count = 0;
        foreach (GameObject item in m_prefabs) //loop through list
        {
            GameObject g = Instantiate(item) as GameObject;
            if (g == default)
            {
                Debug.LogError("failed to instantiate");
            }
            else
            {
                g.SetActive(true);
                g.transform.SetParent(gameObject.transform, false);
            }
            count++;
        }
    }

}
