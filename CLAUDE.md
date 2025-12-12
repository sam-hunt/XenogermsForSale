# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

RimWorld mod that adds xenogerms for preset xenotypes to trader inventories. Requires Biotech DLC and Harmony. The mod uses Harmony patches and XML patches to modify trader stock and xenogerm behavior.

## Build Commands

```bash
# Build (from repo root)
dotnet build

# Build Release
dotnet build -c Release

# Custom RimWorld path (if not in default location)
dotnet build -p:RimWorldPath="/path/to/RimWorld"
# Or set RIMWORLD_PATH environment variable
```

Output goes to `1.6/Assemblies/`. The project auto-detects RimWorld installation for references, falling back to NuGet `Krafs.Rimworld.Ref` package for CI builds.

## Architecture

### Core Components

- **XenogermsForSaleMod** - Entry point. Initializes Harmony patches and mod settings.
- **StockGenerator_Xenogerms** - Custom stock generator added to traders via XML patch. Generates xenogerms with weighted spawn rates (cheaper = more common).
- **XenogermFactory** - Creates xenogerms from XenotypeDef or CustomXenotype, populating genes and setting CompXenotypeSource.
- **CompXenotypeSource** - ThingComp that stores the source XenotypeDef reference. Added to Xenogerm ThingDef via XML patch. Used to distinguish trader-sold xenogerms from player-crafted ones.
- **Patch_ImplantXenogermItem** - Harmony postfix on `GeneUtility.ImplantXenogermItem`. Assigns the actual XenotypeDef to the pawn (not just genes), enabling Ideology recognition.

### Pricing System

- **XenogermPricing** - Centralized pricing calculations. Formula: base + (metabolism × mult) + (complexity × mult) + (archites × mult)
- **StatPart_XenogermValue** - Adds calculated premium to MarketValue stat for trader xenogerms only.
- **StatPart_XenogermSellFactor** - Adds archite bonus to SellPriceFactor. Base sell factor (0.05) set via XML to prevent exploits.

### XML Patches (1.6/Patches/)

- `Patches_Traders.xml` - Adds StockGenerator_Xenogerms to Orbital_ExoticGoods
- `Patches_GeneTrader.xml` - Conditionally adds stock generator to Gene Trader mod's trader
- `Patches_Xenogerm.xml` - Adds CompXenotypeSource to Xenogerm ThingDef
- `Patches_Stats.xml` - Adds StatParts to MarketValue and SellPriceFactor stats; sets base SellPriceFactor on Xenogerm

## Key Implementation Details

- Trader xenogerms have CompXenotypeSource set; player-crafted xenogerms do not
- The distinction between trader/player xenogerms drives pricing and xenotype assignment behavior
- Mod settings are accessed via `XenogermsForSaleMod.Settings`
- All pricing formula values are configurable in mod settings
