using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemoApp.Core.Constants
{
    public class MyEnum
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum Order
        {
            Shipped = 3,
            ReadyToShip = 1,
            InProgress = 2,
            Done = 4,
            Canceled = 0
        }
        [JsonConverter(typeof(StringEnumConverter))]
        public enum Product
        {
            InStock = 0,
            OutStock = 1,
            ComingSoon = 2
        }
    }
}
