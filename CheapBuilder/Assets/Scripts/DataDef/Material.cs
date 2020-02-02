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
    public Material(MaterialType m, string n, float q, float c) { m_type = m; m_name = n; m_quality = q; m_cost = c; }
    protected Material() { }

    public static Material PickRandomMaterial(float value) //this is always returning Structural
    {
        Material m = new Material();
        MaterialType mtype = new MaterialType();

        float ChanceForLowQualityMats = Random.value* value; //random value has a chance to be 0. Better to have low housing getting high mats than high housing getting low mats

        if (ChanceForLowQualityMats > 75)
            mtype = mtype | MaterialType.Damaged;
        else if (ChanceForLowQualityMats > 25)
            mtype = mtype | MaterialType.NoBrand;
        else
            mtype = mtype | MaterialType.Branded;

        mtype |= (MaterialType)System.Math.Pow(2, Random.Range(0, Mathf.Log(2, (float)MaterialType.AllMaterials + 1))) ; 

        ListOfAllMaterials list = GameObject.FindObjectOfType<ListOfAllMaterials>();
        if (list == default)
            return null;
        return list.GetMaterialOfFlag(mtype);
    }
}
