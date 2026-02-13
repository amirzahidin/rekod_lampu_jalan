using RekodLampuJalan.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RekodLampuJalan.Utils
{
    public class common
    {
        public resultJenisItem pinTagJenisItem(string pinTag)
        {
            resultJenisItem result = new resultJenisItem();
            string[] parts = pinTag.Split('_');
            result.name = parts[0];
            result.id =int.Parse( parts[1]); 
            return result;
        }
        public resultJenisItem PinImageJenisItem(string imageName)
        {
            resultJenisItem result = new resultJenisItem();
            string[] parts = imageName.Split('_');
            result.name = parts[0]+"_"+parts[1];
            result.id = int.Parse(parts[parts.Count()-1]);
            return result;
        }
        public double CalculateDistanceInMeters(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371000; // Earth's radius in meters

            var lat1Rad = DegreesToRadians(lat1);
            var lon1Rad = DegreesToRadians(lon1);
            var lat2Rad = DegreesToRadians(lat2);
            var lon2Rad = DegreesToRadians(lon2);

            var dLat = lat2Rad - lat1Rad;
            var dLon = lon2Rad - lon1Rad;

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c; // Distance in meters
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
        public async Task<MainData> SyncAlamat(MainData model,Alamat alamat)
        {
            foreach (var item in model.map_pin)
            {
                alamatFull alamatFull_ = getFullAlamat(item.alamat, alamat);
                if (alamatFull_ != null)
                {
                    item.alamatFull = alamatFull_;
                }
            }
            foreach (var item in model.feeder_pillar)
            {
                alamatFull alamatFull_= getFullAlamat(item.alamat,alamat);
                if(alamatFull_!=null)
                {
                    item.alamatFull= alamatFull_;
                }
            }
            return model;
        }
        private alamatFull getFullAlamat(int alamatId,Alamat alamat)
        {
            jalan_ jalan = alamat.jalan.Where(x => x.id == alamatId).FirstOrDefault();
            if (jalan != null)
            {
                bandar_ bandar = alamat.bandar.Where(x => x.id == jalan.bandar_id).First();
                if (bandar != null)
                {
                    daerah_ daerah = alamat.daerah.Where(x => x.id == bandar.daerah_id).First();
                    if (daerah != null)
                    {
                        poskod_ poskod = alamat.poskod.Where(x => x.id == daerah.poskod_id).First();
                        if (poskod != null)
                        {
                            negeri_ negeri = alamat.negeri.Where(x => x.id == poskod.negeri_id).First();
                            alamatFull alamatFull = new alamatFull();
                            alamatFull.jalan = jalan;
                            alamatFull.bandar = bandar;
                            alamatFull.poskod = poskod;
                            alamatFull.daerah = daerah;
                            alamatFull.negeri = negeri;
                            return alamatFull;
                        }
                    }
                }
            }
            return null;
        }
    }

    public class resultJenisItem
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class LengthToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double length)
                return $"{length:0.0} m";
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && str.EndsWith(" m") && double.TryParse(str[..^2], out double result))
                return result;
            return value;
        }
    }
   

}
