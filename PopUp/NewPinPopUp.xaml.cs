
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using RekodLampuJalan.Models;
using RekodLampuJalan.PopUp.PopUpViewModel;
using RekodLampuJalan.Utils;

namespace RekodLampuJalan.PopUp
{
    public partial class NewPinPopUp : Popup
	{
		NewPinPopUpViewModel viewModel;
		int id_;
		public NewPinPopUp(int id,Location location,AlamatInsert alamat)
		{
           // WeakReferenceMessenger.Default.Send(new MapViewBusyMessage(true));
            InitializeComponent();
			id_=id;
			viewModel = new NewPinPopUpViewModel(id, location, alamat);
			this.BindingContext = viewModel;
            SuggestionListJenis_Lampu.IsVisible=false;
            SuggestionListJenama_Tiang.IsVisible=false;
            SuggestionListJenama_Lampu.IsVisible = false;
            SuggestionListJenis_Pintu_Penyelenggaraan.IsVisible=false;
            SuggestionListJenis_Tiang.IsVisible=false;
            SuggestionListMaterial_Tiang.IsVisible=false;
            SuggestionListPanjang_Lengan_Lampu.IsVisible=false ;
            SuggestionListSaiz_Pintu_Penyelenggaraan.IsVisible =false;
            SuggestionListTinggi_Tiang.IsVisible=false;
            //WeakReferenceMessenger.Default.Send(new MapViewBusyMessage(false));
        }

