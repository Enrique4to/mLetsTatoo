namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Linq;
    using Models;
    using Services;
    using Plugin.Media.Abstractions;
    using Xamarin.Forms;
    using System.IO;
    using ExifLib;
    using System.Collections.ObjectModel;

    public class PublicacionesItemViewModel : PublicacionesCollection
    {
        #region Services
        private ApiService apiService;
        #endregion
        #region Attributes
        private Image file;
        private Image file2;
        #endregion
        #region Properties
        public ObservableCollection<ImgPublicacionItemViewModel> Imagenes1 { get; set; }

        #region OneImageStack
        public bool One_Image { get; set; }
        public Image One_Image_One { get; set; }
        #endregion
        #region TwoImageVerticalStack
        public bool Two_Image_Ver { get; set; }
        public Image Two_Image_One_Ver { get; set; }
        public Image Two_Image_Two_Ver { get; set; }
        #endregion
        #region TwoImageHorizontalStack
        public bool Two_Image_Hor { get; set; }
        public Image Two_Image_One_Hor { get; set; }
        public Image Two_Image_Two_Hor { get; set; }
        #endregion
        #region TwoImageDiferentStack
        public bool Two_Image_Dif { get; set; }
        public Image Two_Image_One_Dif { get; set; }
        public Image Two_Image_Two_Dif { get; set; }
        #endregion
        #region ThreeImageHorizontalStack
        public bool Three_Image_Hor { get; set; }
        public Image Three_Image_One_Hor { get; set; }
        public Image Three_Image_Two_Hor { get; set; }
        public Image Three_Image_Three_Hor { get; set; }
        #endregion
        #region ThreeImageVerticalStack
        public bool Three_Image_Ver { get; set; }
        public Image Three_Image_One_Ver { get; set; }
        public Image Three_Image_Two_Ver { get; set; }
        public Image Three_Image_Three_Ver { get; set; }
        #endregion
        #region FourImageStack
        public bool Four_Image { get; set; }
        public Image Four_Image_One { get; set; }
        public Image Four_Image_Two { get; set; }
        public Image Four_Image_Three { get; set; }
        public Image Four_Image_Four { get; set; } 
        #endregion

        #endregion
        #region Constructor
        public PublicacionesItemViewModel()
        {
            this.apiService = new ApiService();
            this.One_Image = false;
            this.Two_Image_Hor = false;
            this.Two_Image_Ver = false;
            this.Two_Image_Dif = false;
            this.Three_Image_Hor = false;
            this.Three_Image_Ver = false;
            this.Four_Image = false;
            this.LoadImages();
        }
        #endregion
        #region Commands

        #endregion
        #region Methods


        private void LoadImages()
        {

            //this.Imagenes = MainViewModel.GetInstance().Login.Imagenes.Select(i => new T_imgpublicacion
            //{
            //    Id_Publicacion = i.Id_Publicacion,
            //    Id_Usuario = i.Id_Usuario,
            //    Id_Imgpublicacion = i.Id_Imgpublicacion,
            //    Imagen = i.Imagen,

            //}).ToList();

            if (this.Imagenes != null)
            {
                if (this.Imagenes.Count == 1)
                {
                    this.One_Image= true;
                    this.Two_Image_Hor= false;
                    this.Two_Image_Ver = false;
                    this.Two_Image_Dif = false;
                    this.Three_Image_Hor = false;
                    this.Three_Image_Ver = false;
                    this.Four_Image = false;
                    this.One_Image_One.Source = ImageSource.FromStream(() => new MemoryStream(this.Imagenes.ElementAtOrDefault(0).Imagen));
                }
                if (this.Imagenes.Count == 2)
                {
                    this.One_Image = false;
                    this.Three_Image_Hor = false;
                    this.Three_Image_Ver = false;
                    this.Four_Image = false;
                    this.file.Source = ImageSource.FromStream(() => new MemoryStream(this.Imagenes.ElementAtOrDefault(0).Imagen));
                    this.file2.Source = ImageSource.FromStream(() => new MemoryStream(this.Imagenes.ElementAtOrDefault(1).Imagen));
                    //var fileInfo1 = ExifReader.ReadJpeg(file);
                    //var fileInfo2 = ExifReader.ReadJpeg(publicacion.Imagenes.ElementAtOrDefault(1).GetStream());
                    if (this.file.Width > this.file.Height && this.file2.Width > this.file2.Height)
                    {
                        this.Two_Image_Hor = true;
                        this.Two_Image_Ver = false;
                        this.Two_Image_Dif = false;
                        this.Two_Image_One_Hor = this.file;
                        this.Two_Image_Two_Hor = this.file2;
                        //Two_Hor
                    }
                    else if (this.file.Width < this.file.Height && this.file2.Width < this.file2.Height)
                    {
                        this.Two_Image_Hor = false;
                        this.Two_Image_Ver = true;
                        this.Two_Image_Dif = false;
                        this.Two_Image_One_Ver = this.file;
                        this.Two_Image_Two_Ver = this.file2;
                        //Two_Ver
                    }
                    else if (
                        (this.file.Width < this.file.Height && this.file2.Width > this.file2.Height)
                        || (this.file.Width > this.file.Height && this.file2.Width < this.file2.Height))
                    {
                        this.Two_Image_Hor = false;
                        this.Two_Image_Ver = false;
                        this.Two_Image_Dif = true;
                        this.Two_Image_One_Dif = this.file;
                        this.Two_Image_Two_Dif = this.file2;
                        //Two_Dif
                    }
                }
                if (this.Imagenes.Count == 3)
                {
                    this.One_Image = false;
                    this.Two_Image_Hor = false;
                    this.Two_Image_Ver = false;
                    this.Two_Image_Dif = false;
                    this.Four_Image = false;

                    this.file.Source = ImageSource.FromStream(() => new MemoryStream(this.Imagenes.ElementAtOrDefault(0).Imagen));

                    if (this.file.Width > this.file.Height)
                    {
                        this.Three_Image_Hor = true;
                        this.Three_Image_Ver = false;

                        this.Three_Image_One_Hor.Source = ImageSource.FromStream(() => new MemoryStream(this.Imagenes.ElementAtOrDefault(0).Imagen));

                        this.Three_Image_Two_Hor.Source = ImageSource.FromStream(() => new MemoryStream(this.Imagenes.ElementAtOrDefault(1).Imagen));

                        this.Three_Image_Three_Hor.Source = ImageSource.FromStream(() => new MemoryStream(this.Imagenes.ElementAtOrDefault(2).Imagen));
                    }
                    else
                    {
                        this.Three_Image_Hor = false;
                        this.Three_Image_Ver = true;

                        this.Three_Image_One_Ver.Source = ImageSource.FromStream(() => new MemoryStream(this.Imagenes.ElementAtOrDefault(0).Imagen));

                        this.Three_Image_Two_Ver.Source = ImageSource.FromStream(() => new MemoryStream(this.Imagenes.ElementAtOrDefault(1).Imagen));

                        this.Three_Image_Three_Ver.Source = ImageSource.FromStream(() => new MemoryStream(this.Imagenes.ElementAtOrDefault(2).Imagen));
                    }
                }
                if (this.Imagenes.Count > 3)
                {
                    this.One_Image = false;
                    this.Two_Image_Hor = false;
                    this.Two_Image_Ver = false;
                    this.Two_Image_Dif = false;
                    this.Three_Image_Hor = false;
                    this.Three_Image_Ver = false;
                    this.Four_Image = true;
                    this.Four_Image_One.Source = ImageSource.FromStream(() => new MemoryStream(this.Imagenes.ElementAtOrDefault(0).Imagen));
                    this.Four_Image_Two.Source = ImageSource.FromStream(() => new MemoryStream(this.Imagenes.ElementAtOrDefault(1).Imagen));
                    this.Four_Image_Three.Source = ImageSource.FromStream(() => new MemoryStream(this.Imagenes.ElementAtOrDefault(2).Imagen));
                    this.Four_Image_Four.Source = ImageSource.FromStream(() => new MemoryStream(this.Imagenes.ElementAtOrDefault(3).Imagen));
                }
            }
            else
            {
                this.file = null;
                this.file2 = null;

                this.One_Image = false;
                this.Two_Image_Hor = false;
                this.Two_Image_Ver = false;
                this.Two_Image_Dif = false;
                this.Three_Image_Hor = false;
                this.Three_Image_Ver = false;
                this.Four_Image = false;
            }
            
        }
        #endregion
    }
}
