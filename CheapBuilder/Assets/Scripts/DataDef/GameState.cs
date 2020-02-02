using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameState
{
    public static int GameDay;
    public static int CompletetionTolerance;
    public static float CompetenceRatio;
    public static float LuckModifier;
    public static List<ActiveJob> ActiveJobs;
    public static List<WorkOrder> AvailibleJobs;
    public static List<Building> Cityscape;
    public static float CurrentCash = 15000;
    public static float WorkHoursPerDay  = 8;
    public static float HourProductivity = 3;
    public static float ManHourSurchange = 0.001f;
    public static float LogImpact = 4;
    public static float AdvancePercentage = 0.2f;
    public static float MaxFailure = 2;
    public static float RepairThreshold = 3;
}

