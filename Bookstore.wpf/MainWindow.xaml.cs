using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;

namespace Bookstore.wpf
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            using var db = new BookstoreContext();

            //var böcker = db.Böckers.ToList();
            
            //var lagersaldo = db.LagerSaldos.ToList();

            var butiker = db.Butikers
                .Include(butiker => butiker.LagerSaldos)
                .ToList();

            myDataGrid.ItemsSource = new ObservableCollection<Butiker>(butiker);
        }
    }
}