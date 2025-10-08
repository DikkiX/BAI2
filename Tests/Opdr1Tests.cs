using NUnit.Framework;


namespace BAI
{
    [TestFixture]
    public class Opdr1Tests
    {
        [TestCase((uint)0b01010101_01010101_01010101_01010101, (uint)0b01010101_00000000_01010101_01010101)]
        [TestCase((uint)0b10101010_10101010_10101010_10101010, (uint)0b10101010_00000000_10101010_10101010)]
        public void Opdr1a_1_FilterRood(uint input, uint expected)
        {
            // Arrange

            // Act
            uint actual = PixelFuncs.FilterRood(input);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase((uint)0b01010101_01010101_01010101_01010101, (uint)0b01010101_01010101_00000000_01010101)]
        [TestCase((uint)0b10101010_10101010_10101010_10101010, (uint)0b10101010_10101010_00000000_10101010)]
        public void Opdr1a_2_FilterGroen(uint input, uint expected)
        {
            // Arrange

            // Act
            uint actual = PixelFuncs.FilterGroen(input);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase((uint)0b01010101_01010101_01010101_01010101, (uint)0b01010101_01010101_01010101_00000000)]
        [TestCase((uint)0b10101010_10101010_10101010_10101010, (uint)0b10101010_10101010_10101010_00000000)]
        public void Opdr1a_3_FilterBlauw(uint input, uint expected)
        {
            // Arrange

            // Act
            uint actual = PixelFuncs.FilterBlauw(input);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase((uint)0b00000000_01010101_00000000_00000000, (uint)0b01010101)]
        [TestCase((uint)0b11111111_01010101_11111111_11111111, (uint)0b01010101)]
        [TestCase((uint)0b01010101_01010101_01010101_01010101, (uint)0b01010101)]
        [TestCase((uint)0b10101010_01010101_10101010_10101010, (uint)0b01010101)]
        [TestCase((uint)0b00000000_10101010_00000000_00000000, (uint)0b10101010)]
        [TestCase((uint)0b11111111_10101010_11111111_11111111, (uint)0b10101010)]
        [TestCase((uint)0b01010101_10101010_01010101_01010101, (uint)0b10101010)]
        [TestCase((uint)0b10101010_10101010_10101010_10101010, (uint)0b10101010)]
        public void Opdr1b_1_RoodWaarde(uint input, uint expected)
        {
            // Arrange

            // Act
            uint actual = PixelFuncs.RoodWaarde(input);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase((uint)0b00000000_00000000_01010101_00000000, (uint)0b01010101)]
        [TestCase((uint)0b11111111_11111111_01010101_11111111, (uint)0b01010101)]
        [TestCase((uint)0b01010101_01010101_01010101_01010101, (uint)0b01010101)]
        [TestCase((uint)0b10101010_10101010_01010101_10101010, (uint)0b01010101)]
        [TestCase((uint)0b00000000_00000000_10101010_00000000, (uint)0b10101010)]
        [TestCase((uint)0b11111111_11111111_10101010_11111111, (uint)0b10101010)]
        [TestCase((uint)0b01010101_01010101_10101010_01010101, (uint)0b10101010)]
        [TestCase((uint)0b10101010_10101010_10101010_10101010, (uint)0b10101010)]
        public void Opdr1b_2_GroenWaarde(uint input, uint expected)
        {
            // Arrange

            // Act
            uint actual = PixelFuncs.GroenWaarde(input);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase((uint)0b00000000_01010101_00000000_01010101, (uint)0b01010101)]
        [TestCase((uint)0b11111111_01010101_11111111_01010101, (uint)0b01010101)]
        [TestCase((uint)0b01010101_01010101_01010101_01010101, (uint)0b01010101)]
        [TestCase((uint)0b10101010_01010101_10101010_01010101, (uint)0b01010101)]
        [TestCase((uint)0b00000000_10101010_00000000_10101010, (uint)0b10101010)]
        [TestCase((uint)0b11111111_10101010_11111111_10101010, (uint)0b10101010)]
        [TestCase((uint)0b01010101_10101010_01010101_10101010, (uint)0b10101010)]
        [TestCase((uint)0b10101010_10101010_10101010_10101010, (uint)0b10101010)]
        public void Opdr1b_3_BlauwWaarde(uint input, uint expected)
        {
            // Arrange

            // Act
            uint actual = PixelFuncs.BlauwWaarde(input);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}