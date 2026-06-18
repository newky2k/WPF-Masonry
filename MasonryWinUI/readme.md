# Masonry for WinUI

A fluid, "Windows 8 tile"–style layout grid for **WinUI 3 / Windows App SDK**. Arranges variable-sized children into a tightly packed, gap-filling layout that reflows as items are added, removed, or resized.

Targets .NET 10 (`net10.0-windows10.0.19041.0`). Requires the Windows App SDK.

## Install

```powershell
dotnet add package LoDaTek.MasonryWinUI
```

## Usage

Add the namespace and drop a `MasonryControl` into your XAML. It is an `ItemsControl`, so populate it with child `FrameworkElement`s directly or via `ItemsSource`.

```xml
<Page xmlns:masonry="using:MasonryWinUI">
    <masonry:MasonryControl Spacing="5" MaxWidth="850">
        <Border Width="300" Height="125" Background="Teal" />
        <Border Width="147" Height="125" Background="BlueViolet" />
        <Border Width="300" Height="255" Background="Green" />
    </masonry:MasonryControl>
</Page>
```

> Give each child an explicit `Width`/`Height` (or size-determining content) so the packer can measure it.

## Tiles

The package ships a `Tile` control — a `Button` subclass styled as a live-tile, with a title, optional count, and content area. Define styles for your tile sizes and reuse them:

```xml
<Page xmlns:masonry="using:MasonryWinUI">
    <Page.Resources>
        <Style x:Key="LargeTileStyle" TargetType="masonry:Tile">
            <Setter Property="Width" Value="300" />
            <Setter Property="Height" Value="125" />
            <Setter Property="Foreground" Value="White" />
            <Setter Property="TitleFontSize" Value="12" />
        </Style>
    </Page.Resources>

    <masonry:MasonryControl Spacing="5">
        <masonry:Tile Title="Projects"
                      Background="Teal"
                      Style="{StaticResource LargeTileStyle}"
                      HorizontalTitleAlignment="Right">
            <!-- any content: icon, image, etc. -->
            <SymbolIcon Symbol="Folder" />
        </masonry:Tile>
    </masonry:MasonryControl>
</Page>
```

### `Tile` properties

| Property | Type | Default | Description |
|---|---|---|---|
| `Title` | `string` | — | Tile caption. |
| `Count` | `string` | — | Optional secondary value (e.g. a badge number). |
| `HorizontalTitleAlignment` | `HorizontalAlignment` | `Left` | Horizontal placement of the title. |
| `VerticalTitleAlignment` | `VerticalAlignment` | `Bottom` | Vertical placement of the title. |
| `TitleFontSize` | `double` | `12` | Title font size. |
| `CountFontSize` | `double` | `28` | Count font size. |
| `TiltFactor` | `int` | `5` | Tilt amount for the tile's press interaction. |

`Tile` derives from `Button`, so `Background`, `Foreground`, `Command`, `Click`, etc. all work as usual.

## `MasonryControl` properties

| Property | Type | Description |
|---|---|---|
| `Spacing` | `int` | Gap (px) added around each child during packing. |

## How it works

Children are laid out by tracking the open horizontal spans (a free-space "matrix") and placing each child in the shallowest span wide enough to hold it. Positions are applied via `Margin`. Because WinUI lacks WPF's render/child size-change hooks, the control re-runs its layout from `MeasureOverride`, `ArrangeOverride`, `OnApplyTemplate`, and when containers are prepared.

There is no animated variant in the WinUI version.

See the `MasonryWinUI.Example` project in the repository for a complete runnable sample.
