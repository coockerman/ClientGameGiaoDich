
public class UpdateStoreData 
{
    public Item itemGold;
    public Item itemIron;
    public Item itemFood;

    public UpdateStoreData()
    {
        
    }
    
    public UpdateStoreData(Item itemGold, Item itemIron, Item itemFood)
    {
        this.itemGold = itemGold;
        this.itemIron = itemIron;
        this.itemFood = itemFood;
    }
    
    public override string ToString()
    {
        return $"UpdateStoreData{{itemGold={itemGold}, itemIron={itemIron}, itemFood={itemFood}}}";
    }
}