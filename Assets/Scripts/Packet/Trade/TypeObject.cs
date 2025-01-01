public class TypeObject
{
    public const string FOOD = "food";
    public const string IRON = "iron";
    public const string GOLD = "gold";
    public const string MELEE = "melee";
    public const string ARROW = "arrow";
    public const string CAVALRY = "cavalry";
    public const string CITIZEN = "citizen";

    public static string EnumToString(ItemType type)
    {
        if(type == ItemType.Food) return FOOD;
        else if(type == ItemType.Iron) return IRON;
        else if(type == ItemType.Gold) return GOLD;
        else if(type == ItemType.Melee) return MELEE;
        else if(type == ItemType.Arrow) return ARROW;
        else if(type == ItemType.Cavalry) return CAVALRY;
        else if (type == ItemType.Citizen) return CITIZEN;
        else return null;
    }

    public static ItemType StringToEnum(string type)
    {
        if(type == FOOD) return ItemType.Food;
        else if(type == IRON) return ItemType.Iron;
        else if(type == GOLD) return ItemType.Gold;
        else if(type == MELEE) return ItemType.Melee;
        else if(type == ARROW) return ItemType.Arrow;
        else if(type == CAVALRY) return ItemType.Cavalry;
        else if(type == CITIZEN) return ItemType.Citizen;
        else return 0;
    }
}