using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ImageToGoogleMaps
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var gps = ImageMetadataReader.ReadMetadata(args[0])
                                 .OfType<GpsDirectory>()
                                 .FirstOrDefault();

                var location = gps.GetGeoLocation();

                string GPS = location.Latitude.ToString().Replace(',', '.') + "," + location.Longitude.ToString().Replace(',', '.');
                string GPS_LINK = "http://maps.google.de/maps?q=" + GPS;

                
                System.Diagnostics.Process.Start(GPS_LINK);
                var t = new Thread((ThreadStart)(() =>
                {
                    System.Windows.Forms.Clipboard.SetText(GPS);
                }));

                t.SetApartmentState(ApartmentState.STA);

                t.Start();

                t.Join();
            }
            catch(Exception e)
            {
                Console.ReadKey();
            }
        }
    }
}
