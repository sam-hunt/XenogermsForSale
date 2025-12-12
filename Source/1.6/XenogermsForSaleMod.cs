using HarmonyLib;
using UnityEngine;
using Verse;

namespace XenogermsForSale
{
    public class XenogermsForSaleMod : Mod
    {
        public static XenogermsForSaleSettings Settings { get; private set; }

        public XenogermsForSaleMod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<XenogermsForSaleSettings>();

            var harmony = new Harmony("SamHunt.XenogermsForSale");
            harmony.PatchAll();
            Log.Message("[XenogermsForSale] Mod loaded.");
        }

        public override string SettingsCategory()
        {
            return "Xenogerms For Sale";
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);

            listing.CheckboxLabeled(
                "Include archite xenotypes",
                ref Settings.includeArchiteXenotypes,
                "Allow traders to sell xenogerms for xenotypes containing archite genes (e.g., Sanguophage). These are typically more expensive.");

            listing.CheckboxLabeled(
                "Include inheritable xenotypes",
                ref Settings.includeInheritableXenotypes,
                "Allow traders to sell xenogerms for germline xenotypes (e.g., Impid, Yttakin). Implanting these creates 'naturalized' members recognized by ideology.");

            listing.CheckboxLabeled(
                "Include player-created xenotypes",
                ref Settings.includePlayerCreatedXenotypes,
                "Allow traders to sell xenogerms for xenotypes you created in the scenario editor. Disabled by default since traders wouldn't logically have access to your custom designs.");

            listing.GapLine(16f);
            listing.Label("Xenogerm Pricing");
            Text.Font = GameFont.Tiny;
            listing.Label("Adjust the market value formula for trader-sold xenogerms. Changes affect new xenogerms only.");
            Text.Font = GameFont.Small;
            listing.Gap(6f);

            Settings.basePresetValue = (float)System.Math.Round(listing.SliderLabeled(
                $"Base preset value: {Settings.basePresetValue:F0}",
                Settings.basePresetValue,
                XenogermsForSaleSettings.MinBasePresetValue,
                XenogermsForSaleSettings.MaxBasePresetValue,
                tooltip: $"Base silver value added to all preset xenotype xenogerms. Default: {XenogermsForSaleSettings.DefaultBasePresetValue}"));

            Settings.valuePerMetabolism = (float)System.Math.Round(listing.SliderLabeled(
                $"Value per metabolism: {Settings.valuePerMetabolism:F0}",
                Settings.valuePerMetabolism,
                XenogermsForSaleSettings.MinValuePerMetabolism,
                XenogermsForSaleSettings.MaxValuePerMetabolism,
                tooltip: $"Silver value added per point of absolute metabolism. Default: {XenogermsForSaleSettings.DefaultValuePerMetabolism}"));

            Settings.valuePerComplexity = (float)System.Math.Round(listing.SliderLabeled(
                $"Value per complexity: {Settings.valuePerComplexity:F0}",
                Settings.valuePerComplexity,
                XenogermsForSaleSettings.MinValuePerComplexity,
                XenogermsForSaleSettings.MaxValuePerComplexity,
                tooltip: $"Silver value added per point of genetic complexity. Default: {XenogermsForSaleSettings.DefaultValuePerComplexity}"));

            Settings.valuePerArchite = (float)System.Math.Round(listing.SliderLabeled(
                $"Value per archite gene: {Settings.valuePerArchite:F0}",
                Settings.valuePerArchite,
                XenogermsForSaleSettings.MinValuePerArchite,
                XenogermsForSaleSettings.MaxValuePerArchite,
                tooltip: $"Silver value added per archite gene. Default: {XenogermsForSaleSettings.DefaultValuePerArchite}"));

            listing.End();
        }
    }
}
