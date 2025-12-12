# Xenogerms For Sale

A RimWorld mod that adds xenogerms for preset xenotypes to trader inventories, allowing you to purchase complete xenotype packages instead of collecting individual genes.

## Features

**Purchase xenogerms from traders** - Orbital exotic goods traders now sell xenogerms for all preset xenotypes (Hussar, Impid, Sanguophage, etc.). Each xenogerm contains all the genes needed for that xenotype.

**Proper xenotype assignment** - Unlike player-crafted xenogerms, purchased xenogerms assign the actual xenotype to the pawn upon implantation. This enables:
- Ideology recognition (pawns are recognized as their xenotype for precept purposes)
- Creating "naturalized" members of endogene-only xenotypes like Impid

**Weighted spawn rates** - Cheaper xenogerms appear more frequently at traders, while expensive archite xenotypes like Sanguophage are rarer finds.

## Pricing

Xenogerm prices are based on gene complexity, metabolism impact, and archite gene count, e.g:
- Pigskin, Dirtmole: ~1,500 silver
- Hussar, Highmate: ~1,600-1,850 silver
- Sanguophage: ~3,200 silver

Sell prices are intentionally low (5%) to prevent buy-sell exploits, with a bonus for archite xenogerms.

## Mod Settings

- **Include archite xenotypes** - Toggle whether traders sell xenogerms containing archite genes (default: on)
- **Include inheritable xenotypes** - Toggle germline xenotypes like Impid and Yttakin (default: on)
- **Include player-created xenotypes** - Toggle xenotypes from scenario editor (default: off)
- **Xenogerm Pricing** - Customize the price formula by adjusting base preset value and multipliers for metabolism, complexity, and archite genes

## Compatibility

- **Gene Trader mod** (`tac.genetrader`) - Fully supported. If installed, the orbital gene trader will also stock xenogerms.
- **Save-safe** - Can be added or removed mid-game without issues.
- **Xenotype mods** - Automatically includes xenotypes from other mods.

## Requirements

- RimWorld 1.6
- Biotech DLC

## Installation

Subscribe on the Steam Workshop, or download and extract to your RimWorld `Mods/` folder.

## License

MIT