        public NewPinPopUp(MapPin mapPin)
        {
            InitializeComponent();
            viewModel = new NewPinPopUpViewModel(mapPin);
            this.BindingContext = viewModel;
            SuggestionListJenis_Lampu.IsVisible = false;
            SuggestionListJenama_Tiang.IsVisible = false;
            SuggestionListJenama_Lampu.IsVisible = false;
            SuggestionListJenis_Pintu_Penyelenggaraan.IsVisible = false;
            SuggestionListJenis_Tiang.IsVisible = false;
            SuggestionListMaterial_Tiang.IsVisible = false;
            SuggestionListPanjang_Lengan_Lampu.IsVisible = false;
            SuggestionListSaiz_Pintu_Penyelenggaraan.IsVisible = false;
            SuggestionListTinggi_Tiang.IsVisible = false;
        }
        private void bordertapped(object sender, EventArgs e)
        {
            SuggestionListJenis_Lampu.IsVisible = false;
            SuggestionListJenama_Tiang.IsVisible = false;
            SuggestionListJenama_Lampu.IsVisible = false;
            SuggestionListJenis_Pintu_Penyelenggaraan.IsVisible = false;
            SuggestionListJenis_Tiang.IsVisible = false;
            SuggestionListMaterial_Tiang.IsVisible = false;
            SuggestionListPanjang_Lengan_Lampu.IsVisible = false;
            SuggestionListSaiz_Pintu_Penyelenggaraan.IsVisible = false;
            SuggestionListTinggi_Tiang.IsVisible = false;
        }
        private async void OnSaveClicked(object sender, EventArgs e)
		{
            string[] add1 = viewModel.address1Text.Split(",");
            string[] add2 = viewModel.address2Text.Split(",");
            string[] add3 = viewModel.address2Text.Split(",");
            bool flgAdd = true;
            viewModel.IsBusy=true;
            foreach (string s in add1)
            {
                if (s.Trim() == "" || s == null)
                {
                    flgAdd = false;
                }
            }
            foreach (string s in add2)
            {
                if (s.Trim() == "" || s == null)
                {
                    flgAdd = false;
                }
            }
            foreach (string s in add3)
            {
                if (s.Trim() == "" || s == null)
                {
                    flgAdd = false;
                }
            }


            if (viewModel.dropDownStringItem.Jenis_Lampu == null)
            {
                viewModel.IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Data Tidak Mencukupi", "Sila pilih jenis lampu", "OK");
                
            }
            else if (viewModel.dropDownStringItem.Tinggi_Tiang == null)
            {
                viewModel.IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Data Tidak Mencukupi", "Sila pilih tinggi tiang", "OK");
            }
            else if (viewModel.dropDownStringItem.Material_Tiang == null)
            {
                viewModel.IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Data Tidak Mencukupi", "Sila pilih material tiang", "OK");
            }
            else if (viewModel.dropDownStringItem.Jenis_Pintu_Penyelenggaraan == null)
            {
                viewModel.IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Data Tidak Mencukupi", "Sila pilih jenis pintu penyelenggaraan", "OK");
            }
            else if (viewModel.dropDownStringItem.Saiz_Pintu_Penyelenggaraan == null)
            {
                viewModel.IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Data Tidak Mencukupi", "Sila pilih saiz pintu penyelenggaraan", "OK");
            }
            else if (viewModel.dropDownStringItem.Panjang_Lengan_Lampu == null)
            {
                viewModel.IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Data Tidak Mencukupi", "Sila pilih panjang lengan lampu", "OK");
            }
            else if (viewModel.dropDownStringItem.Jenis_Tiang == null)
            {
                viewModel.IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Data Tidak Mencukupi", "Sila pilih jenis tiang", "OK");
            }
            else if (viewModel.dropDownStringItem.Jenama_Lampu == null)
            {
                viewModel.IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Data Tidak Mencukupi", "Sila pilih jenama lampu", "OK");
            }
            else if (viewModel.dropDownStringItem.Jenama_Tiang == null)
            {
                viewModel.IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Data Tidak Mencukupi", "Sila pilih jenama tiang", "OK");
            }
            else if(!flgAdd)
            {
                viewModel.IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Data Tidak Mencukupi", "Sila lengkapkan alamat", "OK");
            }
            else
            {
                InsertNewData insertNewData = new InsertNewData();
                if(viewModel.DropDownItems.Jenis_Lampu.Where(x=>x.Item==viewModel.dropDownStringItem.Jenis_Lampu).Count()==0)
                {
                    ReferenceInsertResponse result= await insertNewData.InsertReferenceAsync("jenis_lampu",viewModel.dropDownStringItem.Jenis_Lampu);
                    LookupItemString newItem= new LookupItemString();
                    newItem.Id=result.id;
                    newItem.Item = viewModel.dropDownStringItem.Jenis_Lampu;
                    App.DropDownItem.Jenis_Lampu.Add(newItem);
                    viewModel.mapPin.jenis_lampu = result.id;
                }
                else
                {
                    viewModel.mapPin.jenis_lampu=viewModel.DropDownItems.Jenis_Lampu.Where(x => x.Item == viewModel.dropDownStringItem.Jenis_Lampu).First().Id;
                }

                if (viewModel.DropDownItems.Tinggi_Tiang.Where(x => x.Item == viewModel.dropDownStringItem.Tinggi_Tiang).Count() == 0)
                {
                    ReferenceInsertResponse result = await insertNewData.InsertReferenceAsync("tinggi_tiang", viewModel.dropDownStringItem.Tinggi_Tiang.ToString());
                    LookupItemDouble newItem = new LookupItemDouble();
                    newItem.Id = result.id;
                    newItem.Item = viewModel.dropDownStringItem.Tinggi_Tiang;
                    App.DropDownItem.Tinggi_Tiang.Add(newItem);
                    viewModel.mapPin.tinggi_tiang = result.id;
                }
                else
                {
                    viewModel.mapPin.tinggi_tiang = viewModel.DropDownItems.Tinggi_Tiang.Where(x => x.Item == viewModel.dropDownStringItem.Tinggi_Tiang).First().Id;
                }

                if (viewModel.DropDownItems.Material_Tiang.Where(x => x.Item == viewModel.dropDownStringItem.Material_Tiang).Count() == 0)
                {
                    ReferenceInsertResponse result = await insertNewData.InsertReferenceAsync("material_tiang", viewModel.dropDownStringItem.Material_Tiang);
                    LookupItemString newItem = new LookupItemString();
                    newItem.Id = result.id;
                    newItem.Item = viewModel.dropDownStringItem.Material_Tiang;
                    App.DropDownItem.Material_Tiang.Add(newItem);
                    viewModel.mapPin.material_tiang = result.id;
                }
                else
                {
                    viewModel.mapPin.material_tiang = viewModel.DropDownItems.Material_Tiang.Where(x => x.Item == viewModel.dropDownStringItem.Material_Tiang).First().Id;
                }

                if (viewModel.DropDownItems.Jenis_Pintu_Penyelenggaraan.Where(x => x.Item == viewModel.dropDownStringItem.Jenis_Pintu_Penyelenggaraan).Count() == 0)
                {
                    ReferenceInsertResponse result = await insertNewData.InsertReferenceAsync("jenis_pintu_penyelenggaraan", viewModel.dropDownStringItem.Jenis_Pintu_Penyelenggaraan);
                    LookupItemString newItem = new LookupItemString();
                    newItem.Id = result.id;
                    newItem.Item = viewModel.dropDownStringItem.Jenis_Pintu_Penyelenggaraan;
                    App.DropDownItem.Jenis_Pintu_Penyelenggaraan.Add(newItem);
                    viewModel.mapPin.jenis_pintu_penyelenggaraan = result.id;
                }
                else
                {
                    viewModel.mapPin.jenis_pintu_penyelenggaraan = viewModel.DropDownItems.Jenis_Pintu_Penyelenggaraan.Where(x => x.Item == viewModel.dropDownStringItem.Jenis_Pintu_Penyelenggaraan).First().Id;
                }


                if (viewModel.DropDownItems.Saiz_Pintu_Penyelenggaraan.Where(x => x.Item == viewModel.dropDownStringItem.Saiz_Pintu_Penyelenggaraan).Count() == 0)
                {
                    ReferenceInsertResponse result = await insertNewData.InsertReferenceAsync("saiz_pintu_penyelenggaraan", viewModel.dropDownStringItem.Saiz_Pintu_Penyelenggaraan);
                    LookupItemString newItem = new LookupItemString();
                    newItem.Id = result.id;
                    newItem.Item = viewModel.dropDownStringItem.Saiz_Pintu_Penyelenggaraan;
                    App.DropDownItem.Saiz_Pintu_Penyelenggaraan.Add(newItem);
                    viewModel.mapPin.saiz_pintu_penyelenggaraan = result.id;
                }
                else
                {
                    viewModel.mapPin.saiz_pintu_penyelenggaraan = viewModel.DropDownItems.Saiz_Pintu_Penyelenggaraan.Where(x => x.Item == viewModel.dropDownStringItem.Saiz_Pintu_Penyelenggaraan).First().Id;
                }


                if (viewModel.DropDownItems.Panjang_Lengan_Lampu.Where(x => x.Item == viewModel.dropDownStringItem.Panjang_Lengan_Lampu).Count() == 0)
                {
                    ReferenceInsertResponse result = await insertNewData.InsertReferenceAsync("panjang_lengan_lampu", viewModel.dropDownStringItem.Panjang_Lengan_Lampu.ToString());
                    LookupItemDouble newItem = new LookupItemDouble();
                    newItem.Id = result.id;
                    newItem.Item = viewModel.dropDownStringItem.Panjang_Lengan_Lampu;
                    App.DropDownItem.Panjang_Lengan_Lampu.Add(newItem);
                    viewModel.mapPin.panjang_lengan_lampu = result.id;
                }
                else
                {
                    viewModel.mapPin.panjang_lengan_lampu = viewModel.DropDownItems.Panjang_Lengan_Lampu.Where(x => x.Item == viewModel.dropDownStringItem.Panjang_Lengan_Lampu).First().Id;
                }


                if (viewModel.DropDownItems.Jenis_Tiang.Where(x => x.Item == viewModel.dropDownStringItem.Jenis_Tiang).Count() == 0)
                {
                    ReferenceInsertResponse result = await insertNewData.InsertReferenceAsync("jenis_tiang", viewModel.dropDownStringItem.Jenis_Tiang.ToString());
                    LookupItemString newItem = new LookupItemString();
                    newItem.Id = result.id;
                    newItem.Item = viewModel.dropDownStringItem.Jenis_Tiang;
                    App.DropDownItem.Jenis_Tiang.Add(newItem);
                    viewModel.mapPin.jenis_tiang = result.id;
                }
                else
                {
                    viewModel.mapPin.jenis_tiang = viewModel.DropDownItems.Jenis_Tiang.Where(x => x.Item == viewModel.dropDownStringItem.Jenis_Tiang).First().Id;
                }


                if (viewModel.DropDownItems.Jenama_Lampu.Where(x => x.Item == viewModel.dropDownStringItem.Jenama_Lampu).Count() == 0)
                {
                    ReferenceInsertResponse result = await insertNewData.InsertReferenceAsync("jenama_lampu", viewModel.dropDownStringItem.Jenama_Lampu.ToString());
                    LookupItemString newItem = new LookupItemString();
                    newItem.Id = result.id;
                    newItem.Item = viewModel.dropDownStringItem.Jenama_Lampu;
                    App.DropDownItem.Jenama_Lampu.Add(newItem);
                    viewModel.mapPin.jenama_lampu = result.id;
                }
                else
                {
                    viewModel.mapPin.jenama_lampu = viewModel.DropDownItems.Jenama_Lampu.Where(x => x.Item == viewModel.dropDownStringItem.Jenama_Lampu).First().Id;
                }

                if (viewModel.DropDownItems.Jenama_Tiang.Where(x => x.Item == viewModel.dropDownStringItem.Jenama_Tiang).Count() == 0)
                {
                    ReferenceInsertResponse result = await insertNewData.InsertReferenceAsync("jenama_tiang", viewModel.dropDownStringItem.Jenama_Tiang.ToString());
                    LookupItemString newItem = new LookupItemString();
                    newItem.Id = result.id;
                    newItem.Item = viewModel.dropDownStringItem.Jenama_Tiang;
                    App.DropDownItem.Jenama_Tiang.Add(newItem);
                    viewModel.mapPin.jenama_tiang = result.id;
                }
                else
                {
                    viewModel.mapPin.jenama_tiang = viewModel.DropDownItems.Jenama_Tiang.Where(x => x.Item == viewModel.dropDownStringItem.Jenama_Tiang).First().Id;
                }
                // viewModel.mapPin.jenis_lampu = viewModel.dropDownSelectedItem.Jenis_Lampu.Id;
                //viewModel.mapPin.tinggi_tiang = viewModel.dropDownSelectedItem.Tinggi_Tiang.Id;
                //viewModel.mapPin.material_tiang = viewModel.dropDownSelectedItem.Material_Tiang.Id;
                //viewModel.mapPin.jenis_pintu_penyelenggaraan = viewModel.dropDownSelectedItem.Jenis_Pintu_Penyelenggaraan.Id;
                //viewModel.mapPin.saiz_pintu_penyelenggaraan = viewModel.dropDownSelectedItem.Saiz_Pintu_Penyelenggaraan.Id;
                // viewModel.mapPin.panjang_lengan_lampu = viewModel.dropDownSelectedItem.Panjang_Lengan_Lampu.Id;
                // viewModel.mapPin.jenis_tiang = viewModel.dropDownSelectedItem.Jenis_Tiang.Id;
                //viewModel.mapPin.jenama_lampu = viewModel.dropDownSelectedItem.Jenama_Lampu.Id;
                //viewModel.mapPin.jenama_tiang = viewModel.dropDownSelectedItem.Jenama_Tiang.Id;
                MapPinPopUpData mapPinPopUpData = new MapPinPopUpData();
                mapPinPopUpData.mapPin = viewModel.mapPin;
                mapPinPopUpData.IsUpdate = viewModel.IsUpdate;
                mapPinPopUpData.IsDelete = false;
                viewModel.IsBusy = false;
                this.Close(mapPinPopUpData);
            }

        }
        private async void OnCancelClicked(object sender, EventArgs e)
        {
            this.Close();
        }
        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            MapPinPopUpData mapPinPopUpData = new MapPinPopUpData();
            mapPinPopUpData.mapPin = viewModel.mapPin;
            mapPinPopUpData.IsUpdate = viewModel.IsUpdate;
            mapPinPopUpData.IsDelete = true;
            this.Close(mapPinPopUpData);
        }

