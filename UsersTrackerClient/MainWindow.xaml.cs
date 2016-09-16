using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UsersTracker.Entities;
using UsersTracker.EnumManager;
using UsersTracker.WebService;

namespace UsersTrackerClient
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ClientProduct> CliPro;
        public ObservableCollection<Client> Cli;
        public ObservableCollection<Product> Pro;
        public WebServiceManager<ClientProduct> webServiceClientProduct;
        public WebServiceManager<Client> webServiceClient;
        public WebServiceManager<Product> webServiceProduct;

        public MainWindow()
        {
            InitializeComponent();
            webServiceClientProduct = new WebServiceManager<ClientProduct>(DataConnectionResource.LOCALAPI);
            webServiceClient = new WebServiceManager<Client>(DataConnectionResource.LOCALAPI);
            webServiceProduct = new WebServiceManager<Product>(DataConnectionResource.LOCALAPI);
            this.CliPro = new ObservableCollection<ClientProduct>();
            this.Cli = new ObservableCollection<Client>();
            this.Pro = new ObservableCollection<Product>();
            this.dataGridClientProduct.ItemsSource = CliPro;
            this.dataGridClient.ItemsSource = Cli;
            this.dataGridProduct.ItemsSource = Pro;
            SetupFromWebService();
        }

        private async void SetupFromWebService()
        {
            GridSetter<ClientProduct>(await webServiceClientProduct.Get(), this.CliPro);
            GridSetter<Client>(await webServiceClient.Get(), this.Cli);
            GridSetter<Product>(await webServiceProduct.Get(), this.Pro);
        }

        private void GridSetter<T>(List<T> list, ObservableCollection<T> to)
        {
            to.Clear();
            foreach (var item in list)
            {
                to.Add(item);
            }
        }

        private async void btn_Click(object sender, RoutedEventArgs e)
        {
            await webServiceClientProduct.Put(this.CliPro.ToList());
            await webServiceClient.Put(this.Cli.ToList());
            await webServiceProduct.Put(this.Pro.ToList());

            SetupFromWebService();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            SetupFromWebService();
        }
    }
}
