using Newtonsoft.Json;

public class RequestPacket
{
        public PacketType packetType;
        public AbstractData abstractData;
        public UpdateStoreData updateStoreData;
        
        public RequestPacket()
        {
                
        }
        public RequestPacket(PacketType packetType)
        {
                this.packetType = packetType;
                this.abstractData = new AbstractData(true);
                this.updateStoreData = null;
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
        public static string toJson(RequestPacket requestPacket)
        {
                return JsonConvert.SerializeObject(requestPacket);
        }

        public static RequestPacket fromJson(string json)
        {
                return JsonConvert.DeserializeObject<RequestPacket>(json);
        }
}