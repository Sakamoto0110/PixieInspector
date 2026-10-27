namespace PixieInspector.Options;

// A mask in the member's storage width, with a host-supplied label. Zero explicitly clears
// the value; a composite sets/clears its bits together. Aliases may have different labels.
public sealed record FlagChoice(ulong Mask, string Label);
