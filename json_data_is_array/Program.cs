using System;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace json_data_is_array
{
    class Program
    {
        static void Main(string[] args)
        {
            var myClass = JsonSerializer.Deserialize<MyClass>(jsondoc);
            Console.WriteLine($"{myClass.StringValue} {myClass.LongValue} {string.Join(",", myClass.StringArrayValue)}");

            Console.WriteLine($"\n{JsonSerializer.Serialize(myClass)}");
        }
        class MyClass
        {
            [JsonIgnore]
            public string StringValue
            {
                get => StringValueBackingStore.FirstOrDefault();
                set
                {
                    if(StringValueBackingStore.Length == 1)
                    {
                        StringValueBackingStore[0] = value;
                    }
                    else
                    {
                        Debug.Assert(false, "Expecting array length of 1");
                    }
                }
            }
            public string[] StringArrayValue { get; set; }

            [JsonIgnore]
            public long LongValue
            {
                get => LongValueBackingStore.FirstOrDefault();
                set
                {
                    if (LongValueBackingStore.Length == 1)
                    {
                        LongValueBackingStore[0] = value;
                    }
                    else
                    {
                        Debug.Assert(false, "Expecting array length of 1");
                    }
                }
            }

            [JsonPropertyName("LongValue")]
            public long[] LongValueBackingStore { get; set; } = new long[1];

            [JsonPropertyName("StringValue")]
            public string[] StringValueBackingStore { get; set; } = new string[1];
        }

        const string jsondoc =
@"{
    ""StringValue"": [""mystringvalue""],
    ""StringArrayValue"": [""value1"",""value2""],
    ""LongValue"": [123]
}";
    }
}
