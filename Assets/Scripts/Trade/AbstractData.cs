public class AbstractData
{
       public bool isStatus;
       public ItemType itemType;
       public int count;
       public float price;

       public AbstractData()
       {
              
       }
       public AbstractData(bool isStatus)
       {
              this.isStatus = isStatus;
       }
       public AbstractData(bool isStatus, ItemType itemType, int count, float price)
       {
              this.isStatus = isStatus;
              this.itemType = itemType;
              this.count = count;
              this.price = price;
       }
}