        private void OnEntryTextChangedJenis_Lampu(object sender, TextChangedEventArgs e)
        {
            var keyword = e.NewTextValue.ToLower();
            var filtered = viewModel.DropDownItems.Jenis_Lampu.Where(x => x.Item.ToLower().Contains(keyword)).ToList();

            SuggestionListJenis_Lampu.ItemsSource = filtered;
            SuggestionListJenis_Lampu.IsVisible = filtered.Any();
        }
        private void OnEntryTappedJenis_Lampu(object sender, EventArgs e)
        {
            if(SuggestionListJenis_Lampu.IsVisible)
            {
                SuggestionListJenis_Lampu.IsVisible = false;         
                SuggestionListJenama_Tiang.IsVisible = false;
                SuggestionListJenama_Lampu.IsVisible = false;
                SuggestionListJenis_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListJenis_Tiang.IsVisible = false;
                SuggestionListMaterial_Tiang.IsVisible = false;
                SuggestionListPanjang_Lengan_Lampu.IsVisible = false;
                SuggestionListSaiz_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListTinggi_Tiang.IsVisible = false;
            }
            else
            {
                SuggestionListJenis_Lampu.IsVisible = true;

              
                SuggestionListJenama_Tiang.IsVisible = false;
                SuggestionListJenama_Lampu.IsVisible = false;
                SuggestionListJenis_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListJenis_Tiang.IsVisible = false;
                SuggestionListMaterial_Tiang.IsVisible = false;
                SuggestionListPanjang_Lengan_Lampu.IsVisible = false;
                SuggestionListSaiz_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListTinggi_Tiang.IsVisible = false;
            }
            
        }
        private void OnSuggestionSelectedJenis_Lampu(object sender, SelectionChangedEventArgs e)
        {
            LookupItemString selectedItem = e.CurrentSelection.FirstOrDefault() as LookupItemString;

            CustomPickerEntryJenis_Lampu.Text = selectedItem.Item;
            SuggestionListJenis_Lampu.IsVisible = false;
           
        }

