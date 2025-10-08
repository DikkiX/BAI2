using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Interactivity;
using BAI;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace BAI2.Avalonia;

public partial class MainWindow : Window
{
    private uint[] _pixelData = Array.Empty<uint>();
    private Bitmap? _image;
    private int _width, _height, _bitmapScale;
    private WriteableBitmap? _writeableBitmap;

    public MainWindow()
    {
        InitializeComponent();
        LoadImageAndInit();
    }

    private void LoadImageAndInit()
    {
        var path = System.IO.Path.Combine(AppContext.BaseDirectory, "logo-HBOICT.png");
        using (var img = SixLabors.ImageSharp.Image.Load<Rgba32>(path))
        {
            _width = img.Width;
            _height = img.Height;
            _pixelData = new uint[_width * _height];
            int pos = 0;
            img.ProcessPixelRows(accessor =>
            {
                for (int y = 0; y < _height; y++)
                {
                    var span = accessor.GetRowSpan(y);
                    for (int x = 0; x < _width; x++)
                    {
                        var px = span[x];
                        _pixelData[pos++] = (uint)(px.A << 24 | px.R << 16 | px.G << 8 | px.B);
                    }
                }
            });
        }

        _image = new Bitmap(path);
        _bitmapScale = (int)(_width / imgBitmap.Width);
        if (_bitmapScale <= 0) _bitmapScale = 1;

        _writeableBitmap = new WriteableBitmap(new PixelSize(_width, _height), new Vector(96, 96), PixelFormat.Bgra8888, AlphaFormat.Unpremul);
        RenderPixelData(PixelFuncs.FilterNiks);
        imgBitmap.Source = _writeableBitmap;
    }

    private void btnFilterKleur_Click(object? sender, RoutedEventArgs e)
    {
        switch ((sender as Button)?.Name)
        {
            case "btnFilterRood":
                RenderPixelData(PixelFuncs.FilterRood);
                break;
            case "btnFilterGroen":
                RenderPixelData(PixelFuncs.FilterGroen);
                break;
            case "btnFilterBlauw":
                RenderPixelData(PixelFuncs.FilterBlauw);
                break;
            case "btnOrigineleKleuren":
                RenderPixelData(PixelFuncs.FilterNiks);
                break;
        }
    }

    private void imgBitmap_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var p = e.GetPosition(imgBitmap);
        int Xpos = (int)p.X;
        int Ypos = (int)p.Y;
        if (Xpos < 0 || Ypos < 0 || Xpos >= imgBitmap.Bounds.Width || Ypos >= imgBitmap.Bounds.Height)
            return;

        int srcX = Math.Min(_width - 1, Xpos * _bitmapScale);
        int srcY = Math.Min(_height - 1, Ypos * _bitmapScale);
        uint pixelvalue = _pixelData[srcY * _width + srcX];

        lblCursorWaardes.Text = string.Format("Positie: ({0}, {1}) - rood: {2}, groen: {3}, blauw: {4}",
            Xpos, Ypos, PixelFuncs.RoodWaarde(pixelvalue), PixelFuncs.GroenWaarde(pixelvalue), PixelFuncs.BlauwWaarde(pixelvalue));
    }

    private void btnKleurenSet_Click(object? sender, RoutedEventArgs e)
    {
        switch ((sender as Button)?.Name)
        {
            case "btnAlleKleuren":
                RenderHashSet(SetFuncs.AlleKleuren(_pixelData));
                break;
            case "btnBlauwtinten":
                RenderHashSet(SetFuncs.BlauwTinten(_pixelData));
                break;
            case "btnDonkereKleuren":
                RenderHashSet(SetFuncs.DonkereKleuren(_pixelData));
                break;
            case "btnNietBlauw":
                RenderHashSet(SetFuncs.NietBlauwTinten(_pixelData));
                break;
            case "btnDonkerblauw":
                RenderHashSet(SetFuncs.DonkerBlauwTinten(_pixelData));
                break;
        }
    }

    private void btnStegaPlaatje_Click(object? sender, RoutedEventArgs e)
    {
        RenderPixelData(PixelFuncs.Steganografie);
    }

    private void btnStegaPlaatje2_Click(object? sender, RoutedEventArgs e)
    {
        RenderPixelData(PixelFuncs.Steganografie2);
    }

    private void RenderPixelData(Func<uint, uint> bitwiseTransform)
    {
        if (_writeableBitmap is null) return;
        var transformed = _pixelData.Select(bitwiseTransform).ToArray();
        using var fb = _writeableBitmap.Lock();
        unsafe
        {
            var dst = (byte*)fb.Address;
            int stride = fb.RowBytes;
            int pos = 0;
            for (int y = 0; y < _height; y++)
            {
                var row = dst + y * stride;
                for (int x = 0; x < _width; x++)
                {
                    uint v = transformed[pos++];
                    // BGRA order for Avalonia
                    row[x * 4 + 0] = (byte)(v & 0xFF);
                    row[x * 4 + 1] = (byte)((v >> 8) & 0xFF);
                    row[x * 4 + 2] = (byte)((v >> 16) & 0xFF);
                    row[x * 4 + 3] = (byte)((v >> 24) & 0xFF);
                }
            }
        }
        // force refresh in UI
        imgBitmap.Source = null;
        imgBitmap.Source = _writeableBitmap;
        imgBitmap.InvalidateVisual();
    }

    private void RenderHashSet(System.Collections.Generic.HashSet<uint> kleurenData)
    {
        if (_writeableBitmap is null) return;
        uint[] hashsetData = new uint[_pixelData.Length];
        int pos = 0;
        foreach (uint kleur in kleurenData.OrderBy(waarde => waarde))
        {
            int count = (int)Math.Floor((double)(_pixelData.Length / Math.Max(1, kleurenData.Count)));
            for (int i = 0; i < count && pos < hashsetData.Length; i++)
            {
                hashsetData[pos++] = kleur;
            }
        }
        while (pos < hashsetData.Length)
        {
            hashsetData[pos] = hashsetData[pos - 1];
            pos++;
        }

        using var fb = _writeableBitmap.Lock();
        unsafe
        {
            var dst = (byte*)fb.Address;
            int stride = fb.RowBytes;
            int idx = 0;
            for (int y = 0; y < _height; y++)
            {
                var row = dst + y * stride;
                for (int x = 0; x < _width; x++)
                {
                    uint v = hashsetData[idx++];
                    row[x * 4 + 0] = (byte)(v & 0xFF);
                    row[x * 4 + 1] = (byte)((v >> 8) & 0xFF);
                    row[x * 4 + 2] = (byte)((v >> 16) & 0xFF);
                    row[x * 4 + 3] = (byte)((v >> 24) & 0xFF);
                }
            }
        }
        imgBitmap.Source = null;
        imgBitmap.Source = _writeableBitmap;
        imgBitmap.InvalidateVisual();
    }
}