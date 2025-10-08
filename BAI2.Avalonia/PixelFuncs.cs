using System;
using System.Collections.Generic;

namespace BAI
{
    public class PixelFuncs
    {
        public static uint FilterNiks(uint pixel)
        {
            return pixel;
        }

        public static uint FilterRood(uint pixel)
        {
            // Zet rood-bits (23-16) op 0, behoud rest
            return pixel & 0xFF00FFFF;
        }
        
        public static uint FilterGroen(uint pixel)
        {
            // Zet groen-bits (15-8) op 0, behoud rest
            return pixel & 0xFFFF00FF;
        }
        
        public static uint FilterBlauw(uint pixel)
        {
            // Zet blauw-bits (7-0) op 0, behoud rest
            return pixel & 0xFFFFFF00;
        }


        public static byte RoodWaarde(uint pixelvalue)
        {
            // Shift rood naar rechts (16 bits), pak laatste 8 bits
            return (byte)((pixelvalue >> 16) & 0xFF);
        }

        public static byte GroenWaarde(uint pixelvalue)
        {
            // Shift groen naar rechts (8 bits), pak laatste 8 bits
            return (byte)((pixelvalue >> 8) & 0xFF);
        }

        public static byte BlauwWaarde(uint pixelvalue)
        {
            // Blauw staat al rechts, pak alleen laatste 8 bits
            return (byte)(pixelvalue & 0xFF);
        }

        public static uint Steganografie(uint pixelvalue)
        {
            // *** IMPLEMENTATION HERE *** //
            return 0;
        }


        // ***** Voor de liefhebbers - deze hoef je NIET te maken om een voldoende te krijgen! ***** //
        public static uint Steganografie2(uint pixelvalue)
        {
            // In het originele plaatje zit nog een tweede plaatje verstopt, maar dan op
            // een *nog* ingewikkelder manier.
            // Laat zien dat je echt een expert bent, en decodeer hier het tweede plaatje.
            // (Hint: kijk naar de eerste 4 bytes van de gedecodeerde data.)

            // *** IMPLEMENTATION HERE *** //
            return 0;
        }
    }
}