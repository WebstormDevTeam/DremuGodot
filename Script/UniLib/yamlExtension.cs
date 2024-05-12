using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DremuGodot.Script.UniLib;

public partial class yamlExtension: Node2D
{
    public static void Test()
    {
        GD.Print(yamlExtension.ConvertJsonToYaml("{\"Name\": \"Hallo\"}"));
    }
    
    public static string ConvertYamlToJson(string yaml)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        // 反序列化YAML字符串为动态对象
        var yamlObject = deserializer.Deserialize<object>(yaml);

        // 序列化动态对象为JSON字符串
        var json = JsonConvert.SerializeObject(yamlObject, Formatting.Indented);

        return json;
    }
    
    public static string ConvertJsonToYaml(string json)
    {
        // 反序列化JSON字符串为Dictionary对象
        var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

        var serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        // 序列化Dictionary对象为YAML字符串
        var yaml = serializer.Serialize(jsonObject);

        return yaml;
    }
 
}