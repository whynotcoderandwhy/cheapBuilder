using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Worker
{
    public static Worker NewWorker(float hourlyWage, float desiredQuality)
    {
        return new Worker()
        {
            m_name = System.Guid.NewGuid().ToString(),
            m_hourlyRate = hourlyWage,
            m_qualityModifier = Random.Range( Mathf.Min(hourlyWage * GameState.CompetenceRatio, desiredQuality), hourlyWage * GameState.CompetenceRatio * GameState.LuckModifier)
        };
    }

    protected string m_name;
    public string Name => m_name;
    protected float m_qualityModifier;
    public float QualityModifier => m_qualityModifier;
    protected float m_hourlyRate;
    public float HourlyRate => m_hourlyRate;
}

