using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Material
{
    [System.Flags]
    public enum MaterialType
    {
        Structural  = 0x001,
        Guilding    = 0x002,
        Adhesive    = 0x004,
        FalseFront  = 0x008,
        Mechanical  = 0x010,
        Insulation  = 0x020,

        AllMaterials = Structural | Guilding | Adhesive | FalseFront | Mechanical | Insulation,

        Branded = 0x100,
        NoBrand = 0x200,
        Damaged = 0x400,

        AllQualities = Branded | NoBrand | Damaged,
    }

    [SerializeField]
    protected MaterialType m_type;
    public MaterialType MaterialFlags => m_type;
    [SerializeField]
    protected string m_name;
    public string Name => m_name;
    [SerializeField]
    protected float m_quality;
    public float Quality => m_quality;
    [SerializeField]
    protected float m_cost;
    public float Cost => m_cost;


    /// <summary>
    /// should restructure unit testing, but for now please do not use for anything other than unit testing
    /// </summary>
    public void TestingInit(MaterialType m, string n, float q, float c) { m_type = m; m_name = n; m_quality = q; m_cost = c; }


    public static Material GenerateRandomMaterial(float value)
    {
        Material m = new Material();
        m.m_type = new MaterialType();
        m.m_name = "";

        float ChanceForLowQualityMats = Random.value* value; //random value has a chance to be 0. Better to have low housing getting high mats than high housing getting low mats

        if (ChanceForLowQualityMats > 75)
        {
            m.m_type = m.m_type | MaterialType.Damaged;
            m.m_name += "Damaged ";
        }
        else if (ChanceForLowQualityMats > 25 && ChanceForLowQualityMats < 75)
        {
            m.m_type = m.m_type | MaterialType.NoBrand;
            m.m_name += "Standard ";
        }
        else
        {
            m.m_type = m.m_type | MaterialType.Branded;
            m.m_name += "Branded ";
        }
        m.m_quality = (100 - ChanceForLowQualityMats);
        m.m_cost = m.m_quality * (1 + value / 100.0f);

        switch (Random.Range(0, 5))
        {
            case 0:
                m.m_type = m.m_type | MaterialType.Structural;
                m.m_name += "Structural";
                break;
            case 1:
                m.m_type = m.m_type | MaterialType.Guilding;
                m.m_name += "Guilding";
                break;
            case 2:
                m.m_type = m.m_type | MaterialType.Adhesive;
                m.m_name += "Adhesive";
                break;
            case 3:
                m.m_type = m.m_type | MaterialType.FalseFront;
                m.m_name += "FalseFront";
                break;
            case 4:
                m.m_type = m.m_type | MaterialType.Mechanical;
                m.m_name += "Mechanical";
                break;
            case 5:
                m.m_type = m.m_type | MaterialType.Insulation;
                m.m_name += "Insulation";
                break;
        }



        return m;
    }
}
