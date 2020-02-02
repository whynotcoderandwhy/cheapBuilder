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

    //list of all inactive work orders

        //list of all buildings - assign this based on a list of transform points?




    public static float CurrentCash;
    public static float WorkHoursPerDay;
    public static float HourProductivity;
    public static float ManHourSurchange = 0.001f;
    public static float LogImpact = 4;
    public static float AdvancePercentage = 0.2f;
    public static float MaxFailure = 2;//before quest goes away
}