        private void OnEntryTextChangedJenama_Lampu(object sender, TextChangedEventArgs e)
        {
            var keyword = e.NewTextValue.ToLower();
            var filtered = viewModel.DropDownItems.Jenama_Lampu.Where(x => x.Item.ToLower().Contains(keyword)).ToList();

            SuggestionListJenama_Lampu.ItemsSource = filtered;
            SuggestionListJenama_Lampu.IsVisible = filtered.Any();
        }
        private void OnEntryTappedJenama_Lampu(object sender, EventArgs e)
        {
            if (SuggestionListJenama_Lampu.IsVisible)
            {
                SuggestionListJenis_Lampu.IsVisible = false;
                SuggestionListJenama_Tiang.IsVisible = false;
                SuggestionListJenama_Lampu.IsVisible = false;
                SuggestionListJenis_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListJenis_Tiang.IsVisible = false;
                SuggestionListMaterial_Tiang.IsVisible = false;
                SuggestionListPanjang_Lengan_Lampu.IsVisible = false;
                SuggestionListSaiz_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListTinggi_Tiang.IsVisible = false;
            }
            else
            {
                SuggestionListJenama_Lampu.IsVisible = true;

                SuggestionListJenis_Lampu.IsVisible = false;
                SuggestionListJenama_Tiang.IsVisible = false;
 
                SuggestionListJenis_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListJenis_Tiang.IsVisible = false;
                SuggestionListMaterial_Tiang.IsVisible = false;
                SuggestionListPanjang_Lengan_Lampu.IsVisible = false;
                SuggestionListSaiz_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListTinggi_Tiang.IsVisible = false;
            }

        }
        private void OnSuggestionSelectedJenama_Lampu(object sender, SelectionChangedEventArgs e)
        {
            LookupItemString selectedItem = e.CurrentSelection.FirstOrDefault() as LookupItemString;

            CustomPickerEntryJenama_Lampu.Text = selectedItem.Item;
            SuggestionListJenama_Lampu.IsVisible = false;

        }

