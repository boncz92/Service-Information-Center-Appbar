using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.IO;
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
    /// Interaction logic for ActiveDirectory_MoreInfo.xaml
    /// </summary>
    public partial class ActiveDirectory_MoreInfo : Window
    {
        string UserID { get; set; }

        public ActiveDirectory_MoreInfo(string userID)
        {
            InitializeComponent();
            UserID = userID;
        }
        void MoreInfoWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Title = UserID + " Extended Active Directory Information";
            using (DirectoryEntry directoryEntry = new DirectoryEntry(Properties.Settings.Default.Domain_Name))
            {
                DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry)
                {
                    Filter = "(samaccountname=" + UserID + ")"
                };
                directorySearcher.PropertiesToLoad.Clear();
                directorySearcher.PropertiesToLoad.Add("name");
                directorySearcher.PropertiesToLoad.Add("sammaccountname");
                directorySearcher.PropertiesToLoad.Add("description");
                directorySearcher.PropertiesToLoad.Add("title");
                directorySearcher.PropertiesToLoad.Add("manager");
                directorySearcher.PropertiesToLoad.Add("department");
                directorySearcher.PropertiesToLoad.Add("homedirectory");
                directorySearcher.PropertiesToLoad.Add("whenchanged");
                directorySearcher.PropertiesToLoad.Add("mail");
                directorySearcher.PropertiesToLoad.Add("memberof");
                SearchResult searchResult = directorySearcher.FindOne();
                DirectoryEntry directoryEntryUser = searchResult.GetDirectoryEntry();

                ResultPropertyCollection properties = searchResult.Properties;
                List<UserProperties> userProperties = new List<UserProperties>();
                foreach (string propertyName in properties.PropertyNames)
                {
                    foreach (var propertyValue in properties[propertyName])
                    {
                        if(propertyName != "memberof" & propertyName != "adspath")
                        {
                            userProperties.Add(new UserProperties() { Name = propertyName, Value = propertyValue.ToString() });
                        }
                    }
                }
                UserProperties_DataGrid.ItemsSource = userProperties;

                PropertyValueCollection groups = directoryEntryUser.Properties["memberof"];
                List<GroupMembership> groupMembership = new List<GroupMembership>();
                foreach (string g in groups)
                {
                    DirectorySearcher directorySearcherGroup = new DirectorySearcher
                    {
                        Filter = "(&(objectCategory=group)(distinguishedname=" + g + "))"
                    };
                    SearchResult searchResultGroup = directorySearcherGroup.FindOne();
                    groupMembership.Add(new GroupMembership() { Name = searchResultGroup.Properties["cn"][0].ToString() });
                }
                MemberOf_DataGrid.ItemsSource = groupMembership;
                
            }
            MemberOf_DataGrid.Items.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

        }
        void MoreInfoWindow_Closed(object sender, CancelEventArgs e)
        {

        }
        public class GroupMembership
        {
            public string Name { get; set; }
        }
        public class UserProperties
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}
