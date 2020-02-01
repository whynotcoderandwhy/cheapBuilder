using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material
{
    [System.Flags]
    public enum MaterialType
    {
        Structural = 0x001,
        Guilding = 0x002,
        Adhesive = 0x004,
        FalseFront = 0x008,
        Mechanical = 0x010,
        Insulation = 0x020,

        AllMaterials = Structural | Guilding | Adhesive | FalseFront | Mechanical | Insulation,

        Branded = 0x100,
        NoBrand = 0x200,
        Damaged = 0x400,

        AllQualities = Branded | NoBrand | Damaged,
    }

    protected MaterialType m_type;
    public MaterialType MaterialFlags => m_type;
    protected string m_name;
    protected float m_quality;
    protected float m_cost;
}
