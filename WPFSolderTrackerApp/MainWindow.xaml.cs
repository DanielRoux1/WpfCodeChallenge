using System.Data.Entity;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Data.Linq.Helpers;
using DevExpress.Xpf.Map;
using Newtonsoft.Json;
using SoldierComponent.Models;

namespace WPFSolderTrackerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>The soldier database</summary>
        SoldierDb _SoldierDb;
        Random rand = new Random((int)DateTime.Now.Ticks);
        List<Soldier> _Soldiers;
        public MainWindow()
        {
            InitializeComponent();
        }



        /// <summary>Handles the Click event of the Button control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (chkLoadFromDB.IsChecked == true)
            {

                if (loadSoldiersFromDb())
                {
                    if (_SoldierDb.Soldiers.Count() == 0)
                    {
                        System.Windows.MessageBox.Show("Database Empty Please load from file " + _SoldierDb.Soldiers.Count());
                    }
                    else
                    {
                        dgSoldiers.ItemsSource = _Soldiers.ToArray();
                        foreach (Soldier tempSolder in _Soldiers)
                        {
                            SolderMapItems.Items.Add(new MapPushpin() { Text = tempSolder.SoldierName, Location = new GeoPoint((double)tempSolder.Latitude, (double)tempSolder.Longitude) });
                        }
                    }
                }
            }
            else
            {
                if (loadSoldierFromFile())
                {
                    //Add Markers
                    dgSoldiers.ItemsSource = _Soldiers.ToArray();
                    foreach (Soldier tempSolder in _Soldiers)
                    {
                        SolderMapItems.Items.Add(new MapPushpin() { Text = tempSolder.SoldierName, Location = new GeoPoint((double)tempSolder.Latitude, (double)tempSolder.Longitude) });
                    }
                }
            }

        }

        /// <summary>Loads the soldiers from database.</summary>
        /// <returns>
        ///   List of Soldiers
        /// </returns>
        private bool loadSoldiersFromDb()
        {
            _SoldierDb = new SoldierDb();
            _SoldierDb.Soldiers.Load();
            _Soldiers = _SoldierDb.Soldiers.ToList<Soldier>();
            return true;
        }

        /// <summary>Loads the soldier from Json file.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        private bool loadSoldierFromFile()
        {
            using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + "/Data/Soldiers.json"))
            {
                string json = r.ReadToEnd();
                _Soldiers = JsonConvert.DeserializeObject<List<Soldier>>(json);
            }
            
            return true;
        }

        /// <summary>Handles the 1 event of the Button_Click control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_Soldiers == null) return;
            if (_Soldiers.Count() > 0 )
            {
                simulateMovements();
            }
            else
            {
                System.Windows.MessageBox.Show("No Soldiers to Move, Please add them from the database or JSON File");
            }
        }

        /// <summary>Simulates the movements of the Soldiers.</summary>
        /// <returns>
        ///   True if it could Move the pins for each soldier
        /// </returns>
        private bool simulateMovements()
        {

            foreach (MapPushpin tmpPushPin in SolderMapItems.Items)
            {
                tmpPushPin.Location = new GeoPoint(rand.NextDouble() * 40, rand.NextDouble() * 40);
            }
            return true;
        }
    }
}