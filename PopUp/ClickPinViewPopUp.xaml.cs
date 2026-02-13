using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using RekodLampuJalan.Models;
using RekodLampuJalan.PopUp.PopUpViewModel;

namespace RekodLampuJalan.PopUp
{
    public partial class ClickPinViewPopUp : Popup
    {
        ClickPinViewPopUpViewModel viewModel;
        public ClickPinViewPopUp(MapPin mapPin)
        {
           // WeakReferenceMessenger.Default.Send(new MapViewBusyMessage(true));
            InitializeComponent();
            viewModel=new ClickPinViewPopUpViewModel(mapPin);
            this.BindingContext = viewModel;
           // WeakReferenceMessenger.Default.Send(new MapViewBusyMessage(false));
            //var _JenisItem = App.DropDownItem.Jenis_Item.Where(x => x.Id == mapPin.jenis_item).FirstOrDefault();
            //if (_JenisItem != null)
            //{
            //    JenisItem.Text = _JenisItem.Item;
            //}
            //else
            //{
            //    JenisItem.Text = "";
            //}

            //var _JenisLampu = App.DropDownItem.Jenis_Lampu.Where(x => x.Id == mapPin.jenama_lampu).FirstOrDefault();
            //if (_JenisLampu != null)
            //{
            //    JenisLampu.Text = _JenisLampu.Item;
            //}
            //else
            //{
            //    JenisLampu.Text = "";
            //}
            //var _TinggiTiang = App.DropDownItem.Tinggi_Tiang.Where(x => x.Id == mapPin.tinggi_tiang).FirstOrDefault();
            //if (_TinggiTiang != null)
            //{
            //    TinggiTiang.Text = _TinggiTiang.Item.ToString();
            //}
            //else
            //{
            //    TinggiTiang.Text = "";
            //}


            //var _MaterialTiang = App.DropDownItem.Material_Tiang.Where(x => x.Id == mapPin.material_tiang).FirstOrDefault();
            //if (_MaterialTiang != null)
            //{
            //    MaterialTiang.Text = _MaterialTiang.Item;
            //}
            //else
            //{
            //    MaterialTiang.Text = "";
            //}


            //var _JenisPintuPenyelenggaraan = App.DropDownItem.Jenis_Pintu_Penyelenggaraan.Where(x => x.Id == mapPin.jenis_pintu_penyelenggaraan).FirstOrDefault();
            //if (_JenisPintuPenyelenggaraan != null)
            //{
            //    JenisPintuPenyelenggaraan.Text = _JenisPintuPenyelenggaraan.Item;
            //}
            //else
            //{
            //    JenisPintuPenyelenggaraan.Text = "";
            //}

            //var _SaizPintuPenyelenggaraan = App.DropDownItem.Saiz_Pintu_Penyelenggaraan.Where(x => x.Id == mapPin.saiz_pintu_penyelenggaraan).FirstOrDefault();
            //if (_SaizPintuPenyelenggaraan != null)
            //{
            //    SaizPintuPenyelenggaraan.Text = _SaizPintuPenyelenggaraan.Item;
            //}
            //else
            //{
            //    SaizPintuPenyelenggaraan.Text = "";
            //}


            //var _PanjangLenganLampu = App.DropDownItem.Panjang_Lengan_Lampu.Where(x => x.Id == mapPin.panjang_lengan_lampu).FirstOrDefault();
            //if (_PanjangLenganLampu != null)
            //{
            //    PanjangLenganLampu.Text = _PanjangLenganLampu.Item.ToString();
            //}
            //else
            //{
            //    PanjangLenganLampu.Text = "";
            //}

            //var _JenisTiang = App.DropDownItem.Jenis_Tiang.Where(x => x.Id == mapPin.jenis_tiang).FirstOrDefault();
            //if (_JenisTiang != null)
            //{
            //    JenisTiang.Text = _JenisItem.Item;
            //}
            //else
            //{
            //    JenisTiang.Text = "";
            //}
            //var _JenamaLampu = App.DropDownItem.Jenama_Lampu.Where(x => x.Id == mapPin.jenama_lampu).FirstOrDefault();
            //if (_JenamaLampu != null)
            //{
            //    JenamaLampu.Text = _JenamaLampu.Item;
            //}
            //else
            //{
            //    JenamaLampu.Text = "";
            //}
            //var _JenamaTiang = App.DropDownItem.Jenama_Tiang.Where(x => x.Id == mapPin.jenama_tiang).FirstOrDefault();
            //if (_JenamaTiang != null)
            //{
            //    JenamaTiang.Text = _JenamaTiang.Item;
            //}
            //else
            //{
            //    JenamaTiang.Text = "";
            //}


            ////if (attchment != "" && attchment != null)
            ////{
            ////    AttachedImage.IsVisible = true;
            ////    AttachedImage.Source = ImageSource.FromUri(new Uri("https://vertbyte.com/rekod_lampu_jalan/uploads/" + attchment));
            ////    this.Size = new Size(400, 370);
            ////}
            ////else
            ////{
            //    AttachedImage.IsVisible = false;
            //    AttachedImage.Source = null;
            //    this.Size = new Size(400, 220);
            //}
        }
        private async void OnCloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

