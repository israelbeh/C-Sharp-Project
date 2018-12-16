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
    /*Description: Allows user to add, delete, read and update clients
     * */
    /// <summary>
    /// Interaction logic for Client.xaml
    /// </summary>
    public partial class Clients : Window
    {
        jeffersonEntities dbContext = new jeffersonEntities();

        private void Clients_Loaded(object sender, RoutedEventArgs e)
        {
            //populates the datagrid with the Employee1 view information
            var Client = dbContext.Clients1.SqlQuery("Select * from Clients").ToList();
            ClientDataGrid.ItemsSource = Client;
            ClientDataGrid.SelectedIndex = 0;

            String connectionString = "data source=cactus.arvixe.com;initial catalog=jefferson;persist security info=True;user id=ijbeh;password=cordoba7";
            /*Create a connection object. The method Get Connection String is a method that returns my connection string to the cactus.arvixe server. The String looks like this:"Data Source=cactus.arvixe.com;Initial Catalog=jefferson;Persist Security Info=True;User ID=YOURUSERNAMEHERE;Password=YOURPASSWORDHERE"*/
            /*You can store this as a String and pass it with a simple method.*/
            SqlConnection sqlConn = new SqlConnection(connectionString);

            /*This is the create select statement line */
            SqlDataAdapter sqlAdapt = new SqlDataAdapter(@"SELECT (FirstName+ ' ' +LastName) ReferredBy FROM Clients", sqlConn);

            /*Just do this part. You may not understand it. */
            SqlCommandBuilder sqlCmdBuilder = new SqlCommandBuilder(sqlAdapt);

            /*Create a dataset object to hold the data pulled*/
            DataSet sqlSetEmployee = new DataSet();
            sqlAdapt.Fill(sqlSetEmployee, "ReferredBy" );

            /*Close the connection.*/
            sqlConn.Close();

            /*Create a datatable object that will be the location of the usable data pulled from the dataset */
            DataTable dtEmployee = new DataTable();

            /*Add the data from dataset to the datatable */
            dtEmployee = sqlSetEmployee.Tables["ReferredBy" ];

            /*For ever piece of row data in the datatable.*/
            foreach (DataRow dr in dtEmployee.Rows)
            {
                /*Checks for duplicate entries and then add if not duplicate. The combobox is called cbCounselor by the way.*/
                if (!ReferredByCB.Items.Contains(dr["ReferredBy" ]))
                {
                    ReferredByCB.Items.Add(dr["ReferredBy"]);
                }
            }
            
        }
        public Clients()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClientAddBtn_Click(object sender, RoutedEventArgs e)
        {
            jeffersonEntities dbContext = new jeffersonEntities();

            //makes so the user has to enter all critical information
            if (ClientFNameTxt.Text == null || ClientLNameTxt.Text == null ||
                ClientAddressTxt.Text == null ||
                ClientCityTxt.Text == null ||
                ClientZipTxt.Text == null ||
                ClientPhoneTxt.Text == null ||
                ClientEmailTxt.Text == null ||
                cbState.SelectedIndex == -1)
            {
                MessageBox.Show("Please Enter All information");
            }
            else
            {
                int iMatch = 0;
                //the for loop goes through all the records to check if there are any matches
                for (int iCount = 0; iCount < ClientDataGrid.Items.Count - 1; iCount++)
                {

                    Client1 CurrentClient = new Client1();
                    ClientDataGrid.SelectedIndex = iCount;// set the index to that of the iCount 
                    CurrentClient = (Client1)ClientDataGrid.SelectedItem;

                    //if the new name matches a record, one is added to iMatch
                    if (ClientFNameTxt.Text.ToLowerInvariant() == CurrentClient.FirstName.ToLowerInvariant() &&
                        ClientLNameTxt.Text.ToLowerInvariant() == CurrentClient.LastName.ToLowerInvariant() &&
                        ClientAddressTxt.Text.ToLowerInvariant() == CurrentClient.Address.ToLowerInvariant())
                    {
                        iMatch++;
                    }
                }
                if (iMatch > 0)
                {
                    //asks the user if they want to add the name even tho it already exists
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Client already exists with this name, add anyway?", "Add Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        dbContext.Database.ExecuteSqlCommand("Insert into Person(FirstName, LastName, Address, City, State, Zip, Phone, Email) Values (@FirstName, @LastName, @Address, @City, @State, @Zip, @Phone, @Email)",
                            //assign the parameters values
                            new SqlParameter("FirstName", ClientFNameTxt.Text),
                            new SqlParameter("LastName", ClientLNameTxt.Text),
                            new SqlParameter("Address", ClientAddressTxt.Text),
                            new SqlParameter("City", ClientCityTxt.Text),
                            new SqlParameter("State", cbState.SelectionBoxItem),
                            new SqlParameter("Zip", ClientZipTxt.Text),
                            new SqlParameter("Phone", ClientPhoneTxt.Text),
                            new SqlParameter("Email", ClientEmailTxt.Text));
                        //the if statement verifies if we need to grab a clientID by spliting the first name and last name
                        if (ReferredByCB.SelectedIndex != -1)
                        {
                            //Execute the following query to add new record to the person table.
                            //make variables that capture the values
                            var refName = ReferredByCB.SelectionBoxItem.ToString();

                            string Ref = refName;
                            //Ex: ger = Holly White

                            var reName = Ref.Split(' ');
                            //Ex: [1] = White   [0] = Holly
                            string refLastName = reName[1].ToString();
                            string refFirstName = reName[0].ToString();

                            Console.WriteLine(refLastName);
                            //gets ClientID for referred by
                            int query_super_ID = (from Person in dbContext.People
                                                  where Person.FirstName == refFirstName &&
                                                     Person.LastName == refLastName
                                                  select Person).First<Person>().personID;
                            int RefID = Convert.ToInt32(query_super_ID);
                            //runds query to run query
                            dbContext.Database.ExecuteSqlCommand("insert into Client values((select max(personid) from person), @FoundOut, @ReminderMethod, @ReferredBy)",
                               new SqlParameter("FoundOut", FoundOutCB.SelectionBoxItem),
                               new SqlParameter("ReminderMethod", ClientReminderCB.SelectionBoxItem),
                               new SqlParameter("ReferredBy", RefID));

                        }
                        else
                        {
                            //Execute the follwoing query to add new record to the Client table
                            dbContext.Database.ExecuteSqlCommand("insert into Client(ClientID, FoundOut, ReminderMethod) values((select max(personid) from person), @FoundOut, @ReminderMethod)",
                               new SqlParameter("FoundOut", FoundOutCB.SelectionBoxItem),
                               new SqlParameter("ReminderMethod", ClientReminderCB.SelectionBoxItem));
                        }

                        //Populate the datagrid with the new records
                        var Client = dbContext.Clients1.SqlQuery("select * from Clients").ToList();
                        ClientDataGrid.ItemsSource = Client;
                        //select the first record in the datagrid
                        ClientDataGrid.SelectedIndex = 0;

                        //Display that the new client has been added
                        String UpdateName = ClientFNameTxt.Text + " " + ClientLNameTxt.Text;
                        MessageBox.Show(UpdateName + " Added to Current Clients");


                        //Reset the values in the fields
                        ClientFNameTxt.Text = null;
                        ClientLNameTxt.Text = null;
                        FoundOutCB.SelectedIndex = -1;
                        ClientReminderCB.SelectedIndex = -1;
                        ReferredByCB.SelectedIndex = -1;
                        ClientAddressTxt.Text = null;
                        ClientCityTxt.Text = null;
                        cbState.SelectedIndex = -1;
                        ClientZipTxt.Text = null;
                        ClientPhoneTxt.Text = null;
                        ClientEmailTxt.Text = null;
                        ReferredByCB.SelectedIndex = -1;
                        ClientID = null;
                    }
                    else
                    {
                        //Reset the values in the fields
                        ClientFNameTxt.Text = null;
                        ClientLNameTxt.Text = null;
                        FoundOutCB.SelectedIndex = -1;
                        ClientReminderCB.SelectedIndex = -1;
                        ReferredByCB.SelectedIndex = -1;
                        ClientAddressTxt.Text = null;
                        ClientCityTxt.Text = null;
                        cbState.SelectedIndex = -1;
                        ClientZipTxt.Text = null;
                        ClientPhoneTxt.Text = null;
                        ClientEmailTxt.Text = null;
                        ReferredByCB.SelectedIndex = -1;
                        ClientID = null;
                    }

                }
                else
                {
                    //runs query to add records
                    dbContext.Database.ExecuteSqlCommand("Insert into Person(FirstName, LastName, Address, City, State, Zip, Phone, Email) Values (@FirstName, @LastName, @Address, @City, @State, @Zip, @Phone, @Email)",
                        //assign the parameters values
                            new SqlParameter("FirstName", ClientFNameTxt.Text),
                            new SqlParameter("LastName", ClientLNameTxt.Text),
                            new SqlParameter("Address", ClientAddressTxt.Text),
                            new SqlParameter("City", ClientCityTxt.Text),
                            new SqlParameter("State", cbState.SelectionBoxItem),
                            new SqlParameter("Zip", ClientZipTxt.Text),
                            new SqlParameter("Phone", ClientPhoneTxt.Text),
                            new SqlParameter("Email", ClientEmailTxt.Text));
                    //the if statement verifies if we need to grab a clientID by spliting the first name and last name
                    if (ReferredByCB.SelectedIndex != -1)
                        {
                            //Execute the following query to add new record to the person table.
                            //make variables that capture the values
                            var refName = ReferredByCB.SelectionBoxItem.ToString();

                            string Ref = refName;
                            //Ex: ger = Holly White

                            var reName = Ref.Split(' ');
                            //Ex: [1] = White   [0] = Holly
                            string refLastName = reName[1].ToString();
                            string refFirstName = reName[0].ToString();

                            Console.WriteLine(refLastName);

                            int query_super_ID = (from Person in dbContext.People
                                                  where Person.FirstName == refFirstName &&
                                                     Person.LastName == refLastName
                                                  select Person).First<Person>().personID;
                            int RefID = Convert.ToInt32(query_super_ID);

                            dbContext.Database.ExecuteSqlCommand("insert into Client values((select max(personid) from person), @FoundOut, @ReminderMethod, @ReferredBy)",
                               new SqlParameter("FoundOut", FoundOutCB.SelectionBoxItem),
                               new SqlParameter("ReminderMethod", ClientReminderCB.SelectionBoxItem),
                               new SqlParameter("ReferredBy", RefID));

                        }
                        else
                        {
                            //Execute the follwoing query to add new record to the Client table
                            dbContext.Database.ExecuteSqlCommand("insert into Client(ClientID, FoundOut, ReminderMethod) values((select max(personid) from person), @FoundOut, @ReminderMethod)",
                               new SqlParameter("FoundOut", FoundOutCB.SelectionBoxItem),
                               new SqlParameter("ReminderMethod", ClientReminderCB.SelectionBoxItem));
                        }

                        //Populate the datagrid with the new records
                        var Client = dbContext.Clients1.SqlQuery("select * from Clients").ToList();
                        ClientDataGrid.ItemsSource = Client;
                        //select the first record in the datagrid
                        ClientDataGrid.SelectedIndex = 0;

                        //Display that the new client has been added
                        String UpdateName = ClientFNameTxt.Text + " " + ClientLNameTxt.Text;
                        MessageBox.Show(UpdateName + " Added to Current Clients");


                        //Reset the values in the fields
                        ClientFNameTxt.Text = null;
                        ClientLNameTxt.Text = null;
                        FoundOutCB.SelectedIndex = -1;
                        ClientReminderCB.SelectedIndex = -1;
                        ReferredByCB.SelectedIndex = -1;
                        ClientAddressTxt.Text = null;
                        ClientCityTxt.Text = null;
                        cbState.SelectedIndex = -1;
                        ClientZipTxt.Text = null;
                        ClientPhoneTxt.Text = null;
                        ClientEmailTxt.Text = null;
                        ReferredByCB.SelectedIndex = -1;
                        ClientID = null;
                }
            }
                  
           

        }
        //The ClientID variable holds the value of the Client selected in the datagrid so that when the record is updated it, The corrected record is updated
        String ClientID;
       
        private void ClientUpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            jeffersonEntities dbContext = new jeffersonEntities();
            //Execute the following query to add new record to the person table.
            //make variables that capture the values
           try
           {
               int RefID;
               //The update statement updates all fields. This enables the user to update any and all the information
               dbContext.Database.ExecuteSqlCommand("Update Person set FirstName = @FirstName, LastName = @LastName, Address = @Address, City = @City, State = @State, ZIP = @ZIP, Phone = @Phone, Email = @Email where PersonID = @ClientID",
                   //Assigns parameters values found in the textfields
                   new SqlParameter("FirstName", ClientFNameTxt.Text),
                   new SqlParameter("LastName", ClientLNameTxt.Text),
                   new SqlParameter("Address", ClientAddressTxt.Text),
                   new SqlParameter("City", ClientCityTxt.Text),
                   new SqlParameter("State", cbState.SelectionBoxItem),
                   new SqlParameter("Zip", ClientZipTxt.Text),
                   new SqlParameter("Phone", ClientPhoneTxt.Text),
                   new SqlParameter("Email", ClientEmailTxt.Text),
                   new SqlParameter("ClientID", this.ClientID));
               
               if (ReferredByCB.SelectedIndex != -1)
               {
                   var refName = ReferredByCB.SelectionBoxItem.ToString();

                   string Ref = refName;
                   //Ex: ger = Holly White

                   var reName = Ref.Split(' ');
                   //Ex: [1] = White   [0] = Holly
                   string refLastName = reName[1].ToString();
                   string refFirstName = reName[0].ToString();


                   int query_Referred_ID = (from Person in dbContext.People
                                            where Person.FirstName == refFirstName &&
                                               Person.LastName == refLastName
                                            select Person).First<Person>().personID;
                   RefID = Convert.ToInt32(query_Referred_ID);

                   //updates the Degree by using the ClientID so that we know we are updating the correct record
                   dbContext.Database.ExecuteSqlCommand("Update Client set FoundOut = @FoundOut, ReminderMethod = @ReminderMethod, ReferredByClientID = @ReferredBy where ClientID = @ClientID",
                     new SqlParameter("FoundOut", FoundOutCB.SelectionBoxItem),
                     new SqlParameter("ReminderMethod", ClientReminderCB.SelectionBoxItem),
                     new SqlParameter("ReferredBy", RefID),
                     new SqlParameter("ClientID", this.ClientID));
               }
               else
               {
                   //updates the Degree by using the ClientID so that we know we are updating the correct record
                   dbContext.Database.ExecuteSqlCommand("Update Client set FoundOut = @FoundOut, ReminderMethod = @ReminderMethod where ClientID = @ClientID",
                     new SqlParameter("FoundOut", FoundOutCB.SelectionBoxItem),
                     new SqlParameter("ReminderMethod", ClientReminderCB.SelectionBoxItem),
                     new SqlParameter("ClientID", this.ClientID));
               }

               String UpdateName = ClientFNameTxt.Text + " " + ClientLNameTxt.Text;
               //Resets the text fields
               ClientFNameTxt.Text = null;
               ClientLNameTxt.Text = null;
               FoundOutCB.SelectedIndex = -1;
               ClientReminderCB.SelectedIndex = -1;
               ReferredByCB.SelectedIndex = -1;
               ClientAddressTxt.Text = null;
               ClientCityTxt.Text = null;
               cbState.SelectedIndex = -1;
               ClientZipTxt.Text = null;
               ClientPhoneTxt.Text = null;
               ClientEmailTxt.Text = null;
               ReferredByCB.SelectedIndex = -1;
               this.ClientID = null;

               ClientAddBtn.IsEnabled = true;
               ClientDeleteBtn.IsEnabled = true;

               //Populates the datagrid with the updated information
               var Client = dbContext.Clients1.SqlQuery("select * from Clients").ToList();
               ClientDataGrid.ItemsSource = Client;
               ClientDataGrid.SelectedIndex = Index;

               
               MessageBox.Show(UpdateName + " Updated");

               ClientAddBtn.IsEnabled = true;
               ClientEditBtn.IsEnabled = true;

               
   
           }
           catch
           {
               MessageBox.Show("Please select a Client from 'Current Clients' and Click the 'Edit Client' Button to populate fields with the Client's information");
           }
        }

        private void ClientFirstBtn_Click(object sender, RoutedEventArgs e)
        {
            //sets the datagrid index to zero to select the first record
            ClientDataGrid.SelectedIndex = 0;
        }

        private void ClientPreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            //selects the previous record by checking if the current index is greater than zero. if it is the selected record is selected by 
            //subtracting one from the current selected index
            if (ClientDataGrid.SelectedIndex > 0)
                ClientDataGrid.SelectedIndex = ClientDataGrid.SelectedIndex - 1;
        }

        private void ClientNextBtn_Click(object sender, RoutedEventArgs e)
        {
            //Selects the next record in the list by verifying if the selected index is less than the total number of records. if it is
            // one is added to the current selected index
            if (ClientDataGrid.SelectedIndex < ClientDataGrid.Items.Count)
                ClientDataGrid.SelectedIndex = ClientDataGrid.SelectedIndex + 1;
        }

        private void ClientLastBtn_Click(object sender, RoutedEventArgs e)
        {
            //Selects the last record in the datagrid by counting all of the records and subtracting one
            ClientDataGrid.SelectedIndex = ClientDataGrid.Items.Count - 1;
        }
        int Index;
        private void ClientEditBtn_Click(object sender, RoutedEventArgs e)
        {
            //Create an object called CurrentClient of type Client1
            Client1 CurrentClient = new Client1();
            //CurrentClient holds the values of the selectedItem by casting the ClientDataGrid as type Client1. This makes it possible to grab these values
            CurrentClient = (Client1)ClientDataGrid.SelectedItem;
            Index = ClientDataGrid.SelectedIndex;
            if (CurrentClient.ReferredBy != null)
            {
               
                ReferredByCB.SelectedValue = CurrentClient.ReferredBy;

            }

            //Populate the text fields with their respective data from the datagrid
            ClientFNameTxt.Text = CurrentClient.FirstName;
            ClientLNameTxt.Text = CurrentClient.LastName;
            ClientReminderCB.Text = CurrentClient.ReminderMethod;           
            FoundOutCB.Text = CurrentClient.FoundOut;
            ClientAddressTxt.Text = CurrentClient.Address;
            ClientCityTxt.Text = CurrentClient.City;
            cbState.Text = CurrentClient.State;
            ClientZipTxt.Text = CurrentClient.ZIP;
            ClientPhoneTxt.Text = CurrentClient.Phone;
            ClientEmailTxt.Text = CurrentClient.Email;

            //ClientID holds the ClientID of the record that is being updated
            this.ClientID = CurrentClient.ClientID.ToString();
            ClientAddBtn.IsEnabled = false;
            ClientEditBtn.IsEnabled = false;
            
        }

        private void ClientDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            //Create an object called CurrentClient of type Client1
            Client1 CurrentClient = new Client1();
            //CurrentClient holds the values of the selectedItem by casting the ClientDataGrid as type Client1. This makes it possible to grab these values
            CurrentClient = (Client1)ClientDataGrid.SelectedItem;
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Delete " + CurrentClient.FirstName + " " + CurrentClient.LastName + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                
                
                ClientID = CurrentClient.ClientID.ToString();
                //Execute the following query to delete the record from the Client table using the ClientID

                int iNum;
                for (iNum = 0; iNum < ClientDataGrid.Items.Count - 1; iNum++)
                {
                    ClientDataGrid.SelectedIndex = iNum;

                   Client1 ReferClient = new Client1();
                    ReferClient = (Client1)ClientDataGrid.SelectedItem;

                    string Ref = ReferClient.ReferredBy;
                    //Ex: ger = Holly White
                    if (ReferClient.ReferredBy != null)
                    {
                        var reName = Ref.Split(' ');
                        //Ex: [1] = White   [0] = Holly
                        string refLastName = reName[1].ToString();
                        string refFirstName = reName[0].ToString();

                        Console.WriteLine(refLastName);

                        //gets the ClientID by running a query
                        int query_super_ID = (from Person in dbContext.People
                                              where Person.FirstName == refFirstName &&
                                                 Person.LastName == refLastName
                                              select Person).First<Person>().personID;
                        //Assign value to RefID
                        int RefID = Convert.ToInt32(query_super_ID);
                        //Gets the referredbyCLientID 
                        String CLREF = ReferClient.ClientID.ToString();
                        //converts the referredByClientID to an Integer
                        int CLR = Convert.ToInt32(CLREF);
                        
                        //Compares the ReferredByClientID with Client of Whomever you want to delete
                        if (RefID == Convert.ToInt32(ClientID))
                        {
                            //Sets the ReferredByClientID to null so that you can delete the Client who referred
                            dbContext.Database.ExecuteSqlCommand("Update Client set ReferredByCLientID = null where ClientID = @RefID",
                                new SqlParameter("RefID", CLR));
                        }
                    }
                }
                dbContext.Database.ExecuteSqlCommand("Delete from Client where ClientID = @ClientID",
                    //assign parameters values
                     new SqlParameter("ClientID", ClientID));
                //Execute the following query to delete the record from the person table using the person ID
                dbContext.Database.ExecuteSqlCommand("Delete from Person where PersonID = @PersonID",
                     new SqlParameter("PersonID", ClientID));

                ClientFNameTxt.Text = null;
                ClientLNameTxt.Text = null;
                FoundOutCB.SelectedIndex = -1;
                ClientReminderCB.SelectedIndex = -1;
                ReferredByCB.SelectedIndex = -1;
                ClientAddressTxt.Text = null;
                ClientCityTxt.Text = null;
                cbState.SelectedIndex = -1;
                ClientZipTxt.Text = null;
                ClientPhoneTxt.Text = null;
                ClientEmailTxt.Text = null;
                ReferredByCB.SelectedIndex = -1;
                this.ClientID = null;

                String DeleteName = CurrentClient.FirstName + " " + CurrentClient.LastName;
                MessageBox.Show(DeleteName + " Deleted From Current Clients");

                //Populated the datagrid so that it no longer contains the deleted record
                var Client = dbContext.Clients1.SqlQuery("select * from Clients").ToList();
                ClientDataGrid.ItemsSource = Client;
                ClientDataGrid.SelectedIndex = 0;

                ClientAddBtn.IsEnabled = true;
                ClientEditBtn.IsEnabled = true;

                
            }
        }

    }
}
