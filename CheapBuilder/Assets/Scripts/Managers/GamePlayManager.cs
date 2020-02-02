using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager
{
    protected WorkerManager m_workerManager = new WorkerManager();

    public void IncrementDay()
    {
        GameState.GameDay++;
        GameState.CurrentCash -= m_workerManager.PayWorkers();

    }
    public void RecruitWorkers() { }

    public void AcceptNewJob(Building JobRequester) { }

    public void ResolveWorkOrder()
    {


    }
}
