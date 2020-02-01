
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class WorkerManager 
{


    protected List<Worker> m_activeWorkers = new List<Worker>();
    protected List<Worker> m_workerPool = new List<Worker>();
    public List<Worker> GenerateHigheringPool(float hourlyWage, float desiredQuality)
    {
        return new List<Worker>()
        {
            Worker.NewWorker(hourlyWage,desiredQuality),
            Worker.NewWorker(hourlyWage,desiredQuality),
            Worker.NewWorker(hourlyWage,desiredQuality)
        };

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="workee">the worker to switch</param>
    /// <param name="Inuse">the worker </param>
    /// <returns>if the operation was succesful</returns>
    public bool SwitchWorker(Worker workee, bool Inuse)
    {
        List<Worker> source = (Inuse) ? m_workerPool : m_activeWorkers;
        List<Worker> destination = (!Inuse) ? m_workerPool : m_activeWorkers;

        if ((source.IndexOf(workee) < 0)|| destination.IndexOf(workee) > -1)
            return false;

        source.Remove(workee);
        destination.Add(workee);

        return true;
    }


    public void NewWorker(Worker workee)
    {
        m_workerPool.Add(workee);
    }

    public void FireWorker(Worker workee)
    {
        if (m_activeWorkers.Remove(workee))
            return;
        if (m_workerPool.Remove(workee))
            return;
    }

    protected bool FoundandRemoved (List<Worker> list, Worker workee)
    {
        return list.Remove(workee);
    }

    public bool isWorking(Worker workee)
    {
        return m_activeWorkers.IndexOf(workee) > -1;
    }
}
