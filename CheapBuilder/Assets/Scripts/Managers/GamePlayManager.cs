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
        ResolveExpiredJobs();
    }

    public void ResolveExpiredJobs()
    {
        for(int i = GameState.ActiveJobs.Count -1; i >= 0; i--)
        {
            if (!GameState.ActiveJobs[i].WorkComplete)
                continue;
            ActiveJob jobCleanup = GameState.ActiveJobs[i];
            GameState.ActiveJobs.RemoveAt(i);

            if (jobCleanup.WorkFailed)
            {
                //TODO: do more than just this
                jobCleanup.ResetProgress();
                continue;
            }
            GameState.CurrentCash += (1 - GameState.AdvancePercentage) * jobCleanup.JobWorth;
        }
    }

    public void RecruitWorkers() { }

    public void AcceptNewJob(Building JobRequester) { }

    public void ResolveWorkOrder()
    {
        
    }
}
