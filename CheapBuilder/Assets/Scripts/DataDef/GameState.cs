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
    public static float CurrentCash;
    public static float WorkHoursPerDay;
    public static float HourProductivity;
    public static float ManHourSurchange = 0.001f;
    public static float LogImpact = 4;

}

