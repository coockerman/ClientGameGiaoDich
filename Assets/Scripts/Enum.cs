public enum PacketType
{
    Buy = 0, 
    Sell = 1, 
    ResponseBuy = 2, 
    ResponseSell = 3, 
    UpdateStore = 4, 
    ResponseUpdateStore = 5, 
    Bankrupt = 6,
    ResponseFindOpponent = 7,
    DayPlay = 8,
    ResponsePlayerCanAttack = 9,
    AttackPlayer = 10,
    ResponseInformationAfterDay = 11,
    ResponsePlayerAttack = 12,
    InformationPlayer = 13,
    ResponseResultAndResource = 14,
    NamePlayer = 15,
    ResponseNamePlayer = 16
}

public enum ItemType
{
    Gold = 0, 
    Iron = 1, 
    Food = 2
    
}

public enum SolierType
{
    Melee = 0,
    Arrow = 1,
    Cavalry = 2,    
    Citizen = 3
}

public enum TypeObj
{
    Item = 0,
    Solier = 1
}
public enum StateGamePlayer
{
    Idle = 0, 
    Waiting = 1, 
    Processing = 2
}