
public class SoldierData 
{
    public float Melee;
    public float Arrow;
    public float Cavalry;
    public SoldierData() {}

    public SoldierData(float melee, float arrow, float cavalry) {
        Melee = melee;
        Arrow = arrow;
        Cavalry = cavalry;
    }

    public float getMelee() {
        return Melee;
    }

    public void setMelee(float melee) {
        Melee = melee;
    }

    public float getArrow() {
        return Arrow;
    }

    public void setArrow(float arrow) {
        Arrow = arrow;
    }

    public float getCavalry() {
        return Cavalry;
    }

    public void setCavalry(float cavalry) {
        Cavalry = cavalry;
    }
}
