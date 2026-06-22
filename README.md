# Masonry

A fluid, "Windows 8 tile"–style layout grid that arranges variable-sized children into a tightly packed, gap-filling layout — like the CSS/JavaScript Masonry libraries, but native for Windows desktop.

Two implementations are provided from the same packing algorithm:

| Technology | Package | Docs |
|---|---|---|
| **WPF** (.NET 10 & .NET Framework 4.6.2) | [`wpfmasonry`](https://www.nuget.org/packages/wpfmasonry) | [Masonry/readme.md](Masonry/readme.md) |
| **WinUI 3** (Windows App SDK, .NET 10) | [`LoDaTek.MasonryWinUI`](https://www.nuget.org/packages/LoDaTek.MasonryWinUI) | [MasonryWinUI/readme.md](MasonryWinUI/readme.md) |

## How it works

`MasonryControl` is an `ItemsControl`. As children are added, removed, or resized, it computes each child's position by tracking the open horizontal spans (a free-space "matrix") and dropping each child into the shallowest span wide enough to hold it. Positioning is done via `Margin`, so children are left/top aligned within the control.

The WPF version also offers `AnimatedMasonryControl`, which animates tiles gliding to their new positions instead of snapping.

## Install

WPF:

```powershell
dotnet add package wpfmasonry
```

WinUI:

```powershell
dotnet add package LoDaTek.MasonryWinUI
```

## Quick start

WPF:

```xml
<masonry:MasonryControl xmlns:masonry="clr-namespace:Masonry;assembly=Masonry"
                        ItemsSource="{Binding Elements}" Spacing="5" />
```

WinUI:

```xml
<masonry:MasonryControl xmlns:masonry="using:MasonryWinUI" Spacing="5">
    <masonry:Tile Title="Projects" Background="Teal" Width="300" Height="125" />
</masonry:MasonryControl>
```

See the per-technology docs above for full usage, and the `Masonry.Example` / `MasonryWinUI.Example` projects for runnable samples.

## Building

The solution is `Masonry.slnx` (requires the .NET 10 SDK on Windows; the WinUI projects also need the Windows App SDK workload).

```powershell
dotnet build Masonry.slnx -c Release
```

## License

WPF library originally created by Nikita Bernthaler (2013–2016). See [LICENSE](LICENSE) for terms.
