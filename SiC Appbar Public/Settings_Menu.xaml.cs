using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace SiC_Appbar
{
    /// <summary>
    /// Interaction logic for Settings_Menu.xaml
    /// </summary>
    public partial class Settings_Menu : Window
    {
        public Settings_Menu()
        {
            InitializeComponent();
        }
        void SettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if(Properties.Settings.Default.Password_Pref == "Static")
            {
                PwSettings_Static.IsChecked = true;
            }
            else if (Properties.Settings.Default.Password_Pref == "Random")
            {
                PwSettings_Random.IsChecked = true;
            }
            PwSettings_StaticString.Text = Properties.Settings.Default.Password_String;
        }
        void SettingsWindow_Closed(object sender, CancelEventArgs e)
        {
            if (PwSettings_Static.IsChecked == true)
            {
                Properties.Settings.Default.Password_Pref = "Static";
            }
            else if (PwSettings_Random.IsChecked == true)
            {
                Properties.Settings.Default.Password_Pref = "Random";
            }
            Properties.Settings.Default.Password_String = PwSettings_StaticString.Text;
        }
    }
}
