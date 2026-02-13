using RekodLampuJalan.Models;
using RekodLampuJalan.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RekodLampuJalan.Services
{
    public interface IPopupService
    {
        Task<string?> ShowLoginPopupAsync();
        Task<string?> ShowFeederViewPopUpAsync(FeederPillar feederPillar);
        Task<string?> ShowPinViewPopUpAsync(MapPin mapPin);
        Task<MapPinPopUpData?> ShowPinNewPopUpAsync(int id, Location location, AlamatInsert alamat);
        Task<MapPinPopUpData?> ShowPinNewPopUpAsync(MapPin mapPin);
        Task<ImagePopupResult?> ShowImageFromFilePopUpAsync(string? imagePath = null);
        Task<FeederPillarPopUpData?> ShowFeederPillarNewPopUpAsync(Location location, AlamatInsert alamat);
        Task<FeederPillarPopUpData?> ShowFeederPillarNewPopUpAsync(FeederPillar feederPillar);
        Task<AlamatInsert?> ShowAlamatEditPopUpAsync(List<AlamatInsert> listAlamat, AlamatInsert alamatInsert);
        Task<string?> ShowFlyCablePopUpAsync(FlyCable flyCable);
    }
}
