using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsePacket
{
    public string typeResponse;
    public string callbackResult;

    public ResponsePacket()
    {
        
    }

    public ResponsePacket(string typeResponse, string callbackResult)
    {
        this.typeResponse = typeResponse;
        this.callbackResult = callbackResult;
    }
}
