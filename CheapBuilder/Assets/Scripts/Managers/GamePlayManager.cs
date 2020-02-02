using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GamePlayManager
{

    public static void GenerateStarterRepairs()
    {
        while(GameState.AvailibleJobs.Count < GameState.RepairInit)
        {
            GenerateRepairs();
        }
    }

    protected WorkerManager m_workerManager = new WorkerManager();

    public void IncrementDay()
    {
        GameState.GameDay++;
        GameState.CurrentCash -= m_workerManager.PayWorkers();
        GameState.CurrentCash -= GameState.ActiveJobs.Select(
            ActiveJob => ActiveJob.ResolveDayAndCalculateCost()
            ).Sum();
        ResolveExpiredJobs();
        GenerateRepairs();
    }

    public static void GenerateRepairs()
    {
        float repairDensity = (GameState.ActiveJobs.Count + GameState.AvailibleJobs.Count) / GameState.Cityscape.Count;
        if (Random.Range(0, 1 - repairDensity) < GameState.RepairThreshold)
            return;

        int targetedhome = Random.Range(0, GameState.Cityscape.Count - 1);
        while (GameState.Cityscape[targetedhome].HasWorkOrder)
            targetedhome = Random.Range(0, GameState.Cityscape.Count - 1);

        WorkOrder newWorkOrder = new WorkOrder();
        newWorkOrder.CreateInitialWorkOrderSpecs(GameState.Cityscape[targetedhome]);
        GameState.AvailibleJobs.Add(newWorkOrder);
    }


    public void ResolveExpiredJobs()
    {
        for(int i = GameState.ActiveJobs.Count -1; i >= 0; i--)
        {
            if (!GameState.ActiveJobs[i].WorkComplete)
                continue;
            ActiveJob jobCleanup = GameState.ActiveJobs[i];


            if (jobCleanup.WorkFailed)
            {
                //TODO: do more than just this
                if (jobCleanup.ResetProgress())
                    GameState.ActiveJobs.RemoveAt(i);
                continue;
            }
            GameState.CurrentCash += (1 - GameState.AdvancePercentage) * jobCleanup.JobWorth;
        }
    }

    public void RecruitWorkers() { }

    public void AcceptNewJob(WorkOrder JobRequest)
    {
        if (!GameState.AvailibleJobs.Remove(JobRequest))
            return;
        GameState.ActiveJobs.Add(new ActiveJob(JobRequest));
    }

}
