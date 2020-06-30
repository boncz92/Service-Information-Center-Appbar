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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using WpfAppBar;
using System.Diagnostics;
using System.IO;
//https://github.com/rafallopatka/ToastNotifications Toast Notifications Github
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using MaterialDesignThemes.Wpf;
using System.Linq.Expressions;

namespace SiC_Appbar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Make the window an appbar
        }
        bool UserFound { get; set; }
        string UserID { get; set; }

        //Notification Settings
        readonly Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new PrimaryScreenPositionProvider(
                corner: Corner.BottomRight,
                offsetX: 10,
                offsetY: 45);
            cfg.DisplayOptions.TopMost = true;
            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(5),
                maximumNotificationCount: MaximumNotificationCount.FromCount(3));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.Appbar_Orientation == "Top")
            {
                AppBarFunctions.SetAppBar(this, ABEdge.Top);
            }
            if (Properties.Settings.Default.Appbar_Orientation == "Bottom")
            {
                AppBarFunctions.SetAppBar(this, ABEdge.Bottom);
            }
        }
        void MainWindow_Closed(object sender, CancelEventArgs e)
        {
            AppBarFunctions.SetAppBar(this, ABEdge.None);
        }

        private void MenuPopupClose_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            notifier.Dispose();
        }
        private void MenuPopupSettings_OnClick(object sender, RoutedEventArgs e)
        {
            Settings_Menu settingsMenu = new Settings_Menu();
            settingsMenu.ShowDialog();
        }
        private void Search_Event(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                ActiveDirectoryQuery(command: "query");
            }
        }
        private void MoreInfo_Click(object sender, RoutedEventArgs e)
        {
            //debug line
            //UserFound = true;
            //UserID = "Testing Yo";
            if (UserFound == true)
            {
                ActiveDirectory_MoreInfo aDMoreInfo = new ActiveDirectory_MoreInfo(userID: UserID);
                aDMoreInfo.ShowDialog();
            }
            else
            {
                notifier.ShowError("No user Querried");
            }

        }
        private void RefreshData_Click(object sender, RoutedEventArgs e)
        {
            ActiveDirectoryQuery(command: "query");
        }
        private void CopyUserID_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(SearchBox.Text.Trim());
        }
        private void PersonLookupButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text.Trim() != "")
            {
                System.Diagnostics.Process.Start("chrome.exe", "https://personlookup/details/" + SearchBox.Text.Trim());
            }
            else
            {
                notifier.ShowError("No text entered in searchbox");
            }
        }
        private void ResetPassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ActiveDirectoryQuery(command: "resetpw");

        }
        private void ResetPassword_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ActiveDirectoryQuery(command: "resetpwtimer");
        }
        private void DisabledUserTool_Button_Click(object sender, RoutedEventArgs e)
        {
            Process sDTool = new Process();
            sDTool.StartInfo.FileName = @"DomainSpecificApp";
            sDTool.Start();
        }
        private void Unlock_Click(object sender, RoutedEventArgs e)
        {
            ActiveDirectoryQuery(command: "unlock");
        }
        private void ClearData_Click(object sender, RoutedEventArgs e)
        {
            adResults_Enabled.Content = "";
            adResults_Locked.Content = "";
            adResults_Name.Content = "";
            adResults_PWExpDate.Content = "";
            adResults_PWLastSet.Content = "";
            adResults_LastBadPw.Content = "";
            adResults_AccExpDate.Content = "";
            adResults_Derpartment.Content = "";
            adResults_JobTitle.Content = "";
            SearchBox.Text = "";
            DisabledUserTool_Button.IsEnabled = false;
            UserID = null;
            UserFound = false;
        }
        private Int64 LongFromLargeInteger(object largeInteger)
        {
            System.Type ltype = largeInteger.GetType();
            Int32 highPart = (Int32)ltype.
            InvokeMember("HighPart", System.Reflection.BindingFlags.GetProperty, null, largeInteger, null);
            Int32 lowPart = (Int32)ltype.InvokeMember("LowPart", System.Reflection.BindingFlags.GetProperty, null, largeInteger, null);
            return ((Int64)highPart << 32) | (UInt32)lowPart;
        }
        private void ActiveDirectoryQuery(string command)
        {
            if (SearchBox.Text.Trim() != "")
            {
                //http://www.utools.nl/downloads/UnlockADDS.pdf
                using (DirectoryEntry directoryEntry = new DirectoryEntry(Properties.Settings.Default.Domain_Name))
                {
                    DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry)
                    {
                        Filter = "(samaccountname=" + SearchBox.Text.Trim() + ")"
                    };
                    SearchResult searchResult = directorySearcher.FindOne();
                    if (searchResult != null)
                    {
                        UserFound = true;
                        DirectoryEntry directoryEntryUser = searchResult.GetDirectoryEntry();
                        if (command == "query")
                        {
                            UserID = directoryEntryUser.Properties["samaccountname"][0].ToString();
                            SearchBox.Text = directoryEntryUser.Properties["samaccountname"][0].ToString();
                            adResults_Name.Content = directoryEntryUser.Properties["givenname"][0].ToString() + " " + directoryEntryUser.Properties["sn"][0].ToString();
                            adResults_JobTitle.Content = directoryEntryUser.Properties["title"][0].ToString();
                            adResults_Derpartment.Content = directoryEntryUser.Properties["department"][0].ToString();
                            //Account Enabled Status Handling
                            bool accountEnabledStatus = !Convert.ToBoolean((int)directoryEntryUser.Properties["userAccountControl"][0] & 0x0002);
                            if (accountEnabledStatus == true)
                            {
                                adResults_Enabled.Content = "Enabled";
                                adResults_Enabled.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF0CFF00");
                                DisabledUserTool_Button.IsEnabled = false;
                            }
                            else
                            {
                                adResults_Enabled.Content = "Disabled";
                                adResults_Enabled.Foreground = Brushes.Red;
                                DisabledUserTool_Button.IsEnabled = true;
                            }
                            //Lockout Status Handling
                            try
                            {
                                Int64 accountLockoutTime = new Int64();
                                accountLockoutTime = LongFromLargeInteger(directoryEntryUser.Properties["lockoutTime"].Value);
                                if (accountLockoutTime == 0)
                                {
                                    adResults_Locked.Content = "Unlocked";
                                    adResults_Locked.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF0CFF00");
                                }
                                else
                                {
                                    adResults_Locked.Content = "Locked";
                                    adResults_Locked.Foreground = Brushes.Red;
                                }
                            }
                            catch
                            {
                                adResults_Locked.Content = "Unlocked";
                            }
                            //PWLastSet
                            Int64 pwdLastSetValue = LongFromLargeInteger(directoryEntryUser.Properties["pwdlastset"].Value);
                            string pwdLastSetDate = DateTime.FromFileTime(pwdLastSetValue).ToString("MM/dd/yyyy");
                            if (pwdLastSetDate == "12/31/1600")
                            {
                                adResults_PWLastSet.Content = "Next Logon";
                            }
                            else
                            {
                                adResults_PWLastSet.Content = pwdLastSetDate;
                            }
                            //PWExpDate
                            DateTime pwExpDate = DateTime.FromFileTime(pwdLastSetValue).AddDays(180);
                            if (pwExpDate.ToString("MM/dd/yyyy") == "06/29/1601")
                            {
                                adResults_PWExpDate.Content = "Next Logon";
                                adResults_PWExpDate.Foreground = Brushes.White;
                            }
                            if (pwExpDate < DateTime.Now)
                            {
                                adResults_PWExpDate.Content = pwExpDate.ToString("MM/dd/yyyy");
                                adResults_PWExpDate.Foreground = Brushes.Red;
                            }
                            else
                            {
                                adResults_PWExpDate.Content = pwExpDate.ToString("MM/dd/yyyy");
                                adResults_PWExpDate.Foreground = Brushes.White;
                            }
                            //PW Last Bad Time
                            Int64 pwdLastBadTime = LongFromLargeInteger(directoryEntryUser.Properties["badpasswordtime"].Value);
                            adResults_LastBadPw.Content = DateTime.FromFileTime(pwdLastBadTime).ToString("MM/dd/yyyy");
                            //Acc Exp Date
                            Int64 accExpValue = LongFromLargeInteger(directoryEntryUser.Properties["accountexpires"].Value);
                            if (accExpValue == 9223372036854775807)
                            {
                                adResults_AccExpDate.Content = "No Exp.";
                            }
                            else
                            {
                                string accExpDate = DateTime.FromFileTime(accExpValue).ToString("MM/dd/yyyy");
                                if (accExpDate == "12/31/1600")
                                {
                                    adResults_AccExpDate.Content = "No Exp.";
                                }
                                else
                                {
                                    adResults_AccExpDate.Content = accExpDate;
                                }
                            }

                        }
                        if (command == "unlock")
                        {
                            directoryEntryUser.Properties["lockouttime"].Value = 0x0000;
                            directoryEntryUser.CommitChanges();
                            ActiveDirectoryQuery(command: "query");
                            notifier.ShowSuccess("Succesfully Unlocked Account");
                        }
                        if (command == "resetpw")
                        {
                            if(Properties.Settings.Default.Password_Pref == "Static")
                            {
                                directoryEntryUser.Invoke("SetPassword", new object[] { Properties.Settings.Default.Password_String });
                                notifier.ShowSuccess("Succesfully Reset Password to " + Properties.Settings.Default.Password_String);
                            }
                            else if (Properties.Settings.Default.Password_Pref == "Random")
                            {
                                Random random = new Random();
                                var R_Word = new List<string> { "Monday", "Tuesday", "Friday", "Saturday", "Sunday"};
                                int RW_index = random.Next(R_Word.Count);
                                var R_Number = new List<string> { "1", "2", "3", "4", "5" };
                                int RN_index = random.Next(R_Number.Count);
                                var R_Special = new List<string> { "!", ".", "?"};
                                int RS_index = random.Next(R_Special.Count);

                                var RandomPW = R_Word[RW_index] + R_Number[RN_index] + R_Special[RS_index];
                                directoryEntryUser.Invoke("SetPassword", new object[] { RandomPW });
                                notifier.ShowSuccess("Succesfully Reset Password to " + RandomPW);

                            }
                            directoryEntryUser.Properties["lockoutTime"].Value = 0;
                            directoryEntryUser.Properties["pwdLastSet"].Value = 0;
                            directoryEntryUser.CommitChanges();
                            ActiveDirectoryQuery(command: "query");
                        }
                        if (command == "resetpwtimer")
                        {
                            try
                            {
                                directoryEntryUser.Properties["pwdLastSet"].Value = 0;
                                directoryEntryUser.CommitChanges();
                                directoryEntryUser.Properties["pwdLastSet"].Value = -1;
                                directoryEntryUser.CommitChanges();
                                notifier.ShowSuccess("Succesfuly reset password timer on " + UserID);
                                ActiveDirectoryQuery(command: "query");
                            }
                            catch
                            {
                                notifier.ShowError("Error Performing reset pw timer");
                            }
                        }
                    }
                    else
                    {
                        UserFound = false;
                        notifier.ShowError("No user found with ID: " + SearchBox.Text.Trim());
                    }
                }
            }
            else
            {
                notifier.ShowError("No text entered in searchbox");
            }
        }//End Function

        private void MenuPopupToggleLoc_OnClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.Appbar_Orientation == "Top")
            {
                Properties.Settings.Default.Appbar_Orientation = "Bottom";
                AppBarFunctions.SetAppBar(this, ABEdge.Bottom);
                Properties.Settings.Default.Save();
            }
            else if (Properties.Settings.Default.Appbar_Orientation == "Bottom")
            {
                Properties.Settings.Default.Appbar_Orientation = "Top";
                AppBarFunctions.SetAppBar(this, ABEdge.Top);
                Properties.Settings.Default.Save();
            }
        }
    }
}