# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

Masonry is a fluid, "Windows 8 tile"–style layout grid control. Two parallel implementations share the same packing algorithm:

- **`Masonry/`** — the original WPF control (NuGet package `wpfmasonry`). Multi-targets `net461`, `net8/9-windows`, and `net8/9.0-windows10.0.18362`.
- **`MasonryWinUI/`** — a WinUI 3 / Windows App SDK port (NuGet package `LoDaTek.MasonryWinUI`). Targets `net8/9.0-windows10.0.19041.0`.

Each library has a matching example app: `Masonry.Example/` (WPF) and `MasonryWinUI.Example/` (WinUI).

## Build & Run

The solution is `Masonry.slnx` (the new XML solution format — the old `Masonry.sln` and `global.json` were removed). Note: the Azure pipelines still glob `**/*.sln`, so CI and local builds may diverge until the pipelines are updated to `.slnx`.

```bash
# Build everything
dotnet build Masonry.slnx -c Release

# Build a single project / target framework
dotnet build Masonry/Masonry.csproj -c Release -f net9-windows

# Run the example apps (WinUI needs an explicit platform)
dotnet run --project Masonry.Example
dotnet run --project MasonryWinUI.Example -c Debug -f net9.0-windows10.0.19041.0 -r win-x64
```

`GeneratePackageOnBuild` is on for both libraries, so a Release build emits `.nupkg` files. There is no test project.

Builds require Windows (WPF/WinUI) and the Windows App SDK workload for the WinUI projects. The WinUI example pins to specific platforms (`x64`/`ARM64`/`x86`) in `Masonry.slnx`.

## Architecture

### The packing algorithm (shared, duplicated)

`MasonryControl.Update()` is the core. It is **copy-pasted nearly verbatim** between `Masonry/MasonryControl.cs` and `MasonryWinUI/MasonryControl.winui.cs` — any fix to the layout math must be applied to both files.

It works as a free-space "matrix": each entry is `[left, right, depth]` describing an open horizontal span and how far down it sits. For each child it:
1. `GetAttachPoint` — finds the shallowest span wide enough for the child.
2. `UpdateAttachArea` / `MatrixTrimWidth` / `MatrixJoin` — consumes that area and merges adjacent equal-depth spans.
3. `SetPosition` — positions the child by setting its `Margin` (left/top). Children are left/top aligned.

Positioning is done via `Margin`, not a custom `Panel`/`ArrangeOverride`, so the control is an `ItemsControl` whose items overlap in a `Grid` and are nudged into place.

### What triggers a relayout

- **WPF**: overrides `OnItemsChanged`, `OnItemsSourceChanged`, `OnRenderSizeChanged`, and `OnChildDesiredSizeChanged`. New children get a `Loaded` handler so layout waits until they have an `ActualWidth`/`ActualHeight` (see `HandleUpdate`).
- **WinUI**: the equivalent virtual size-change hooks don't exist, so it instead calls `Update()` from `MeasureOverride`, `ArrangeOverride`, `OnApplyTemplate`, `PrepareContainerForItemOverride`, and `OnBringIntoViewRequested`. This is the main structural difference from the WPF version.

### Animation (WPF only)

`AnimatedMasonryControl : MasonryControl` makes tiles glide to new positions instead of jumping. It overrides `SetPosition` to enqueue a `ThicknessAnimation` on `AnimationManager` instead of setting `Margin` directly. `AnimationManager` queues per-element animations, tracks active ones, and fires `OnCompleted` when all finish; size/render changes that arrive mid-animation set a `requestUpdate` flag and re-run `Update()` once animation completes. There is no WinUI animation equivalent.

### WinUI tile + theme

`MasonryWinUI/Tile.cs` is a `Button` subclass with `Title`/`Count`/alignment/font-size dependency properties; its default template lives in `MasonryWinUI/Themes/Generic.xaml`. WinUI controls set `DefaultStyleKey` (templated controls); WPF controls do not.

## Conventions

- Source files carry a GPL license header block (the original project), even though the csproj `PackageLicenseExpression` is `MIT` — preserve existing headers when editing. `default.licenseheader` defines the header for tooling.
- Code is organized with `#region` blocks (Static Fields / Fields / Constructors / Public Properties / Public Methods / Methods) and `this.`-qualified member access — match this style in existing files.
- The WPF library uses C#-style `using` directives *inside* the namespace; the WinUI library puts them at the top. Follow whichever file you're editing.
