using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BAI
{
    [TestFixture]
    class Opdr2Tests
    {
        Random random;
        const int NUM_RANDOM = 1000;

        [OneTimeSetUp]
        public void Init()
        {
            random = new Random();
        }

        public uint RandomUInt()
        {
            // https://stackoverflow.com/questions/17080112/generate-random-uint
            uint thirtyBits = (uint)random.Next(1 << 30);
            uint twoBits = (uint)random.Next(1 << 2);
            uint fullRange = (thirtyBits << 2) | twoBits;

            return fullRange;
        }

        public uint[] RandomPixels(int count)
        {
            uint[] pixelvalues = new uint[count];

            for (int i = 0; i < count; i++)
                pixelvalues[i] = RandomUInt();
            return pixelvalues;
        }

        [Test]
        public void Opdr2a_1_AlleKleuren()
        {
            uint[] pixelvalues = RandomPixels(NUM_RANDOM);
            HashSet<uint> set = SetFuncs.AlleKleuren(pixelvalues);

            // NB: er is een kleine kans (< NUM_RANDOM / 2^32 =~ 1 op 4,3 miljoen) dat de volgende Assert onterecht faalt
            // omdat pixelvalues meerdere keren dezelfde waarde bevat.
            Assert.That(pixelvalues.Length, Is.EqualTo(set.Count));
            foreach (uint pixelvalue in pixelvalues)
                Assert.That(set.Contains(pixelvalue), Is.True);
        }

        [Test]
        public void Opdr2a_2_BlauwTinten()
        {
            uint[] pixelvalues = RandomPixels(NUM_RANDOM);
            HashSet<uint> set = SetFuncs.BlauwTinten(pixelvalues);
            int countBlauw = 0;

            foreach (uint pixelvalue in pixelvalues)
            {
                if (PixelFuncs.BlauwWaarde(pixelvalue) > PixelFuncs.RoodWaarde(pixelvalue) &&
                    PixelFuncs.BlauwWaarde(pixelvalue) > PixelFuncs.GroenWaarde(pixelvalue))
                {
                    Assert.That(set.Contains(pixelvalue), Is.True);
                    countBlauw++;
                }
            }

            // NB: er is een kleine kans (< NUM_RANDOM / 2^32 =~ 1 op 4,3 miljoen) dat de volgende Assert onterecht faalt
            // omdat pixelvalues meerdere keren dezelfde waarde bevat.
            Assert.That(countBlauw, Is.EqualTo(set.Count));
        }

        [Test]
        public void Opdr2a_3_DonkereKleuren()
        {
            uint[] pixelvalues = RandomPixels(NUM_RANDOM);
            HashSet<uint> set = SetFuncs.DonkereKleuren(pixelvalues);
            int countDonker = 0;

            foreach (uint pixelvalue in pixelvalues)
            {
                if (PixelFuncs.RoodWaarde(pixelvalue) + PixelFuncs.GroenWaarde(pixelvalue) +
                    PixelFuncs.BlauwWaarde(pixelvalue) < 192)//
                {
                    Assert.That(set.Contains(pixelvalue), Is.True);
                    countDonker++;
                }
            }

            // NB: er is een kleine kans (< NUM_RANDOM / 2^32 =~ 1 op 4,3 miljoen) dat de volgende Assert onterecht faalt
            // omdat pixelvalues meerdere keren dezelfde waarde bevat.
            Assert.That(countDonker, Is.EqualTo(set.Count));
        }

        [Test]
        public void Opdr2b_1_NietBlauwTinten()
        {
            uint[] pixelvalues = RandomPixels(NUM_RANDOM);
            HashSet<uint> set = SetFuncs.NietBlauwTinten(pixelvalues);
            int countNietBlauw = 0;

            foreach (uint pixelvalue in pixelvalues)
            {
                if (PixelFuncs.BlauwWaarde(pixelvalue) <= PixelFuncs.RoodWaarde(pixelvalue) ||
                    PixelFuncs.BlauwWaarde(pixelvalue) <= PixelFuncs.GroenWaarde(pixelvalue))
                {
                    Assert.That(set.Contains(pixelvalue), Is.True);
                    countNietBlauw++;
                }
            }

            // NB: er is een kleine kans (< NUM_RANDOM / 2^32 =~ 1 op 4,3 miljoen) dat de volgende Assert onterecht faalt
            // omdat pixelvalues meerdere keren dezelfde waarde bevat.
            Assert.That(countNietBlauw, Is.EqualTo(set.Count));
        }

        [Test]
        public void Opdr2b_2_DonkerBlauwTinten()
        {
            uint[] pixelvalues = RandomPixels(NUM_RANDOM);
            HashSet<uint> set = SetFuncs.DonkerBlauwTinten(pixelvalues);
            int countDonkerBlauw = 0;

            foreach (uint pixelvalue in pixelvalues)
            {
                if ((PixelFuncs.RoodWaarde(pixelvalue) + PixelFuncs.GroenWaarde(pixelvalue) +
                      PixelFuncs.BlauwWaarde(pixelvalue) < 192) &&
                     (PixelFuncs.BlauwWaarde(pixelvalue) > PixelFuncs.RoodWaarde(pixelvalue)) &&
                     (PixelFuncs.BlauwWaarde(pixelvalue) > PixelFuncs.GroenWaarde(pixelvalue)))
                {
                    Assert.That(set.Contains(pixelvalue), Is.True);
                    countDonkerBlauw++;
                }
            }

            // NB: er is een kleine kans (< NUM_RANDOM / 2^32 =~ 1 op 4,3 miljoen) dat de volgende Assert onterecht faalt
            // omdat pixelvalues meerdere keren dezelfde waarde bevat.
            Assert.That(countDonkerBlauw, Is.EqualTo(set.Count));
        }

    }
}
