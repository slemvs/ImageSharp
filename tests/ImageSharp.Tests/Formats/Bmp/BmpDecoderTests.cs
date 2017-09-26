﻿// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;
// ReSharper disable InconsistentNaming

namespace SixLabors.ImageSharp.Tests
{
    using SixLabors.ImageSharp.Formats.Bmp;

    public class BmpDecoderTests : FileTestBase
    {
        [Theory]
        [WithFileCollection(nameof(AllBmpFiles), PixelTypes.Rgb24)]
        public void DecodeBmp<TPixel>(TestImageProvider<TPixel> provider)
            where TPixel : struct, IPixel<TPixel>
        {
            using (Image<TPixel> image = provider.GetImage(new BmpDecoder()))
            {
                image.DebugSave(provider, "bmp");
                image.CompareToOriginal(provider);
            }
        }

        [Theory]
        [WithFile(TestImages.Bmp.F, CommonNonDefaultPixelTypes)]
        public void BmpDecoder_IsNotBoundToSinglePixelType<TPixel>(TestImageProvider<TPixel> provider)
            where TPixel : struct, IPixel<TPixel>
        {
            using (Image<TPixel> image = provider.GetImage(new BmpDecoder()))
            {
                image.DebugSave(provider, "bmp");
                image.CompareToOriginal(provider);
            }
        }

        [Theory]
        [InlineData(TestImages.Bmp.Car, 24)]
        [InlineData(TestImages.Bmp.F, 24)]
        [InlineData(TestImages.Bmp.NegHeight, 24)]
        [InlineData(TestImages.Bmp.Bpp8, 8)]
        public void DetectPixelSize(string imagePath, int expectedPixelSize)
        {
            TestFile testFile = TestFile.Create(imagePath);
            using (var stream = new MemoryStream(testFile.Bytes, false))
            {
                Assert.Equal(expectedPixelSize, Image.DetectPixelType(stream)?.BitsPerPixel);
            }
        }
    }
}
