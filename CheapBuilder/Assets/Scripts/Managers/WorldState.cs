using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldState : MonoBehaviour
{
    protected static WorldState m_instance;
    public static WorldState Instance => m_instance;

    [SerializeField]
    protected int m_days = 0;
    public int Day => m_days;


    [SerializeField]
    protected float m_howMuchAreWePayingWorkersPerHour = 15.0f;
    public float WorkerWages => m_howMuchAreWePayingWorkersPerHour;


    [SerializeField]
    protected float m_howManyMaterialsPerDayShouldWeExpectWorkersToBeAbleToInstall = 24.0f; //1 unit/hour, why not
    public float DailyPersonalMaterialConsumption => m_howManyMaterialsPerDayShouldWeExpectWorkersToBeAbleToInstall;





    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(gameObject);
        m_instance = this;
    }

    public void IncrementTime()
    {
        m_days++;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
