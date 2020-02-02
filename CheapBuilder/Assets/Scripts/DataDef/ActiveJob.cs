using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ActiveJob
{
    protected WorkOrder m_workOrder;
    protected List<Worker> m_workers;

    public float ResolveDayAndCalculateCost()
    {
        return m_workOrder.ResolveDayAndCalculateCost(m_workers);
    }

    public bool WorkComplete => m_workOrder.WorkComplete;
}

