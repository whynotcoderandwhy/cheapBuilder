using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ActiveJob
{
    protected WorkOrder m_workOrder;
    protected List<Worker> m_workers;
    public float ResolveDayCost()
    {
        return CalculateResouceCostDay() + CalcuateWorkerCostDay();
    }

    public void ResolveDay()
    {
        m_workOrder.ResolveDay(m_workers);
    }

    public bool WorkComplete => m_workOrder.WorkComplete;



    /// <summary>
    /// should calcuate how much resorces (by cost) are consumed for a day
    /// </summary>
    /// <returns></returns>
    protected float CalculateResouceCostDay()
    {
        return 100;
    }

    /// <summary>
    /// should calcuate how many hours each person worked and calcuate a total from that
    /// </summary>
    /// <returns>Total cost for workers</returns>
    protected float CalcuateWorkerCostDay()
    {
        return 100;
    }
}

