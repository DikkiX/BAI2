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
            // Alle kleuren behalve de blauwtinten
            HashSet<uint> result = AlleKleuren(pixeldata);
            //verwijder alles wat in de andere set zit
            result.ExceptWith(BlauwTinten(pixeldata));
            return result;
        }

        public static HashSet<uint> DonkerBlauwTinten(uint[] pixeldata)
        {
            // Kleuren die zowel blauw als donker zijn
            HashSet<uint> result = BlauwTinten(pixeldata);
            //(intersect) houd alleen wat in BEIDE sets zit
            result = new HashSet<uint>(result.Intersect(DonkereKleuren(pixeldata)));
            return result;
        }
    }
}
