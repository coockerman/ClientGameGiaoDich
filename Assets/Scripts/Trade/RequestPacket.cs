using System.Collections.Generic;
using Newtonsoft.Json;

public class RequestPacket
{
        public PacketType packetType;
        public AbstractData abstractData;
        public UpdateStoreData updateStoreData;
        public string namePlayer;
        public string messagePlayer;
        public float dayPlay;
        public SoldierData soldierData;
        public bool isRegisterPlayer;
        public List<InfoPlayer> infoPlayers;
        
        public RequestPacket()
        {
                
        }
        public RequestPacket(PacketType packetType)
        {
                this.packetType = packetType;
                this.abstractData = new AbstractData(true);
                this.updateStoreData = null;
        }

        public RequestPacket(PacketType packetType, string namePlayer, bool isRegisterPlayer)
        {
                this.packetType = packetType;
                this.namePlayer = namePlayer;
                this.isRegisterPlayer = isRegisterPlayer;
        }
        public RequestPacket(PacketType packetType, string namePlayer, string messagePlayer)
        {
                this.packetType = packetType;
                this.namePlayer = namePlayer;
                this.messagePlayer = messagePlayer;
        }

        public RequestPacket(PacketType packetType, float dayPlay, SoldierData soldierData)
        {
                this.packetType = packetType;
                this.dayPlay = dayPlay;
                this.soldierData = soldierData;
        }
        public RequestPacket(PacketType packetType, AbstractData abstractData)
        {
                this.packetType = packetType;
                this.abstractData = abstractData;
                this.updateStoreData = null;
        }
        
        public RequestPacket(PacketType packetType, UpdateStoreData updateStoreData)
        {
                this.packetType = packetType;
                this.abstractData = null;
                this.updateStoreData = updateStoreData;
        }

        public RequestPacket(PacketType packetType, string namePlayer)
        {
                this.packetType = packetType;
                this.namePlayer = namePlayer;
        }
        public static string toJson(RequestPacket requestPacket)
        {
                return JsonConvert.SerializeObject(requestPacket);
        }

        public static RequestPacket fromJson(string json)
        {
                return JsonConvert.DeserializeObject<RequestPacket>(json);
        }
}