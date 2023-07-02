#nullable enable

namespace ScreenToGif.ViewModel.ExportPresets.Video.Codecs;
/// <summary>
/// Group of video codecs grouped by (similar) video coding format(s)
/// </summary>
public sealed class VideoCodecGroup
{
    private static readonly IEnumerable<VideoCodec> emptyCodecs = Array.Empty<VideoCodec>();

    /// <summary>
    /// Codecs for H.264/AVC
    /// </summary>
    public static readonly VideoCodecGroup H264 = new(new VideoCodec[] { new X264() }, new VideoCodec[] { new H264Amf(), new H264Nvenc(), new H264Qsv() });
    /// <summary>
    /// Codecs for H.265/HEVC
    /// </summary>
    public static readonly VideoCodecGroup H265 = new(new VideoCodec[] { new X265() }, new VideoCodec[] { new HevcAmf(), new HevcNvenc(), new HevcQsv() });
    /// <summary>
    /// Codecs for VP8-9
    /// </summary>
    public static readonly VideoCodecGroup VPX = new(new VideoCodec[] { new Vp8(), new Vp9() });
    /// <summary>
    /// Codecs for AV1
    /// </summary>
    public static readonly VideoCodecGroup AV1 = new(new VideoCodec[] { new LibAom(), new SvtAv1(), new Rav1E() });
    /// <summary>
    /// Codecs for MPEG-2/4
    /// </summary>
    public static readonly VideoCodecGroup Mpeg = new(new VideoCodec[] { new Mpeg2(), new Mpeg4() });

    private readonly IEnumerable<VideoCodec> softwareCodecs;
    private readonly IEnumerable<VideoCodec> hardwareCodecs;

    private VideoCodecGroup(IEnumerable<VideoCodec> softwareCodecs, IEnumerable<VideoCodec>? hardwareCodecs = null)
    {
        this.softwareCodecs = softwareCodecs;
        this.hardwareCodecs = hardwareCodecs ?? emptyCodecs;
    }
    private IEnumerable<VideoCodec> OnlySoftwareAddedTo(IEnumerable<VideoCodec> before)
    {
        return before.Concat(softwareCodecs);
    }
    private IEnumerable<VideoCodec> SoftAndHardAddedTo(IEnumerable<VideoCodec> before)
    {
        return before.Concat(softwareCodecs.Concat(hardwareCodecs));
    }
    /// <summary>
    /// Gets a enumerable of individual video codecs from groups and whether hardware codecs are used
    /// </summary>
    /// <param name="groups">An array or list of codec gropus</param>
    /// <param name="enablesHardwareCodecs"><c>true</c> if hardware codecs must be added</param>
    /// <returns>Enumerable of <c>VideoCodec</c>'s</returns>
    public static IEnumerable<VideoCodec> ToIndividuals(IEnumerable<VideoCodecGroup> groups, bool enablesHardwareCodecs)
    {
        return groups.Aggregate(
            Enumerable.Empty<VideoCodec>(),
            enablesHardwareCodecs
                ? ((before, group) => group.SoftAndHardAddedTo(before))
                : ((before, group) => group.OnlySoftwareAddedTo(before))
        );
    }
}
