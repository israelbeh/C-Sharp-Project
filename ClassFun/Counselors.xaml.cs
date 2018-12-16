using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace ClassFun
{
    //allows the user to create, read, update, and delete Counselors
    /// <summary>
    /// Interaction logic for Counselors.xaml
    /// </summary>
    public partial class Counselors : Window
    {
        jeffersonEntities dbContext = new jeffersonEntities();

        public Counselors()
        {
            InitializeComponent();
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //closes the window
            this.Close();
        }
        private void Counselors_Loaded(object sender, RoutedEventArgs e)
        {
            //populates the datagrid with the counselor1 view information
            var Counselor = dbContext.Counselor1.SqlQuery("select * from Counselor1").ToList();
            CounselorDataGrid.ItemsSource = Counselor;
            CounselorDataGrid.SelectedIndex = 0;
        }

        private void CounselorFirstBtn_Click(object sender, RoutedEventArgs e)
        {
            //sets the datagrid index to zero to select the first record
            CounselorDataGrid.SelectedIndex = 0;
        }

        private void CounselorPreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            //selects the previous record by checking if the current index is greater than zero. if it is the selected record is selected by 
            //subtracting one from the current selected index
            if (CounselorDataGrid.SelectedIndex > 0)
                CounselorDataGrid.SelectedIndex = CounselorDataGrid.SelectedIndex - 1;
        }

        private void CounselorNextBtn_Click(object sender, RoutedEventArgs e)
        {
            //Selects the next record in the list by verifying if the selected index is less than the total number of records. if it is
            // one is added to the current selected index
            if (CounselorDataGrid.SelectedIndex < CounselorDataGrid.Items.Count)
                CounselorDataGrid.SelectedIndex = CounselorDataGrid.SelectedIndex + 1;
        }

        private void CounselorLastBtn_Click(object sender, RoutedEventArgs e)
        {
            //Selects the last record in the datagrid by counting all of the records and subtracting one
            CounselorDataGrid.SelectedIndex = CounselorDataGrid.Items.Count - 1;
        }

        private void CounselorAddBtn_Click(object sender, RoutedEventArgs e)
        {
           //checks to see if the fields are empty. if any are empty it prompts the user to fill in all the fields
            if (CounselorFNameTxt.Text == null || DegreeCB.SelectedIndex == -1 || cbState.SelectedIndex == -1 || CounselorLNameTxt.Text == null || 
                CounselorAddressTxt.Text == null | CounselorCityTxt.Text == null || 
                CounselorEmailTxt.Text == null || CounselorZipTxt.Text == null ||
                CounselorPhoneTxt.Text == null )
            {
                MessageBox.Show("Please Enter Counselor Information into ALL fields");
                
            }
                
           else
            {
                //iMatch holds the value to know if the room name matches any name in current room
                int iMatch = 0;
                //the for loop goes through all the records to check if there are any matches
                for (int iCount = 0; iCount < CounselorDataGrid.Items.Count - 1; iCount++)
                {

                    Counselor1 CurrentCounselor = new Counselor1();
                    CounselorDataGrid.SelectedIndex = iCount;// set the index to that of the iCount 
                    CurrentCounselor = (Counselor1)CounselorDataGrid.SelectedItem;

                    //if the new name matches a record, one is added to iMatch
                    if (CounselorFNameTxt.Text.ToLowerInvariant() == CurrentCounselor.FirstName.ToLowerInvariant() && 
                        CounselorLNameTxt.Text.ToLowerInvariant() == CurrentCounselor.LastName.ToLowerInvariant() &&
                        CounselorAddressTxt.Text.ToLowerInvariant() ==CurrentCounselor.Address.ToLowerInvariant())
                    {
                        iMatch++;
                    }
                }
                if (iMatch > 0)
                {
                    //asks the user if they want to add the name even tho it already exists
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Counselor already exists with this name, add anyway?", "Add Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        //Execute the following query to add new record to the person table.
                        //make variables that capture the values
                        dbContext.Database.ExecuteSqlCommand("Insert into Person(FirstName, LastName, Address, City, State, Zip, Phone, Email) Values (@FirstName, @LastName, @Address, @City, @State, @Zip, @Phone, @Email)",
                            //assign the parameters values
                            new SqlParameter("FirstName", CounselorFNameTxt.Text),
                            new SqlParameter("LastName", CounselorLNameTxt.Text),
                            new SqlParameter("Address", CounselorAddressTxt.Text),
                            new SqlParameter("City", CounselorCityTxt.Text),
                            new SqlParameter("State", cbState.SelectionBoxItem),
                            new SqlParameter("Zip", CounselorZipTxt.Text),
                            new SqlParameter("Phone", CounselorPhoneTxt.Text),
                            new SqlParameter("Email", CounselorEmailTxt.Text));

                        //Execute the follwoing query to add new record to the counselor table
                        dbContext.Database.ExecuteSqlCommand("insert into counselor values((select max(personid) from person),@Degree)",
                           new SqlParameter("Degree", DegreeCB.SelectionBoxItem));

                        //Populate the datagrid with the new records
                        var Counselor = dbContext.Counselor1.SqlQuery("select * from Counselor1").ToList();
                        CounselorDataGrid.ItemsSource = Counselor;
                        //select the first record in the datagrid
                        CounselorDataGrid.SelectedIndex = 0;

                        String AddName = CounselorFNameTxt.Text + " " + CounselorLNameTxt.Text;
                        MessageBox.Show(AddName + " added to Current Counselor");

                        //Reset the values in the fields
                        CounselorFNameTxt.Text = null;
                        CounselorLNameTxt.Text = null;
                        DegreeCB.SelectedIndex = -1;
                        CounselorAddressTxt.Text = null;
                        CounselorCityTxt.Text = null;
                        cbState.SelectedIndex = -1;
                        CounselorZipTxt.Text = null;
                        CounselorPhoneTxt.Text = null;
                        CounselorEmailTxt.Text = null;
                        CounselorID = null;

                    }
                    else
                    {
                        CounselorFNameTxt.Text = null;
                        CounselorLNameTxt.Text = null;
                        DegreeCB.SelectedIndex = -1;
                        CounselorAddressTxt.Text = null;
                        CounselorCityTxt.Text = null;
                        cbState.SelectedIndex = -1;
                        CounselorZipTxt.Text = null;
                        CounselorPhoneTxt.Text = null;
                        CounselorEmailTxt.Text = null;
                        CounselorID = null;
                    }
                }
                else
                {
                    //Execute the following query to add new record to the person table.
                    //make variables that capture the values
                    dbContext.Database.ExecuteSqlCommand("Insert into Person(FirstName, LastName, Address, City, State, Zip, Phone, Email) Values (@FirstName, @LastName, @Address, @City, @State, @Zip, @Phone, @Email)",
                        //assign the parameters values
                        new SqlParameter("FirstName", CounselorFNameTxt.Text),
                        new SqlParameter("LastName", CounselorLNameTxt.Text),
                        new SqlParameter("Address", CounselorAddressTxt.Text),
                        new SqlParameter("City", CounselorCityTxt.Text),
                        new SqlParameter("State", cbState.SelectionBoxItem),
                        new SqlParameter("Zip", CounselorZipTxt.Text),
                        new SqlParameter("Phone", CounselorPhoneTxt.Text),
                        new SqlParameter("Email", CounselorEmailTxt.Text));

                    //Execute the follwoing query to add new record to the counselor table
                    dbContext.Database.ExecuteSqlCommand("insert into counselor values((select max(personid) from person),@Degree)",
                       new SqlParameter("Degree", DegreeCB.SelectionBoxItem));

                    //Populate the datagrid with the new records
                    var Counselor = dbContext.Counselor1.SqlQuery("select * from Counselor1").ToList();
                    CounselorDataGrid.ItemsSource = Counselor;
                    //select the first record in the datagrid
                    CounselorDataGrid.SelectedIndex = 0;

                    String AddName = CounselorFNameTxt.Text + " " + CounselorLNameTxt.Text;
                    MessageBox.Show(AddName + " added to Current Counselor");

                    //Reset the values in the fields
                    CounselorFNameTxt.Text = null;
                    CounselorLNameTxt.Text = null;
                    DegreeCB.SelectedIndex = -1;
                    CounselorAddressTxt.Text = null;
                    CounselorCityTxt.Text = null;
                    cbState.SelectedIndex = -1;
                    CounselorZipTxt.Text = null;
                    CounselorPhoneTxt.Text = null;
                    CounselorEmailTxt.Text = null;
                    CounselorID = null;
                }

            }
        }

        private void CounselorDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            //Create an object called CurrentCounselor of type Counselor1
            Counselor1 CurrentCounselor = new Counselor1();
            //CurrentCounselor holds the values of the selectedItem by casting the CounselorDataGrid as type Counselor1. This makes it possible to grab these values
            CurrentCounselor = (Counselor1)CounselorDataGrid.SelectedItem;
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Delete " + CurrentCounselor.FirstName + " " + CurrentCounselor.LastName + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
               
                //Execute the following query to delete the record from the counselor table using the CounselorID
                this.CounselorID = CurrentCounselor.CounselorID.ToString();
                dbContext.Database.ExecuteSqlCommand("Delete from Counselor where CounselorID = @CounselorID",
                    //assign parameters values
                    new SqlParameter("CounselorID", this.CounselorID));

                //Execute the following query to delete the record from the person table using the person ID
                dbContext.Database.ExecuteSqlCommand("Delete from Person where PersonID = @PersonID",
                     new SqlParameter("PersonID", this.CounselorID));

                String DeleteName = CurrentCounselor.FirstName + " " + CurrentCounselor.LastName;
                MessageBox.Show(DeleteName + " deleted from Current Counselors");

                //Populated the datagrid so that it no longer contains the deleted record
                var Counselor = dbContext.Counselor1.SqlQuery("select * from Counselor1").ToList();
                CounselorDataGrid.ItemsSource = Counselor;
                CounselorDataGrid.SelectedIndex = 0;

                //Reset the values in the fields
                CounselorFNameTxt.Text = null;
                CounselorLNameTxt.Text = null;
                DegreeCB.SelectedIndex = -1;
                CounselorAddressTxt.Text = null;
                CounselorCityTxt.Text = null;
                cbState.SelectedIndex = -1;
                CounselorZipTxt.Text = null;
                CounselorPhoneTxt.Text = null;
                CounselorEmailTxt.Text = null;
                CounselorID = null;

                CounselorEditBtn.IsEnabled = true;
                CounselorAddBtn.IsEnabled = true;
            }
            
        }

        //The CounselorID variable holds the value of the counselor selected in the datagrid so that when the record is updated it, The corrected record is updated
        String CounselorID;
        int Index;

        private void CounselorEditBtn_Click(object sender, RoutedEventArgs e)
        {
            //Create an object called CurrentCounselor of type Counselor1
            Counselor1 CurrentCounselor = new Counselor1();
            //CurrentCounselor holds the values of the selectedItem by casting the CounselorDataGrid as type Counselor1. This makes it possible to grab these values
            CurrentCounselor = (Counselor1)CounselorDataGrid.SelectedItem;
            Index = CounselorDataGrid.SelectedIndex;

            //Populate the text fields with their respective data from the datagrid
            CounselorFNameTxt.Text = CurrentCounselor.FirstName;
            CounselorLNameTxt.Text = CurrentCounselor.LastName;
            DegreeCB.Text = CurrentCounselor.DegreeSuffix;
            CounselorAddressTxt.Text = CurrentCounselor.Address;
            CounselorCityTxt.Text = CurrentCounselor.City;
            cbState.Text = CurrentCounselor.State;
            CounselorZipTxt.Text = CurrentCounselor.ZIP;
            CounselorPhoneTxt.Text = CurrentCounselor.Phone;
            CounselorEmailTxt.Text = CurrentCounselor.Email;

            //CounselorID holds the counselorID of the record that is being updated
            this.CounselorID = CurrentCounselor.CounselorID.ToString();

            CounselorAddBtn.IsEnabled = false;
            CounselorEditBtn.IsEnabled = false;
        }

        private void CounselorUpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            jeffersonEntities dbContext = new jeffersonEntities();
            //Create an object called CurrentCounselor of type Counselor1

            try
            {
                //The update statement updates all fields. This enables the user to update any and all the information
                dbContext.Database.ExecuteSqlCommand("Update Person set FirstName = @FirstName, LastName = @LastName, Address = @Address, City = @City, State = @State, ZIP = @ZIP, Phone = @Phone, Email = @Email where PersonID = @CounselorID",
                    //Assigns parameters values found in the textfields
                    new SqlParameter("FirstName", CounselorFNameTxt.Text),
                    new SqlParameter("LastName", CounselorLNameTxt.Text),
                    new SqlParameter("Address", CounselorAddressTxt.Text),
                    new SqlParameter("City", CounselorCityTxt.Text),
                    new SqlParameter("State", cbState.SelectionBoxItem),
                    new SqlParameter("Zip", CounselorZipTxt.Text),
                    new SqlParameter("Phone", CounselorPhoneTxt.Text),
                    new SqlParameter("Email", CounselorEmailTxt.Text),
                    new SqlParameter("CounselorID", this.CounselorID));
                //updates the Degree by using the counselorID so that we know we are updating the correct record
                dbContext.Database.ExecuteSqlCommand("Update counselor set DegreeSuffix = @Degree where CounselorID = @CounselorID",
                  new SqlParameter("Degree", DegreeCB.SelectionBoxItem),
                  new SqlParameter("CounselorID", this.CounselorID));

                //Populates the datagrid with the updated information
                var Counselor = dbContext.Counselor1.SqlQuery("select * from Counselor1").ToList();
                CounselorDataGrid.ItemsSource = Counselor;
                CounselorDataGrid.SelectedIndex = Index;

                String AddName = CounselorFNameTxt.Text + " " + CounselorLNameTxt.Text;
                MessageBox.Show(AddName + " Updated");

                //Resets the text fields
                CounselorFNameTxt.Text = null;
                CounselorLNameTxt.Text = null;
                DegreeCB.SelectedIndex = -1;
                CounselorAddressTxt.Text = null;
                CounselorCityTxt.Text = null;
                cbState.SelectedIndex = -1;
                CounselorZipTxt.Text = null;
                CounselorPhoneTxt.Text = null;
                CounselorEmailTxt.Text = null;
                this.CounselorID = null;
            }
            catch
            {
                MessageBox.Show("Please Select a Counselor from 'Current Counselor', and click the 'Edit Counselor' button to populate Counselor's information in the fields");
            }

        }
    }
    
}
