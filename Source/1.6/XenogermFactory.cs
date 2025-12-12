using System.Collections.Generic;
using RimWorld;
using Verse;

namespace XenogermsForSale
{
    public static class XenogermFactory
    {
        public static Xenogerm CreateForXenotype(XenotypeDef xenotype)
        {
            var xenogerm = (Xenogerm)ThingMaker.MakeThing(ThingDefOf.Xenogerm);

            // Initialize with empty genepacks - this creates the GeneSet internally
            xenogerm.Initialize(new List<Genepack>(), xenotype.label, null);

            // Add genes from xenotype
            if (!xenotype.genes.NullOrEmpty())
            {
                foreach (var gene in xenotype.genes)
                {
                    xenogerm.GeneSet.AddGene(gene);
                }
            }

            // Store xenotype reference for implantation
            var comp = xenogerm.TryGetComp<CompXenotypeSource>();
            if (comp != null)
            {
                comp.sourceXenotype = xenotype;
            }

            return xenogerm;
        }

        public static Xenogerm CreateForCustomXenotype(CustomXenotype customXenotype)
        {
            var xenogerm = (Xenogerm)ThingMaker.MakeThing(ThingDefOf.Xenogerm);

            // Initialize with empty genepacks - this creates the GeneSet internally
            xenogerm.Initialize(new List<Genepack>(), customXenotype.name, customXenotype.iconDef);

            // Add genes from custom xenotype
            if (!customXenotype.genes.NullOrEmpty())
            {
                foreach (var gene in customXenotype.genes)
                {
                    xenogerm.GeneSet.AddGene(gene);
                }
            }

            // Note: No CompXenotypeSource set - pawn will get genes but not a preset xenotype reference
            // This matches vanilla behavior for player-crafted xenogerms

            return xenogerm;
        }
    }
}
