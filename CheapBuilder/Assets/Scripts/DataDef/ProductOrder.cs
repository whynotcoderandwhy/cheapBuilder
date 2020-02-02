using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductOrder 
{
    protected uint m_quantity;
    public uint Quantity => m_quantity;
    protected bool m_quantityLocked;
    public bool IsLocked => m_quantityLocked;
    protected Material m_material;
    public Material Material => m_material;

    public bool SplitThisOrder( out ProductOrder newProduct)
    {
        newProduct = default;
        if (m_quantity < 2)
            return false;

        newProduct = new ProductOrder();
        newProduct.m_material = this.m_material;
        newProduct.m_quantityLocked = this.m_quantityLocked;

        newProduct.m_quantity = (uint) Mathf.Floor(this.m_quantity / 2.0f);
        this.m_quantity = (uint) Mathf.Ceil(this.m_quantity / 2.0f);
        return true;
    }
    protected ProductOrder() { }

    public bool UpdateQuanity(uint newQuanity, bool force = false)
    {
        if (IsLocked && !force)
            return false;
        m_quantity = newQuanity;
        return true;
    }

    public void SetLock(bool lockValue)
    {
        m_quantityLocked = lockValue;
    }


    public ProductOrder(Material material, uint quantity)
    {
        m_quantity = quantity;
        m_material = material;
    }
    public ProductOrder(Building sourceBuilding)
    {
        m_quantity = (uint) Random.Range(1, Mathf.Log(GameState.LogImpact, sourceBuilding.Value));
        m_material = Material.PickRandomMaterial(sourceBuilding.Value);
    }


}
