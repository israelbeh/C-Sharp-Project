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
    //allows user to create, read, update and delete employees
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employees : Window
    {
        jeffersonEntities dbContext = new jeffersonEntities();

        public Employees()
        {
            InitializeComponent();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //loads the window with the necessary information
        private void Employees_Loaded(object sender, RoutedEventArgs e)
        {
            //populates the datagrid with the Employee1 view information
            var Employee = dbContext.Employees1.SqlQuery("select * from Employees").ToList();
            EmployeeDataGrid.ItemsSource = Employee;
            EmployeeDataGrid.SelectedIndex = 0;

            String connectionString = "data source=cactus.arvixe.com;initial catalog=jefferson;persist security info=True;user id=ijbeh;password=cordoba7"; 
            /*Create a connection object. The method Get Connection String is a method that returns my connection string to the cactus.arvixe server. The String looks like this:"Data Source=cactus.arvixe.com;Initial Catalog=jefferson;Persist Security Info=True;User ID=YOURUSERNAMEHERE;Password=YOURPASSWORDHERE"*/
            /*You can store this as a String and pass it with a simple method.*/
            SqlConnection sqlConn = new SqlConnection(connectionString);

            /*This is the create select statement line */
            SqlDataAdapter sqlAdapt = new SqlDataAdapter(@"SELECT (FirstName+ ' ' +LastName) Supervisor FROM Employees", sqlConn);

            /*Just do this part. You may not understand it. */
            SqlCommandBuilder sqlCmdBuilder = new SqlCommandBuilder(sqlAdapt);

            /*Create a dataset object to hold the data pulled*/
            DataSet sqlSetEmployee = new DataSet();
            sqlAdapt.Fill(sqlSetEmployee, "Supervisor");

            /*Close the connection.*/ 
            sqlConn.Close();

            /*Create a datatable object that will be the location of the usable data pulled from the dataset */
            DataTable dtEmployee = new DataTable();

            /*Add the data from dataset to the datatable */
            dtEmployee = sqlSetEmployee.Tables["Supervisor"];

            /*For ever piece of row data in the datatable.*/
            foreach (DataRow dr in dtEmployee.Rows)
            {
                /*Checks for duplicate entries and then add if not duplicate. The combobox is called cbCounselor by the way.*/
                if (!SupervisorCB.Items.Contains(dr["Supervisor"]))
                {
                    SupervisorCB.Items.Add(dr["Supervisor"]);
                }
            }
            
        }

        //The EmployeeID variable holds the value of the Employee selected in the datagrid so that when the record is updated it, The corrected record is updated
        String EmployeeID;
    

        private void EmployeeAddBtn_Click_1(object sender, RoutedEventArgs e)
        {
            //Checks to see if user has left any fields open
            if (EmployeeFNameTxt.Text == null ||
                EmployeeLNameTxt.Text == null ||
                DateHired.Text == null ||
                EmployeeWageTxt.Text == null ||
                SupervisorCB.SelectedIndex == -1 ||
                EmployeeAddressTxt.Text == null ||
                EmployeeCityTxt.Text == null ||
                cbState.SelectedIndex == -1 ||
                EmployeeZipTxt.Text == null ||
                EmployeePhoneTxt.Text == null ||
                EmployeeEmailTxt.Text == null)
            {
                MessageBox.Show("Please Enter ALL Employee Information");
            }
            else
            {
                 //iMatch holds the value to know if the room name matches any name in current room
                int iMatch = 0;
                //the for loop goes through all the records to check if there are any matches
                for (int iCount = 0; iCount < EmployeeDataGrid.Items.Count - 1; iCount++)
                {

                    Employee1 CurrentEmployee = new Employee1();
                    EmployeeDataGrid.SelectedIndex = iCount;// set the index to that of the iCount 
                    CurrentEmployee = (Employee1)EmployeeDataGrid.SelectedItem;

                    //if the new name matches a record, one is added to iMatch
                    if (EmployeeFNameTxt.Text.ToLowerInvariant() == CurrentEmployee.FirstName.ToLowerInvariant() && 
                        EmployeeLNameTxt.Text.ToLowerInvariant() == CurrentEmployee.LastName.ToLowerInvariant() &&
                        EmployeeAddressTxt.Text.ToLowerInvariant() ==CurrentEmployee.Address.ToLowerInvariant())
                    {
                        iMatch++;
                    }
                }
                if (iMatch > 0)
                {
                    //asks the user if they want to add the name even tho it already exists
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Employee already exists with this name, add anyway?", "Add Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {


                        //Execute the following query to add new record to the person table.
                        //make variables that capture the values
                        var supervisorName = SupervisorCB.SelectionBoxItem.ToString();

                        string Sup = supervisorName;
                        //Ex: ger = Holly White

                        var EmployeeName = Sup.Split(' ');
                        //Ex: [1] = White   [0] = Holly
                        string supervisorLastName = EmployeeName[1].ToString();
                        string supervisorFirstName = EmployeeName[0].ToString();

                        //runs query to get SuperID
                        int query_super_ID = (from Person in dbContext.People
                                              where Person.FirstName == supervisorFirstName &&
                                               Person.LastName == supervisorLastName
                                              select Person).First<Person>().personID;
                        //converts to int
                        int superID = Convert.ToInt32(query_super_ID);


                        dbContext.Database.ExecuteSqlCommand("Insert into Person(FirstName, LastName, Address, City, State, Zip, Phone, Email) Values (@FirstName, @LastName, @Address, @City, @State, @Zip, @Phone, @Email)",
                            //assign the parameters values
                            new SqlParameter("FirstName", EmployeeFNameTxt.Text),
                            new SqlParameter("LastName", EmployeeLNameTxt.Text),
                            new SqlParameter("Address", EmployeeAddressTxt.Text),
                            new SqlParameter("City", EmployeeCityTxt.Text),
                            new SqlParameter("State", cbState.SelectionBoxItem),
                            new SqlParameter("Zip", EmployeeZipTxt.Text),
                            new SqlParameter("Phone", EmployeePhoneTxt.Text),
                            new SqlParameter("Email", EmployeeEmailTxt.Text));
                        try
                        {

                            //Execute the follwoing query to add new record to the Employee table
                            dbContext.Database.ExecuteSqlCommand("insert into Employee values((select max(personid) from person), @DateHired, @Wage,  @Supervisor)",
                               new SqlParameter("DateHired", DateHired.SelectedDate.Value.ToShortDateString()),
                               new SqlParameter("Wage", EmployeeWageTxt.Text),
                               new SqlParameter("Supervisor", superID));

                            //Populate the datagrid with the new records
                            var Employee = dbContext.Employees1.SqlQuery("select * from Employees").ToList();
                            EmployeeDataGrid.ItemsSource = Employee;
                            //select the first record in the datagrid
                            EmployeeDataGrid.SelectedIndex = 0;


                            String AddName = EmployeeFNameTxt.Text + " " + EmployeeLNameTxt.Text;
                            MessageBox.Show(AddName + " Added To Current Employees");

                            //Reset the values in the fields
                            EmployeeFNameTxt.Text = null;
                            EmployeeLNameTxt.Text = null;
                            DateHired.Text = null;
                            EmployeeWageTxt.Text = null;
                            SupervisorCB.SelectedIndex = -1;
                            EmployeeAddressTxt.Text = null;
                            EmployeeCityTxt.Text = null;
                            cbState.SelectedIndex = -1;
                            EmployeeZipTxt.Text = null;
                            EmployeePhoneTxt.Text = null;
                            EmployeeEmailTxt.Text = null;
                            EmployeeID = null;
                        }
                        catch
                        {
                            MessageBox.Show("Please Enter ALL Employee Information");
                        }
                    }
                    else
                    {//resets the fields
                        EmployeeFNameTxt.Text = null;
                        EmployeeLNameTxt.Text = null;
                        DateHired.Text = null;
                        EmployeeWageTxt.Text = null;
                        SupervisorCB.SelectedIndex = -1;
                        EmployeeAddressTxt.Text = null;
                        EmployeeCityTxt.Text = null;
                        cbState.SelectedIndex = -1;
                        EmployeeZipTxt.Text = null;
                        EmployeePhoneTxt.Text = null;
                        EmployeeEmailTxt.Text = null;
                        EmployeeID = null;
                    }

                }
                else
                {
                    //Execute the following query to add new record to the person table.
                    //make variables that capture the values
                    var supervisorName = SupervisorCB.SelectionBoxItem.ToString();

                    string Sup = supervisorName;
                    //Ex: ger = Holly White

                    var EmployeeName = Sup.Split(' ');
                    //Ex: [1] = White   [0] = Holly
                    string supervisorLastName = EmployeeName[1].ToString();
                    string supervisorFirstName = EmployeeName[0].ToString();

                    //runs query
                    int query_super_ID = (from Person in dbContext.People
                                          where Person.FirstName == supervisorFirstName &&
                                             Person.LastName == supervisorLastName
                                          select Person).First<Person>().personID;
                    int superID = Convert.ToInt32(query_super_ID);


                    dbContext.Database.ExecuteSqlCommand("Insert into Person(FirstName, LastName, Address, City, State, Zip, Phone, Email) Values (@FirstName, @LastName, @Address, @City, @State, @Zip, @Phone, @Email)",
                        //assign the parameters values
                        new SqlParameter("FirstName", EmployeeFNameTxt.Text),
                        new SqlParameter("LastName", EmployeeLNameTxt.Text),
                        new SqlParameter("Address", EmployeeAddressTxt.Text),
                        new SqlParameter("City", EmployeeCityTxt.Text),
                        new SqlParameter("State", cbState.SelectionBoxItem),
                        new SqlParameter("Zip", EmployeeZipTxt.Text),
                        new SqlParameter("Phone", EmployeePhoneTxt.Text),
                        new SqlParameter("Email", EmployeeEmailTxt.Text));
                    try // try catches errors relating to supervisor
                    {

                        //Execute the follwoing query to add new record to the Employee table
                        dbContext.Database.ExecuteSqlCommand("insert into Employee values((select max(personid) from person), @DateHired, @Wage,  @Supervisor)",
                           new SqlParameter("DateHired", DateHired.SelectedDate.Value.ToShortDateString()),
                           new SqlParameter("Wage", EmployeeWageTxt.Text),
                           new SqlParameter("Supervisor", superID));

                        //Populate the datagrid with the new records
                        var Employee = dbContext.Employees1.SqlQuery("select * from Employees").ToList();
                        EmployeeDataGrid.ItemsSource = Employee;
                        //select the first record in the datagrid
                        EmployeeDataGrid.SelectedIndex = 0;


                        String AddName = EmployeeFNameTxt.Text + " " + EmployeeLNameTxt.Text;
                        MessageBox.Show(AddName + " Added To Current Employees");

                        //Reset the values in the fields
                        EmployeeFNameTxt.Text = null;
                        EmployeeLNameTxt.Text = null;
                        DateHired.Text = null;
                        EmployeeWageTxt.Text = null;
                        SupervisorCB.SelectedIndex = -1;
                        EmployeeAddressTxt.Text = null;
                        EmployeeCityTxt.Text = null;
                        cbState.SelectedIndex = -1;
                        EmployeeZipTxt.Text = null;
                        EmployeePhoneTxt.Text = null;
                        EmployeeEmailTxt.Text = null;
                        EmployeeID = null;
                    }
                    catch
                    {
                        //messagebox
                        MessageBox.Show("Please Enter ALL Employee Information");
                    }
                }
            }
        }
         
        private void EmployeeUpdateBtn_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                jeffersonEntities dbContext = new jeffersonEntities();
                //Create an object called CurrentEmployee of type Employee1

                //This code gets the supervisor for the current selection, splits the name into first and last, and then finds the associated ID
                //Gets name of the supervisor for selected record
                var supervisorName = SupervisorCB.SelectionBoxItem.ToString();

                string Sup = supervisorName;
                //Ex: ger = Holly White

                var EmployeeName = Sup.Split(' ');
                //Ex: [1] = White   [0] = Holly
                string supervisorLastName = EmployeeName[1].ToString();
                string supervisorFirstName = EmployeeName[0].ToString();

                //runs a query to obtain ID
                int query_super_ID = (from Person in dbContext.People
                                      where Person.FirstName == supervisorFirstName &&
                                         Person.LastName == supervisorLastName
                                      select Person).First<Person>().personID;
                int superID = Convert.ToInt32(query_super_ID);
                Console.WriteLine(superID);




                //The update statement updates all fields. This enables the user to update any and all the information
                dbContext.Database.ExecuteSqlCommand("Update Person set FirstName = @FirstName, LastName = @LastName, Address = @Address, City = @City, State = @State, ZIP = @ZIP, Phone = @Phone, Email = @Email where PersonID = @EmployeeID",
                    //Assigns parameters values found in the textfields
                    new SqlParameter("FirstName", EmployeeFNameTxt.Text),
                    new SqlParameter("LastName", EmployeeLNameTxt.Text),
                    new SqlParameter("Address", EmployeeAddressTxt.Text),
                    new SqlParameter("City", EmployeeCityTxt.Text),
                    new SqlParameter("State", cbState.SelectionBoxItem),
                    new SqlParameter("Zip", EmployeeZipTxt.Text),
                    new SqlParameter("Phone", EmployeePhoneTxt.Text),
                    new SqlParameter("Email", EmployeeEmailTxt.Text),
                    new SqlParameter("EmployeeID", this.EmployeeID));
                //updates the Degree by using the EmployeeID so that we know we are updating the correct record
                dbContext.Database.ExecuteSqlCommand("Update Employee set DateHired = @DateHired, Wage = @Wage, SupervisorID = @SupervisorID  where EmployeeID = @EmployeeID",
                  new SqlParameter("DateHired", DateHired.SelectedDate.Value.ToShortDateString()),
                   new SqlParameter("Wage", EmployeeWageTxt.Text),
                   new SqlParameter("SupervisorID", superID),
                  new SqlParameter("EmployeeID", this.EmployeeID));

                String AddName = EmployeeFNameTxt.Text + " " + EmployeeLNameTxt.Text;
                

                //Resets the text fields
                EmployeeFNameTxt.Text = null;
                EmployeeLNameTxt.Text = null;
                DateHired.Text = null;
                EmployeeWageTxt.Text = null;
                SupervisorCB.SelectedIndex = -1;
                EmployeeAddressTxt.Text = null;
                EmployeeCityTxt.Text = null;
                cbState.SelectedIndex = -1;
                EmployeeZipTxt.Text = null;
                EmployeePhoneTxt.Text = null;
                EmployeeEmailTxt.Text = null;
                this.EmployeeID = null;

                

                //Populates the datagrid with the updated information
                var Employee = dbContext.Employees1.SqlQuery("select * from Employees").ToList();
                EmployeeDataGrid.ItemsSource = Employee;
                EmployeeDataGrid.SelectedIndex = Index;
                //confirmation message that record was updated
                MessageBox.Show(AddName + " Updated");

                EmployeeAddBtn.IsEnabled = true;
                EmployeeEditBtn.IsEnabled = true;
                
            }
            catch
            {
                
                MessageBox.Show("Please select an Employee from 'Current Employees' and then click the 'Edit Employee' Button");
            }
        }
        int Index;
        private void EmployeeEditBtn_Click_1(object sender, RoutedEventArgs e)
        {
            //Create an object called CurrentEmployee of type Employee1
            Employee1 CurrentEmployee = new Employee1();
            //CurrentEmployee holds the values of the selectedItem by casting the EmployeeDataGrid as type Employee1. This makes it possible to grab these values
            CurrentEmployee = (Employee1)EmployeeDataGrid.SelectedItem;
            Index = EmployeeDataGrid.SelectedIndex;

 

            //Populate the text fields with their respective data from the datagrid
            EmployeeFNameTxt.Text = CurrentEmployee.FirstName;
            EmployeeLNameTxt.Text = CurrentEmployee.LastName;
            DateHired.Text = CurrentEmployee.DateHired.ToString();
            EmployeeWageTxt.Text = CurrentEmployee.Wage.ToString();
            
            SupervisorCB.SelectedValue = CurrentEmployee.Supervisor;
            EmployeeAddressTxt.Text = CurrentEmployee.Address;
            EmployeeCityTxt.Text = CurrentEmployee.City;
            cbState.Text = CurrentEmployee.State;
            EmployeeZipTxt.Text = CurrentEmployee.ZIP;
            EmployeePhoneTxt.Text = CurrentEmployee.Phone;
            EmployeeEmailTxt.Text = CurrentEmployee.Email;

            //EmployeeID holds the EmployeeID of the record that is being updated
            this.EmployeeID = CurrentEmployee.EmployeeID.ToString();

            EmployeeEditBtn.IsEnabled = false;
            EmployeeAddBtn.IsEnabled = false;
        }

        private void EmployeeDeleteBtn_Click_1(object sender, RoutedEventArgs e)
        {
            Employee1 CurrentEmployee = new Employee1();
                    //CurrentEmployee holds the values of the selectedItem by casting the EmployeeDataGrid as type Employee1. This makes it possible to grab these values
                    CurrentEmployee = (Employee1)EmployeeDataGrid.SelectedItem;
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show( "Delete " + CurrentEmployee.FirstName +  " " + CurrentEmployee.LastName + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    //Create an object called CurrentEmployee of type Employee1
                    
                    EmployeeID = CurrentEmployee.EmployeeID.ToString();



                    //Execute the following query to delete the record from the Employee table using the EmployeeID
                    dbContext.Database.ExecuteSqlCommand("Delete from Employee where EmployeeID = @EmployeeID",
                        //assign parameters values
                        new SqlParameter("EmployeeID", this.EmployeeID));

                    //Execute the following query to delete the record from the person table using the person ID
                    dbContext.Database.ExecuteSqlCommand("Delete from Person where PersonID = @PersonID",
                         new SqlParameter("PersonID", this.EmployeeID));

                    String DeleteName = CurrentEmployee.FirstName + " " + CurrentEmployee.LastName;
                    MessageBox.Show(DeleteName + " deleted from Current Employees");

                    //Populated the datagrid so that it no longer contains the deleted record
                    var Employee = dbContext.Employees1.SqlQuery("select * from Employees").ToList();
                    EmployeeDataGrid.ItemsSource = Employee;
                    EmployeeDataGrid.SelectedIndex = 0;

                    EmployeeFNameTxt.Text = null;
                    EmployeeLNameTxt.Text = null;
                    DateHired.Text = null;
                    EmployeeWageTxt.Text = null;
                    SupervisorCB.SelectedIndex = -1;
                    EmployeeAddressTxt.Text = null;
                    EmployeeCityTxt.Text = null;
                    cbState.SelectedIndex = -1;
                    EmployeeZipTxt.Text = null;
                    EmployeePhoneTxt.Text = null;
                    EmployeeEmailTxt.Text = null;
                    this.EmployeeID = null;

                    EmployeeAddBtn.IsEnabled = true;
                    EmployeeEditBtn.IsEnabled = true;
                }
                catch
                {
                    MessageBox.Show("Please Assign Employees New Supervisor Before Deleting This Individual");
                }
            }
        }
        private void EmployeeFirstBtn_Click_1(object sender, RoutedEventArgs e)
        {
            //sets the datagrid index to zero to select the first record
            EmployeeDataGrid.SelectedIndex = 0;
        }

        private void EmployeePreviousBtn_Click_1(object sender, RoutedEventArgs e)
        {
            //selects the previous record by checking if the current index is greater than zero. if it is the selected record is selected by 
            //subtracting one from the current selected index
            if (EmployeeDataGrid.SelectedIndex > 0)
                EmployeeDataGrid.SelectedIndex = EmployeeDataGrid.SelectedIndex - 1;
        }

        private void EmployeeNextBtn_Click_1(object sender, RoutedEventArgs e)
        {
            //Selects the next record in the list by verifying if the selected index is less than the total number of records. if it is
            // one is added to the current selected index
            if (EmployeeDataGrid.SelectedIndex < EmployeeDataGrid.Items.Count)
                EmployeeDataGrid.SelectedIndex = EmployeeDataGrid.SelectedIndex + 1;
        }

        private void EmployeeLastBtn_Click_1(object sender, RoutedEventArgs e)
        {
            //Selects the last record in the datagrid by counting all of the records and subtracting one
            EmployeeDataGrid.SelectedIndex = EmployeeDataGrid.Items.Count - 1;
        }

    }

       
}
