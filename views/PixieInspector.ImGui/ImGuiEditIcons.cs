using Hexa.NET.ImGui;

namespace PixieInspector.ImGui;

// Textures are uploaded and owned by the host's renderer, never by the Inspector.
public sealed record ImGuiEditIcons(ImTextureRef Lock, ImTextureRef Chain);
