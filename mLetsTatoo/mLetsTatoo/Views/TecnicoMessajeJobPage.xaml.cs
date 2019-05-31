namespace mLetsTatoo.Views
{
    using ViewModels;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Syncfusion.XForms.Buttons;
    using mLetsTatoo.Helpers;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TecnicoMessajeJobPage : TabbedPage
    {
        #region Attributes
        private decimal time;
        private string stringtime;
        public bool pageVisible;
        private decimal CostAddedComision;
        private decimal AdvanceAddedComision;
        private Grid _grid;
        #endregion

        #region Constructors
        public TecnicoMessajeJobPage()
        {

            InitializeComponent();
            this.CurrentPage = this.Messages;
            this.Message.IsChecked = true;
            this.BarBackgroundColor = Color.FromRgb(20, 20, 20);
            this.BarTextColor = Color.FromRgb(200, 200, 200);
            this.CurrentPageChanged += (object sender, EventArgs e) =>
            {

                var i = this.Children.IndexOf(this.CurrentPage);
                this.JobInfo.Icon = "InfoUns.png";
                this.Messages.Icon = "CitasUns.png";

                switch (i)
                {
                    case 0:
                        this.Messages.Icon = "Citas.png";
                        break;
                    case 1:
                        this.JobInfo.Icon = "info.png";
                        break;
                }

            };
        }
        #endregion

        #region Methods
        private void LoadPage(object sender, StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                var pageButton = (SfRadioButton)sender;
                if (pageButton == this.Budget)
                {
                    this.pageVisible = true;
                    this.BudgetStack.IsVisible = true;
                    this.StackMessage.IsVisible = false;
                    this.Budget.TextColor = Color.LightGray;
                    this.Message.TextColor = Color.Gray;
                }
                else if (pageButton == this.Message)
                {
                    this.pageVisible = false;
                    this.BudgetStack.IsVisible = false;
                    this.StackMessage.IsVisible = true;
                    this.Message.TextColor = Color.LightGray;
                    this.Budget.TextColor = Color.Gray;
                }
            }
        }
        private void OnGridSelect(object s, EventArgs e)
        {
            if (_grid != null)
            {
                _grid.BackgroundColor = Color.Transparent;
            }

            var button = (Grid)s;
            button.BackgroundColor = Color.DimGray;
            _grid = button;
        }
        public async void OnTapGestureRecognizerLabel(object sender, EventArgs e)
        {
            var timeSender = (Label)sender;
            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.SelectEstimatedTime,
                Languages.Cancel,
                null,
                "30 mins",
                "1 hr",
                "1 hr 30 mins",
                "2 hrs",
                "2 hrs 30 mins",
                "3 hrs");

            if (source == Languages.Cancel)
            {
                return;
            }

            if (source == "30 mins")
            {
                this.time = 30;
                this.stringtime = "30 mins";
            }
            else if (source == "1 hr")
            {
                this.time = 60;
                this.stringtime = "1 hr";
            }
            else if (source == "1 hr 30 mins")
            {
                this.time = 90;
                this.stringtime = "1 hr 30 mins";
            }
            else if (source == "2 hrs")
            {
                this.time = 120;
                this.stringtime = "2 hrs";
            }
            else if (source == "2 hrs 30 mins")
            {
                this.time = 150;
                this.stringtime = "2 hrs 30 mins";
            }
            else if (source == "3 hrs")
            {
                this.time = 180;
                this.stringtime = "3 hrs";
            }

            if (!string.IsNullOrEmpty(stringtime))
            {
                timeSender.Text = stringtime;
            }

            MainViewModel.GetInstance().TecnicoMessageJob.Time = this.time.ToString();
        }
        private void ConvertTime(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var c = (Label)sender;
            if (c.Text == "30")
            {
                c.Text = "30 mins";
            }
            else if (c.Text == "60")
            {
                c.Text = "1 hr";
            }
            else if (c.Text == "90")
            {
                c.Text = "1 hr 30 mins";
            }
            else if (c.Text == "120")
            {
                c.Text = "2 hrs";
            }
            else if (c.Text == "150")
            {
                c.Text = "2 hrs 30 mins";
            }
            else if (c.Text == "180")
            {
                c.Text = "3 hrs";
            }
        }
        private void AddComision(object sender, FocusEventArgs e)
        {
            var entry = (Entry)sender;
            if (!string.IsNullOrEmpty(entry.Text))
            {
                decimal a = decimal.Parse(entry.Text);

                if (entry == this.Cost)
                {
                    if (a != CostAddedComision || a >= 0)
                    {
                        if (e.IsFocused == false)
                        {
                            entry.Text = (a + 50).ToString();
                            this.CostAddedComision = a + 50;
                        }
                    }
                }

                if (entry == this.Advance)
                {
                    if (a != AdvanceAddedComision || a >= 0)
                    {
                        if (e.IsFocused == false)
                        {
                            entry.Text = (a + 50).ToString();
                            this.AdvanceAddedComision = a + 50;
                            this.Cost.Text = null;
                        }
                    }
                }
            }
        }
        #endregion

        private void SendBudget(object sender, EventArgs e)
        {
            this.Message.IsChecked = true;
        }
    }
}