        private void OnEntryTextChangedPanjang_Lengan_Lampu(object sender, TextChangedEventArgs e)
        {
            if(e.NewTextValue!=null)
            {
                var keyword = e.NewTextValue.ToLower();
                var filtered = viewModel.DropDownItems.Panjang_Lengan_Lampu.Where(x => x.Item.ToString().Contains(keyword)).ToList();

                SuggestionListPanjang_Lengan_Lampu.ItemsSource = filtered;
                SuggestionListPanjang_Lengan_Lampu.IsVisible = filtered.Any();
            }

        }
        private void OnEntryTappedPanjang_Lengan_Lampu(object sender, EventArgs e)
        {
            if (SuggestionListPanjang_Lengan_Lampu.IsVisible)
            {
                SuggestionListPanjang_Lengan_Lampu.IsVisible = false;

                SuggestionListJenis_Lampu.IsVisible = false;
                SuggestionListJenama_Tiang.IsVisible = false;
                SuggestionListJenama_Lampu.IsVisible = false;
                SuggestionListJenis_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListJenis_Tiang.IsVisible = false;
                SuggestionListMaterial_Tiang.IsVisible = false;
           
                SuggestionListSaiz_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListTinggi_Tiang.IsVisible = false;
            }
            else
            {
                SuggestionListPanjang_Lengan_Lampu.IsVisible = true;

                SuggestionListJenis_Lampu.IsVisible = false;
                SuggestionListJenama_Tiang.IsVisible = false;
                SuggestionListJenama_Lampu.IsVisible = false;
                SuggestionListJenis_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListJenis_Tiang.IsVisible = false;
                SuggestionListMaterial_Tiang.IsVisible = false;
           
                SuggestionListSaiz_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListTinggi_Tiang.IsVisible = false;
            }

        }
        private void OnSuggestionSelectedPanjang_Lengan_Lampu(object sender, SelectionChangedEventArgs e)
        {
            LookupItemDouble selectedItem = e.CurrentSelection.FirstOrDefault() as LookupItemDouble;

            CustomPickerEntryPanjang_Lengan_Lampu.Text = selectedItem.Item.ToString();
            SuggestionListPanjang_Lengan_Lampu.IsVisible = false;

        }


