using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductOrder 
{
    public float m_quantity;
    public bool m_quantityLocked = false;
    public Material m_material;

    public bool SplitThisOrder( out ProductOrder newProduct)
    {
        newProduct = default;
        if (m_quantity < 2)
            return false;

        newProduct = new ProductOrder();
        newProduct.m_material = this.m_material;
        newProduct.m_quantityLocked = this.m_quantityLocked;

        newProduct.m_quantity = Mathf.Floor(this.m_quantity / 2.0f);
        this.m_quantity = Mathf.Ceil(this.m_quantity / 2.0f);
        return true;
    }




}
