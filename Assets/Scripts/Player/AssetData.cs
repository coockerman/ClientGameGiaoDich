


public class AssetData
{
    public int countMoney;
    public int countFood;
    public int countIron;
    public int countGold;
    public int countMelee;
    public int countArrow;
    public int countCavalry;
    public int countCitizen;

    public AssetData()
    {
        
    }
    public AssetData(AssetData assetData)
    {
        this.countMoney = assetData.countMoney;
        this.countFood = assetData.countFood;
        this.countIron = assetData.countIron;
        this.countGold = assetData.countGold;
        this.countMelee = assetData.countMelee;
        this.countArrow = assetData.countArrow;
        this.countCavalry = assetData.countCavalry;
        this.countCitizen = assetData.countCitizen;
    }
    public AssetData(int countMoney, int countFood, int countIron, int countGold, int countMelee, int countArrow, int countCavalry, int countCitizen)
    {
        this.countMoney = countMoney;
        this.countFood = countFood;
        this.countIron = countIron;
        this.countGold = countGold;
        this.countMelee = countMelee;
        this.countArrow = countArrow;
        this.countCavalry = countCavalry;
        this.countCitizen = countCitizen;
    }
}