        private void OnEntryTextChangedTinggi_Tiang(object sender, TextChangedEventArgs e)
        {
            if(e.NewTextValue!=null)
            {
                var keyword = e.NewTextValue.ToLower();
                var filtered = viewModel.DropDownItems.Tinggi_Tiang.Where(x => x.Item.ToString().Contains(keyword)).ToList();

                SuggestionListTinggi_Tiang.ItemsSource = filtered;
                SuggestionListTinggi_Tiang.IsVisible = filtered.Any();
            }

        }
        private void OnEntryTappedTinggi_Tiang(object sender, EventArgs e)
        {
            if (SuggestionListTinggi_Tiang.IsVisible)
            {
                SuggestionListTinggi_Tiang.IsVisible = false;


            }
            else
            {
                SuggestionListTinggi_Tiang.IsVisible = true;

                SuggestionListJenis_Lampu.IsVisible = false;
                SuggestionListJenama_Tiang.IsVisible = false;
                SuggestionListJenama_Lampu.IsVisible = false;
                SuggestionListJenis_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListJenis_Tiang.IsVisible = false;
                SuggestionListMaterial_Tiang.IsVisible = false;
                SuggestionListPanjang_Lengan_Lampu.IsVisible = false;
                SuggestionListSaiz_Pintu_Penyelenggaraan.IsVisible = false;
             

            }

        }
        private void OnSuggestionSelectedTinggi_Tiang(object sender, SelectionChangedEventArgs e)
        {
            LookupItemDouble selectedItem = e.CurrentSelection.FirstOrDefault() as LookupItemDouble;

            CustomPickerEntryTinggi_Tiang.Text = selectedItem.Item.ToString();
            SuggestionListTinggi_Tiang.IsVisible = false;

        }

