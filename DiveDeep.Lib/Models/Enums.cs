using System.Text.Json.Serialization;

namespace DiveDeep.Lib.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter<ProductCategory>))]
    public enum ProductCategory
    {
        //Ved ikke om det er tanken
        BCD,
        DiveSuit,
        Fins,
        MaskSnorkel,
        RegulatorSet,
        Tank // nej dette er tanken
    }

    [JsonConverter(typeof(JsonStringEnumConverter<Size>))]
    public enum Size
    {
        XtraSmall,
        Small,
        Medium,
        Large,
        XtraLarge
    }

    [JsonConverter(typeof(JsonStringEnumConverter<SuitType>))]
    public enum SuitType
    {
        Wetsuit,
        Drysuit
    }
}
