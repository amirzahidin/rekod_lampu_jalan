using CommunityToolkit.Maui.Views;
using RekodLampuJalan.Models;
using RekodLampuJalan.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RekodLampuJalan.Services
{
    public class PopupService : IPopupService
    {
        public async Task<string?> ShowLoginPopupAsync()
        {
            var popup = new PopUp.LoginPopUp();
            return await Application.Current.MainPage.ShowPopupAsync(popup) as string;
        }

        public async Task<string?> ShowFeederViewPopUpAsync(FeederPillar feederPillar)
        {
            var popup = new PopUp.FeederViewPopUp(feederPillar);
            return await Application.Current.MainPage.ShowPopupAsync(popup) as string;
        }
        public async Task<string?> ShowPinViewPopUpAsync(MapPin mapPin)
        {
            var popup = new PopUp.ClickPinViewPopUp(mapPin);
            return await Application.Current.MainPage.ShowPopupAsync(popup) as string;
        }
        public async Task<MapPinPopUpData?> ShowPinNewPopUpAsync(int id,Location location,AlamatInsert alamat)
        {
            var popup = new PopUp.NewPinPopUp(id, location, alamat);
            return await Application.Current.MainPage.ShowPopupAsync(popup) as MapPinPopUpData;
        }
        public async Task<MapPinPopUpData?> ShowPinNewPopUpAsync(MapPin mapPin)
        {
            var popup = new PopUp.NewPinPopUp(mapPin);
            return await Application.Current.MainPage.ShowPopupAsync(popup) as MapPinPopUpData;
        }
        public async Task<ImagePopupResult?> ShowImageFromFilePopUpAsync(string? imagePath = null)
        {
            var popup = new PopUp.ImagePopUpFromFile(imagePath);
            Application.Current.MainPage.ShowPopup(popup);
            return await popup.WaitForResultAsync();
        }
        public async Task<FeederPillarPopUpData?> ShowFeederPillarNewPopUpAsync(Location location ,AlamatInsert alamat)
        {
            var popup = new PopUp.NewFeederPillarPopUp(location,alamat);
            return await Application.Current.MainPage.ShowPopupAsync(popup) as FeederPillarPopUpData;
        }
        public async Task<FeederPillarPopUpData?> ShowFeederPillarNewPopUpAsync(FeederPillar feederPillar)
        {
            var popup = new PopUp.NewFeederPillarPopUp(feederPillar);
            return await Application.Current.MainPage.ShowPopupAsync(popup) as FeederPillarPopUpData;
        }
        public async Task<AlamatInsert?> ShowAlamatEditPopUpAsync(List<AlamatInsert> listAlamat,AlamatInsert alamatInsert)
        {
            var popup = new PopUp.AlamatEdit(listAlamat, alamatInsert);
            return await Application.Current.MainPage.ShowPopupAsync(popup) as AlamatInsert;
        }
        public async Task<string?> ShowFlyCablePopUpAsync(FlyCable flyCable)
        {
            var popup = new PopUp.FlyCablePopUp(flyCable);
            return await Application.Current.MainPage.ShowPopupAsync(popup) as string;
        }
    }
}