        private void OnEntryTextChangedJenis_Tiang(object sender, TextChangedEventArgs e)
        {
            var keyword = e.NewTextValue.ToLower();
            var filtered = viewModel.DropDownItems.Jenis_Tiang.Where(x => x.Item.ToLower().Contains(keyword)).ToList();

            SuggestionListJenis_Tiang.ItemsSource = filtered;
            SuggestionListJenis_Tiang.IsVisible = filtered.Any();
        }
        private void OnEntryTappedJenis_Tiang(object sender, EventArgs e)
        {
            if (SuggestionListJenis_Tiang.IsVisible)
            {
                SuggestionListJenis_Tiang.IsVisible = false;
            }
            else
            {
                SuggestionListJenis_Tiang.IsVisible = true;

                SuggestionListJenis_Lampu.IsVisible = false;
                SuggestionListJenama_Tiang.IsVisible = false;
                SuggestionListJenama_Lampu.IsVisible = false;
                SuggestionListJenis_Pintu_Penyelenggaraan.IsVisible = false;
                
                SuggestionListMaterial_Tiang.IsVisible = false;
                SuggestionListPanjang_Lengan_Lampu.IsVisible = false;
                SuggestionListSaiz_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListTinggi_Tiang.IsVisible = false;
            }

        }
        private void OnSuggestionSelectedJenis_Tiang(object sender, SelectionChangedEventArgs e)
        {
            LookupItemString selectedItem = e.CurrentSelection.FirstOrDefault() as LookupItemString;

            CustomPickerEntryJenis_Tiang.Text = selectedItem.Item;
            SuggestionListJenis_Tiang.IsVisible = false;

        }

        private void OnEntryTextChangedMaterial_Tiang(object sender, TextChangedEventArgs e)
        {
            var keyword = e.NewTextValue.ToLower();
            var filtered = viewModel.DropDownItems.Material_Tiang.Where(x => x.Item.ToLower().Contains(keyword)).ToList();

            SuggestionListMaterial_Tiang.ItemsSource = filtered;
            SuggestionListMaterial_Tiang.IsVisible = filtered.Any();
        }
        private void OnEntryTappedMaterial_Tiang(object sender, EventArgs e)
        {
            if (SuggestionListMaterial_Tiang.IsVisible)
            {
                SuggestionListMaterial_Tiang.IsVisible = false;
            }
            else
            {
                SuggestionListMaterial_Tiang.IsVisible = true;

                SuggestionListJenis_Lampu.IsVisible = false;
                SuggestionListJenama_Tiang.IsVisible = false;
                SuggestionListJenama_Lampu.IsVisible = false;
                SuggestionListJenis_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListJenis_Tiang.IsVisible = false;
              
                SuggestionListPanjang_Lengan_Lampu.IsVisible = false;
                SuggestionListSaiz_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListTinggi_Tiang.IsVisible = false;
            }

        }
        private void OnSuggestionSelectedMaterial_Tiang(object sender, SelectionChangedEventArgs e)
        {
            LookupItemString selectedItem = e.CurrentSelection.FirstOrDefault() as LookupItemString;

            CustomPickerEntryMaterial_Tiang.Text = selectedItem.Item;
            SuggestionListMaterial_Tiang.IsVisible = false;

        }


        private void OnEntryTextChangedJenama_Tiang(object sender, TextChangedEventArgs e)
        {
            var keyword = e.NewTextValue.ToLower();
            var filtered = viewModel.DropDownItems.Jenama_Tiang.Where(x => x.Item.ToLower().Contains(keyword)).ToList();

            SuggestionListJenama_Tiang.ItemsSource = filtered;
            SuggestionListJenama_Tiang.IsVisible = filtered.Any();
        }
        private void OnEntryTappedJenama_Tiang(object sender, EventArgs e)
        {
            if (SuggestionListJenama_Tiang.IsVisible)
            {
                SuggestionListJenama_Tiang.IsVisible = false;
            }
            else
            {
                SuggestionListJenama_Tiang.IsVisible = true;

                SuggestionListJenis_Lampu.IsVisible = false;
            
                SuggestionListJenama_Lampu.IsVisible = false;
                SuggestionListJenis_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListJenis_Tiang.IsVisible = false;
                SuggestionListMaterial_Tiang.IsVisible = false;
                SuggestionListPanjang_Lengan_Lampu.IsVisible = false;
                SuggestionListSaiz_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListTinggi_Tiang.IsVisible = false;
            }

        }
        private void OnSuggestionSelectedJenama_Tiang(object sender, SelectionChangedEventArgs e)
        {
            LookupItemString selectedItem = e.CurrentSelection.FirstOrDefault() as LookupItemString;

            CustomPickerEntryJenama_Tiang.Text = selectedItem.Item;
            SuggestionListJenama_Tiang.IsVisible = false;

        }


