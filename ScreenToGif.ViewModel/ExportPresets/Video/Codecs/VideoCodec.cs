using System.Runtime.Serialization;
using ScreenToGif.Domain.Enums;
using ScreenToGif.Domain.ViewModels;

namespace ScreenToGif.ViewModel.ExportPresets.Video.Codecs;

public class VideoCodec : BindableBase
{
    [IgnoreDataMember]
    public VideoCodecs Type { get; protected init; }

    [IgnoreDataMember]
    public string Name { get; protected init; }

    [IgnoreDataMember]
    public string Command { get; protected init; }

    [IgnoreDataMember]
    public string Parameters { get; protected init; }

    [IgnoreDataMember]
    public bool IsHardwareAccelerated { get; protected init; }
        
    [IgnoreDataMember]
    public bool CanSetCrf { get; protected init; }

    [IgnoreDataMember]
    public int MinimumCrf { get; protected init; }

    [IgnoreDataMember]
    public int MaximumCrf { get; protected init; }

    [IgnoreDataMember]
    public List<EnumItem<VideoCodecPresets>> CodecPresets { get; protected init; }

    [IgnoreDataMember]
    public List<EnumItem<VideoPixelFormats>> PixelFormats { get; protected init; }
}

public class EnumItem<T> where T : System.Enum
{
    public T Type { get; set; }

    public string NameKey { get; set; }

    public string Name { get; set; }

    public string Parameter { get; set; }

    public EnumItem()
    { }

    public EnumItem(T type, string nameKey, string name, string parameter)
    {
        Type = type;
        NameKey = nameKey;
        Name = name;
        Parameter = parameter;
    }

    public EnumItem(T type, string nameKey, string parameter)
    {
        Type = type;
        NameKey = nameKey;
        Parameter = parameter;
    }
}