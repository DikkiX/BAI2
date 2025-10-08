using System.Collections.Generic;
using System.Linq;

namespace BAI
{
    public class SetFuncs
    {
        public static HashSet<uint> AlleKleuren(uint[] pixeldata)
        {
            // HashSet slaat alleen unieke waardes op (geen duplicaten)
            return new HashSet<uint>(pixeldata);
        }

        public static HashSet<uint> BlauwTinten(uint[] pixeldata)
        {
            // Alle kleuren waar blauw hoger is dan rood EN groen
            return new HashSet<uint>(
                pixeldata.Where(pixel => 
                    PixelFuncs.BlauwWaarde(pixel) > PixelFuncs.RoodWaarde(pixel) && 
                    PixelFuncs.BlauwWaarde(pixel) > PixelFuncs.GroenWaarde(pixel)
                )
            );
        }

        public static HashSet<uint> DonkereKleuren(uint[] pixeldata)
        {
            // Alle kleuren waar RGB-som < 192
            return new HashSet<uint>(
                pixeldata.Where(pixel =>
                    PixelFuncs.RoodWaarde(pixel) + 
                    PixelFuncs.GroenWaarde(pixel) + 
                    PixelFuncs.BlauwWaarde(pixel) < 192
                )
            );
        }

        public static HashSet<uint> NietBlauwTinten(uint[] pixeldata)
        {
            // *** IMPLEMENTATION HERE *** //
            // Gebruik set-operatoren op 1 of meer van de sets 'alle kleuren' /
            // 'blauwtinten' / 'donkere kleuren'
            // Gebruik dus GEEN loop
            return new HashSet<uint>();
        }

        public static HashSet<uint> DonkerBlauwTinten(uint[] pixeldata)
        {
            // *** IMPLEMENTATION HERE *** //
            // Gebruik set-operatoren op 1 of meer van de sets 'alle kleuren' /
            // 'blauwtinten' / 'donkere kleuren'
            // Gebruik dus GEEN loop
            return new HashSet<uint>();
        }
    }
}
