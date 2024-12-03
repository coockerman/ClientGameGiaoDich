public enum PacketType
{
    Buy = 0, 
    Sell = 1, 
    ResponseBuy = 2, 
    ResponseSell = 3, 
    UpdateStore = 4, 
    ResponseUpdateStore = 5, 
    Bankrupt = 6
}

public enum ItemType
{
    Gold = 0, 
    Iron = 1, 
    Food = 2
}

public enum StateGamePlayer
{
    Idle = 0, 
    Waiting = 1, 
    Processing = 2
}