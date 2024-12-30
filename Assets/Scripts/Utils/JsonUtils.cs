using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class JsonUtils
{
    public static string ToJson<T>(T requestPacket)
    {
        return JsonConvert.SerializeObject(requestPacket);
    }

    public static T FromJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}
