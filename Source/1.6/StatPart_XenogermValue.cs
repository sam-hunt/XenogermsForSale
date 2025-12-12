using System.Text;
using RimWorld;
using Verse;

namespace XenogermsForSale
{
    /// <summary>
    /// Adds market value to xenogerms that have a preset xenotype source (trader-sold xenogerms).
    /// Player-crafted xenogerms (no CompXenotypeSource) retain their base 20 silver value.
    /// Pricing formula is defined in XenogermPricing and configurable via mod settings.
    /// </summary>
    public class StatPart_XenogermValue : StatPart
    {
        private static XenogermsForSaleSettings Settings => XenogermsForSaleMod.Settings;

        public override void TransformValue(StatRequest req, ref float val)
        {
            if (!req.HasThing)
            {
                return;
            }

            if (!(req.Thing is Xenogerm xenogerm))
            {
                return;
            }

            // Only add value for preset xenotype xenogerms (from traders)
            var comp = xenogerm.TryGetComp<CompXenotypeSource>();
            if (comp?.sourceXenotype == null)
            {
                return;
            }

            var geneSet = xenogerm.GeneSet;
            if (geneSet == null)
            {
                return;
            }

            val += XenogermPricing.Calculate(geneSet.GenesListForReading).Premium;
        }

        public override string ExplanationPart(StatRequest req)
        {
            if (!req.HasThing)
            {
                return null;
            }

            if (!(req.Thing is Xenogerm xenogerm))
            {
                return null;
            }

            var comp = xenogerm.TryGetComp<CompXenotypeSource>();
            if (comp?.sourceXenotype == null)
            {
                return null;
            }

            var geneSet = xenogerm.GeneSet;
            if (geneSet == null)
            {
                return null;
            }

            var breakdown = XenogermPricing.Calculate(geneSet.GenesListForReading);
            var sb = new StringBuilder();

            // Show xenotype preset base
            sb.AppendLine($"Full xenogene set premium: +{Settings.basePresetValue}");

            // Show metabolism contribution
            if (breakdown.AbsoluteMetabolism > 0)
            {
                float metabolismValue = breakdown.AbsoluteMetabolism * Settings.valuePerMetabolism;
                sb.AppendLine($"Genetic metabolism premium ({Settings.valuePerMetabolism} x {breakdown.AbsoluteMetabolism}): +{metabolismValue}");
            }

            // Show complexity contribution
            if (breakdown.Complexity > 0)
            {
                float complexityValue = breakdown.Complexity * Settings.valuePerComplexity;
                sb.AppendLine($"Genetic complexity premium ({Settings.valuePerComplexity} x {breakdown.Complexity}): +{complexityValue}");
            }

            // Show archite contribution
            if (breakdown.Archites > 0)
            {
                float architeValue = breakdown.Archites * Settings.valuePerArchite;
                sb.AppendLine($"Archite genes ({Settings.valuePerArchite} x {breakdown.Archites}): +{architeValue}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
