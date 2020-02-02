using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GamePlayManager
{
    protected WorkerManager m_workerManager = new WorkerManager();

    public void IncrementDay()
    {
        GameState.GameDay++;
        GameState.CurrentCash -= m_workerManager.PayWorkers();
        GameState.CurrentCash -= GameState.ActiveJobs.Select(
            ActiveJob => ActiveJob.ResolveDayAndCalculateCost()).Sum();
    }
    public void RecruitWorkers() { }

    public void AcceptNewJob(Building JobRequester) { }

    public void ResolveWorkOrder()
    {


    }
}
