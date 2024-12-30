using System.Collections.Generic;
using Newtonsoft.Json;

public class RequestPacket
{
        public string typeRequest;
        public AuthData authData;
        public PlayerInfo playerInfo;
        
        public RequestPacket()
        {
                
        }
        public RequestPacket(string typeRequest, AuthData authData)
        {
                this.typeRequest = typeRequest;
                this.authData = authData;
        }

        public RequestPacket(string typeRequest, PlayerInfo playerInfo)
        {
                this.typeRequest = typeRequest;
                this.playerInfo = playerInfo;
        }
}

public class RequestPacket1
{
        
        
        public PacketType packetType;
        public AbstractData abstractData;
        public UpdateStoreData updateStoreData;
        public string namePlayer;
        public string messagePlayer;
        public string dayPlay;
        public SoldierData soldierData;
        public bool isRegisterPlayer;
        public List<InfoPlayer> infoPlayers;
        
        public RequestPacket1()
        {
                
        }

        
        // // // ////////////////////////////////////////////////////////////////////
        public RequestPacket1(PacketType packetType)
        {
                this.packetType = packetType;
                this.abstractData = new AbstractData(true);
                this.updateStoreData = null;
        }

        public RequestPacket1(PacketType packetType, string namePlayer, bool isRegisterPlayer)
        {
                this.packetType = packetType;
                this.namePlayer = namePlayer;
                this.isRegisterPlayer = isRegisterPlayer;
        }
        public RequestPacket1(PacketType packetType, string namePlayer, string messagePlayer)
        {
                this.packetType = packetType;
                this.namePlayer = namePlayer;
                this.messagePlayer = messagePlayer;
        }

        public RequestPacket1(PacketType packetType, string dayPlay, SoldierData soldierData)
        {
                this.packetType = packetType;
                this.dayPlay = dayPlay;
                this.soldierData = soldierData;
        }
        public RequestPacket1(PacketType packetType, AbstractData abstractData)
        {
                this.packetType = packetType;
                this.abstractData = abstractData;
                this.updateStoreData = null;
        }
        
        public RequestPacket1(PacketType packetType, UpdateStoreData updateStoreData)
        {
                this.packetType = packetType;
                this.abstractData = null;
                this.updateStoreData = updateStoreData;
        }

        public RequestPacket1(PacketType packetType, string namePlayer)
        {
                this.packetType = packetType;
                this.namePlayer = namePlayer;
        }
        public static string toJson(RequestPacket1 requestPacket1)
        {
                return JsonConvert.SerializeObject(requestPacket1);
        }

        public static RequestPacket1 fromJson(string json)
        {
                return JsonConvert.DeserializeObject<RequestPacket1>(json);
        }
}