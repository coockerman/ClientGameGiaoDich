
public class Item
{
    public ItemType itemType;
    public float priceBuy;
    public float priceSell;
    public int countInStock;
    
    public Item() {}
    
    public Item(ItemType itemType, float priceBuy, float priceSell, int countInStock)
    {
        this.itemType = itemType;
        this.priceBuy = priceBuy;
        this.priceSell = priceSell;
        this.countInStock = countInStock;
    }

    public override string ToString()
    {
        return $"Item{{itemType={itemType}, priceBuy={priceBuy}, priceSell={priceSell}, countInStock={countInStock}}}";
    }
}