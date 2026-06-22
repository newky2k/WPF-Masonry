# WPF Masonry

A fluid, "Windows 8 tile"–style layout grid for **WPF**. Arranges variable-sized children into a tightly packed, gap-filling layout that reflows as items are added, removed, or resized.

Targets .NET 10 (`net10.0-windows`) and .NET Framework 4.6.2 (`net462`).

## Install

```powershell
dotnet add package wpfmasonry
```

## Usage

Add the namespace and drop a `MasonryControl` into your XAML. It is an `ItemsControl`, so populate it with child `FrameworkElement`s directly or via `ItemsSource`.

```xml
<Window xmlns:masonry="clr-namespace:Masonry;assembly=Masonry">
    <ScrollViewer HorizontalScrollBarVisibility="Disabled">
        <masonry:MasonryControl Spacing="5" Background="#E3E3E3">
            <Border Width="200" Height="120" Background="SteelBlue" />
            <Border Width="200" Height="260" Background="IndianRed" />
            <Border Width="200" Height="160" Background="SeaGreen" />
        </masonry:MasonryControl>
    </ScrollViewer>
</Window>
```

### Binding items (MVVM)

Bind `ItemsSource` to a collection of `FrameworkElement`s. Use an `ObservableCollection` so the layout reflows automatically as items change:

```csharp
public ObservableCollection<FrameworkElement> Elements { get; } = new();

Elements.Add(new Border
{
    Width = 200,
    Height = 180,
    Background = Brushes.SteelBlue
});
```

```xml
<masonry:MasonryControl ItemsSource="{Binding Elements}" Spacing="5" />
```

> Children must derive from `FrameworkElement`; anything else throws `InvalidDataException`. Give each child an explicit `Width`/`Height` (or a size-determining content) so the packer can measure it.

### Animated layout

`AnimatedMasonryControl` is a drop-in replacement that animates tiles gliding to their new positions instead of snapping. Set `AnimationDuration` to control the glide:

```xml
<masonry:AnimatedMasonryControl ItemsSource="{Binding Elements}"
                                Spacing="5"
                                AnimationDuration="0:0:2" />
```

## Properties

| Control | Property | Type | Description |
|---|---|---|---|
| `MasonryControl` | `Spacing` | `int` | Gap (px) added around each child during packing. |
| `AnimatedMasonryControl` | `AnimationDuration` | `Duration` | Length of the reposition animation. |

`AnimatedMasonryControl` also exposes the underlying `AnimationManager` for advanced scenarios.

## How it works

Children are laid out by tracking the open horizontal spans (a free-space "matrix") and placing each child in the shallowest span wide enough to hold it. Positions are applied via `Margin`, and a relayout is triggered on items changes, item-source changes, render-size changes, and child desired-size changes.

See the `Masonry.Example` project in the repository for a complete runnable sample.