        private void OnEntryTextChangedJenis_Pintu_Penyelenggaraan(object sender, TextChangedEventArgs e)
        {
            var keyword = e.NewTextValue.ToLower();
            var filtered = viewModel.DropDownItems.Jenis_Pintu_Penyelenggaraan.Where(x => x.Item.ToLower().Contains(keyword)).ToList();

            SuggestionListJenis_Pintu_Penyelenggaraan.ItemsSource = filtered;
            SuggestionListJenis_Pintu_Penyelenggaraan.IsVisible = filtered.Any();
        }
        private void OnEntryTappedJenis_Pintu_Penyelenggaraan(object sender, EventArgs e)
        {
            if (SuggestionListJenis_Pintu_Penyelenggaraan.IsVisible)
            {
                SuggestionListJenis_Pintu_Penyelenggaraan.IsVisible = false;
            }
            else
            {
                SuggestionListJenis_Pintu_Penyelenggaraan.IsVisible = true;

                SuggestionListJenis_Lampu.IsVisible = false;
                SuggestionListJenama_Tiang.IsVisible = false;
                SuggestionListJenama_Lampu.IsVisible = false;
              
                SuggestionListJenis_Tiang.IsVisible = false;
                SuggestionListMaterial_Tiang.IsVisible = false;
                SuggestionListPanjang_Lengan_Lampu.IsVisible = false;
                SuggestionListSaiz_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListTinggi_Tiang.IsVisible = false;
            }

        }
        private void OnSuggestionSelectedJenis_Pintu_Penyelenggaraan(object sender, SelectionChangedEventArgs e)
        {
            LookupItemString selectedItem = e.CurrentSelection.FirstOrDefault() as LookupItemString;

            CustomPickerEntryJenis_Pintu_Penyelenggaraan.Text = selectedItem.Item;
            SuggestionListJenis_Pintu_Penyelenggaraan.IsVisible = false;

        }


        private void OnEntryTextChangedSaiz_Pintu_Penyelenggaraan(object sender, TextChangedEventArgs e)
        {
            var keyword = e.NewTextValue.ToLower();
            var filtered = viewModel.DropDownItems.Saiz_Pintu_Penyelenggaraan.Where(x => x.Item.ToLower().Contains(keyword)).ToList();

            SuggestionListSaiz_Pintu_Penyelenggaraan.ItemsSource = filtered;
            SuggestionListSaiz_Pintu_Penyelenggaraan.IsVisible = filtered.Any();
        }
        private void OnEntryTappedSaiz_Pintu_Penyelenggaraan(object sender, EventArgs e)
        {
            if (SuggestionListSaiz_Pintu_Penyelenggaraan.IsVisible)
            {
                SuggestionListSaiz_Pintu_Penyelenggaraan.IsVisible = false;
            }
            else
            {
                SuggestionListSaiz_Pintu_Penyelenggaraan.IsVisible = true;
                SuggestionListJenis_Lampu.IsVisible = false;
                SuggestionListJenama_Tiang.IsVisible = false;
                SuggestionListJenama_Lampu.IsVisible = false;
                SuggestionListJenis_Pintu_Penyelenggaraan.IsVisible = false;
                SuggestionListJenis_Tiang.IsVisible = false;
                SuggestionListMaterial_Tiang.IsVisible = false;
                SuggestionListPanjang_Lengan_Lampu.IsVisible = false;
               
                SuggestionListTinggi_Tiang.IsVisible = false;
            }

        }
        private void OnSuggestionSelectedSaiz_Pintu_Penyelenggaraan(object sender, SelectionChangedEventArgs e)
        {
            LookupItemString selectedItem = e.CurrentSelection.FirstOrDefault() as LookupItemString;

            CustomPickerEntrySaiz_Pintu_Penyelenggaraan.Text = selectedItem.Item;
            SuggestionListSaiz_Pintu_Penyelenggaraan.IsVisible = false;

        }
    }
}

