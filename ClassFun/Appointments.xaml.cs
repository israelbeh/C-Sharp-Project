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
    //description: allows user to create, read, update, and delete appointments
    /// <summary> 
    /// Interaction logic for Appointment.xaml
    /// </summary>
    public partial class Appointments : Window
    {
        jeffersonEntities dbContext = new jeffersonEntities();

        public Appointments()
        {
            InitializeComponent();
        }
        //loads the appointment datagrid and loads comboboxes
        private void Appointment_Loaded(object sender, RoutedEventArgs e)
        {
            //populates the datagrid with the Employee1 view information
            var Appointment = dbContext.Appointments1.SqlQuery("select * from Appointments").ToList();
            ApptDataGrid.ItemsSource = Appointment;
            ApptDataGrid.SelectedIndex = 0;

            //Loads Employee combobox
            String connectionString = "data source=cactus.arvixe.com;initial catalog=jefferson;persist security info=True;user id=ijbeh;password=cordoba7";
            /*Create a connection object. The method Get Connection String is a method that returns my connection string to the cactus.arvixe server. The String looks like this:"Data Source=cactus.arvixe.com;Initial Catalog=jefferson;Persist Security Info=True;User ID=YOURUSERNAMEHERE;Password=YOURPASSWORDHERE"*/
            /*You can store this as a String and pass it with a simple method.*/
            SqlConnection sqlConn = new SqlConnection(connectionString);

            /*This is the create select statement line */
            SqlDataAdapter sqlAdapt = new SqlDataAdapter(@"SELECT (FirstName+ ' ' +LastName)EmpName FROM Employees", sqlConn);

            /*Just do this part. You may not understand it. */
            SqlCommandBuilder sqlCmdBuilder = new SqlCommandBuilder(sqlAdapt);

            /*Create a dataset object to hold the data pulled*/
            DataSet sqlSetEmployee = new DataSet();
            sqlAdapt.Fill(sqlSetEmployee, "EmpName");

            /*Close the connection.*/
            sqlConn.Close();

            /*Create a datatable object that will be the location of the usable data pulled from the dataset */
            DataTable dtEmployee = new DataTable();

            /*Add the data from dataset to the datatable */
            dtEmployee = sqlSetEmployee.Tables["EmpName"];

            /*For ever piece of row data in the datatable.*/
            foreach (DataRow dr in dtEmployee.Rows)
            {
                /*Checks for duplicate entries and then add if not duplicate. The combobox is called cbCounselor by the way.*/
                if (!ScheduledByCB.Items.Contains(dr["EmpName"]))
                {
                    ScheduledByCB.Items.Add(dr["EmpName"]);
                }
            }

            //loads Counselor Combobox
            String connectionString1 = "data source=cactus.arvixe.com;initial catalog=jefferson;persist security info=True;user id=ijbeh;password=cordoba7";
            /*Create a connection object. The method Get Connection String is a method that returns my connection string to the cactus.arvixe server. The String looks like this:"Data Source=cactus.arvixe.com;Initial Catalog=jefferson;Persist Security Info=True;User ID=YOURUSERNAMEHERE;Password=YOURPASSWORDHERE"*/
            /*You can store this as a String and pass it with a simple method.*/
            SqlConnection sqlConn1 = new SqlConnection(connectionString1);

            /*This is the create select statement line */
            SqlDataAdapter sqlAdapt1 = new SqlDataAdapter(@"SELECT (FirstName+ ' ' +LastName) CName FROM Counselor1", sqlConn1);

            /*Just do this part. You may not understand it. */
            SqlCommandBuilder sqlCmdBuilder1 = new SqlCommandBuilder(sqlAdapt1);

            /*Create a dataset object to hold the data pulled*/
            DataSet sqlSetCounselor = new DataSet();
            sqlAdapt1.Fill(sqlSetCounselor, "CName");

            /*Close the connection.*/
            sqlConn1.Close();

            /*Create a datatable object that will be the location of the usable data pulled from the dataset */
            DataTable dtCounselor = new DataTable();

            /*Add the data from dataset to the datatable */
            dtCounselor = sqlSetCounselor.Tables["CName"];

            /*For ever piece of row data in the datatable.*/
            foreach (DataRow dr in dtCounselor.Rows)
            {
                /*Checks for duplicate entries and then add if not duplicate. The combobox is called cbCounselor by the way.*/
                if (!CounselorCB.Items.Contains(dr["CName"]))
                {
                    CounselorCB.Items.Add(dr["CName"]);
                }
            }

            //loads client combobox
            String connectionString2 = "data source=cactus.arvixe.com;initial catalog=jefferson;persist security info=True;user id=ijbeh;password=cordoba7";
            /*Create a connection object. The method Get Connection String is a method that returns my connection string to the cactus.arvixe server.
             * The String looks like this:"Data Source=cactus.arvixe.com;Initial Catalog=jefferson;Persist Security Info=True;User ID=YOURUSERNAMEHERE;Password=YOURPASSWORDHERE"*/
            /*You can store this as a String and pass it with a simple method.*/
            SqlConnection sqlConn2 = new SqlConnection(connectionString2);

            /*This is the create select statement line */
            SqlDataAdapter sqlAdapt2 = new SqlDataAdapter(@"SELECT (FirstName+ ' ' +LastName)ClName FROM Clients", sqlConn2);

            /*Just do this part. You may not understand it. */
            SqlCommandBuilder sqlCmdBuilder2 = new SqlCommandBuilder(sqlAdapt2);

            /*Create a dataset object to hold the data pulled*/
            DataSet sqlSetClient = new DataSet();
            sqlAdapt2.Fill(sqlSetClient, "ClName");

            /*Close the connection.*/
            sqlConn2.Close();

            /*Create a datatable object that will be the location of the usable data pulled from the dataset */
            DataTable dtClient = new DataTable();

            /*Add the data from dataset to the datatable */
            dtClient = sqlSetClient.Tables["ClName"];

            /*For ever piece of row data in the datatable.*/
            foreach (DataRow dr in dtClient.Rows)
            {
                /*Checks for duplicate entries and then add if not duplicate. The combobox is called cbCounselor by the way.*/
                if (!ClientCB.Items.Contains(dr["ClName"]))
                {
                    ClientCB.Items.Add(dr["ClName"]);
                }
            }

            //loads room combobox
            String connectionString3 = "data source=cactus.arvixe.com;initial catalog=jefferson;persist security info=True;user id=ijbeh;password=cordoba7";
            /*Create a connection object. The method Get Connection String is a method that returns my connection string to the cactus.arvixe server. The String looks like this:"Data Source=cactus.arvixe.com;Initial Catalog=jefferson;Persist Security Info=True;User ID=YOURUSERNAMEHERE;Password=YOURPASSWORDHERE"*/
            /*You can store this as a String and pass it with a simple method.*/
            SqlConnection sqlConn3 = new SqlConnection(connectionString3);

            /*This is the create select statement line */
            SqlDataAdapter sqlAdapt3 = new SqlDataAdapter(@"SELECT RoomName FROM Room", sqlConn3);

            /*Just do this part. You may not understand it. */
            SqlCommandBuilder sqlCmdBuilder3 = new SqlCommandBuilder(sqlAdapt3);

            /*Create a dataset object to hold the data pulled*/
            DataSet sqlSetRoom = new DataSet();
            sqlAdapt3.Fill(sqlSetRoom, "RoomName");

            /*Close the connection.*/
            sqlConn3.Close();

            /*Create a datatable object that will be the location of the usable data pulled from the dataset */
            DataTable dtRoom = new DataTable();

            /*Add the data from dataset to the datatable */
            dtRoom = sqlSetRoom.Tables["RoomName"];

            /*For ever piece of row data in the datatable.*/
            foreach (DataRow dr in dtRoom.Rows)
            {
                /*Checks for duplicate entries and then add if not duplicate. The combobox is called cbCounselor by the way.*/
                if (!RoomCB.Items.Contains(dr["RoomName"]))
                {
                    RoomCB.Items.Add(dr["RoomName"]);
                }
            }

        }
        //holds the appointmentID and CLientID so that when each is updated it updates the right record
        String AppointmentID;
        String ClientID;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ApptAddBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                 //iMatch holds the value to know if the appointment name matches any name in current room
                int iMatch = 0;
                //the for loop goes through all the records to check if there are any matches
                for (int iCount = 0; iCount < ApptDataGrid.Items.Count - 1; iCount++)
                {
                    //create an object of type appointment1 to access appointment information from datagrid
                    Appointment1 CurrentAppt = new Appointment1();
                    ApptDataGrid.SelectedIndex = iCount;// set the index to that of the iCount 
                    CurrentAppt = (Appointment1)ApptDataGrid.SelectedItem;
                    String Date = ApptDate.SelectedDate.Value.ToShortDateString();
                    String Time = (String)TimeCB.SelectionBoxItem;

                    String DateTime = Date + " " + Time;
                    //if the new name matches a record, one is added to iMatch
                    if (DateTime == CurrentAppt.StartTime.ToString() && 
                        ClientCB.SelectionBoxItem.ToString() == CurrentAppt.Client &&
                        RoomCB.SelectionBoxItem.ToString()  == CurrentAppt.RoomName &&
                        CounselorCB.SelectionBoxItem.ToString() == CurrentAppt.Counselor)
                    {
                        iMatch++;
                    }
                }
                if (iMatch > 0)
                {
                    //asks the user if they want to add the name even tho it already exists
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("An Appointment already exists with this name, add anyway?", "Add Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        string Counselor = CounselorCB.SelectionBoxItem.ToString();
                        //Ex: ger = Holly White

                        var CounselorName = Counselor.Split(' ');
                        //Ex: [1] = White   [0] = Holly
                        string CounselorLastName = CounselorName[1].ToString();
                        string CounselorFirstName = CounselorName[0].ToString();

                        //runs query
                        int query_super_ID = (from Person in dbContext.People
                                              where Person.FirstName == CounselorFirstName &&
                                                 Person.LastName == CounselorLastName
                                              select Person).First<Person>().personID;
                        int CLRID = Convert.ToInt32(query_super_ID);

                        //Separate Employee First Name and Last Name
                        string Employee = ScheduledByCB.SelectionBoxItem.ToString();
                        //Ex: ger = Holly White

                        var EmployeeName = Employee.Split(' ');
                        //Ex: [1] = White   [0] = Holly
                        string EmployeeLastName = EmployeeName[1].ToString();
                        string EmployeeFirstName = EmployeeName[0].ToString();

                        //runs query
                        int query_Emp_ID = (from Person in dbContext.People
                                            where Person.FirstName == EmployeeFirstName &&
                                               Person.LastName == EmployeeLastName
                                            select Person).First<Person>().personID;
                        int EmpID = Convert.ToInt32(query_Emp_ID);

                        //Client
                        string Client = ClientCB.SelectionBoxItem.ToString();
                        //Ex: ger = Holly White

                        var ClientName = Client.Split(' ');
                        //Ex: [1] = White   [0] = Holly
                        string ClientLastName = ClientName[1].ToString();
                        string ClientFirstName = ClientName[0].ToString();

                        int query_Client_ID = (from Person in dbContext.People
                                               where Person.FirstName == ClientFirstName &&
                                                  Person.LastName == ClientLastName
                                               select Person).First<Person>().personID;
                        int CID = Convert.ToInt32(query_Client_ID);
                        //concatenates the Date and TIme combobox
                        String Date = ApptDate.SelectedDate.Value.ToShortDateString();
                        String Time = (String)TimeCB.SelectionBoxItem;

                        String DateTime = Date + " " + Time;
                        //runs query
                        dbContext.Database.ExecuteSqlCommand("Insert into Appointment(StartTime, Duration, CounselorID, EmployeeID, RoomNumber, Notes) values (@StartTime, @Duration, @Counselor,@Employee, (Select RoomNumber from Room where RoomName =@RoomName), @Notes)",
                            new SqlParameter("StartTime", DateTime),
                            new SqlParameter("Duration", DurationCB.SelectionBoxItem),
                            new SqlParameter("Counselor", CLRID),
                            new SqlParameter("Employee", EmpID),
                            new SqlParameter("RoomName", RoomCB.SelectionBoxItem),
                            new SqlParameter("Notes", NotesTxt.Text));
                        dbContext.Database.ExecuteSqlCommand("Insert into ScheduledFor (AppointmentID, ClientID) values((Select max(AppointmentID) from appointment),  @Client)",
                            new SqlParameter("Client", CID));

                        //populates the datagrid with the Employee1 view information
                        var Appointment = dbContext.Appointments1.SqlQuery("select * from Appointments").ToList();
                        ApptDataGrid.ItemsSource = Appointment;
                        ApptDataGrid.SelectedIndex = 0;

                        MessageBox.Show("An Appointment was added for " + ClientCB.SelectionBoxItem.ToString());

                        //Resets the text fields
                        ApptDate.Text = null;
                        TimeCB.SelectedIndex = -1;
                        DurationCB.SelectedIndex = -1;
                        CounselorCB.SelectedIndex = -1;
                        ScheduledByCB.SelectedIndex = -1;
                        RoomCB.SelectedIndex = -1;
                        NotesTxt.Text = null;
                        ClientCB.SelectedIndex = -1;
                        AppointmentID = null;
                    }
                    else
                    {
                        //resets the fields
                        ApptDate.Text = null;
                        TimeCB.SelectedIndex = -1;
                        DurationCB.SelectedIndex = -1;
                        CounselorCB.SelectedIndex = -1;
                        ScheduledByCB.SelectedIndex = -1;
                        RoomCB.SelectedIndex = -1;
                        NotesTxt.Text = null;
                        ClientCB.SelectedIndex = -1;
                        AppointmentID = null;
                    }
                }
                else
                {
                    string Counselor = CounselorCB.SelectionBoxItem.ToString();
                    //Ex: ger = Holly White

                    var CounselorName = Counselor.Split(' ');
                    //Ex: [1] = White   [0] = Holly
                    string CounselorLastName = CounselorName[1].ToString();
                    string CounselorFirstName = CounselorName[0].ToString();


                    int query_super_ID = (from Person in dbContext.People
                                          where Person.FirstName == CounselorFirstName &&
                                             Person.LastName == CounselorLastName
                                          select Person).First<Person>().personID;
                    int CLRID = Convert.ToInt32(query_super_ID);

                    //Separate Employee First Name and Last Name
                    string Employee = ScheduledByCB.SelectionBoxItem.ToString();
                    //Ex: ger = Holly White

                    var EmployeeName = Employee.Split(' ');
                    //Ex: [1] = White   [0] = Holly
                    string EmployeeLastName = EmployeeName[1].ToString();
                    string EmployeeFirstName = EmployeeName[0].ToString();


                    int query_Emp_ID = (from Person in dbContext.People
                                        where Person.FirstName == EmployeeFirstName &&
                                           Person.LastName == EmployeeLastName
                                        select Person).First<Person>().personID;
                    int EmpID = Convert.ToInt32(query_Emp_ID);

                    //Client
                    string Client = ClientCB.SelectionBoxItem.ToString();
                    //Ex: ger = Holly White

                    var ClientName = Client.Split(' ');
                    //Ex: [1] = White   [0] = Holly
                    string ClientLastName = ClientName[1].ToString();
                    string ClientFirstName = ClientName[0].ToString();

                    int query_Client_ID = (from Person in dbContext.People
                                           where Person.FirstName == ClientFirstName &&
                                              Person.LastName == ClientLastName
                                           select Person).First<Person>().personID;
                    int CID = Convert.ToInt32(query_Client_ID);

                    String Date = ApptDate.SelectedDate.Value.ToShortDateString();
                    String Time = (String)TimeCB.SelectionBoxItem;

                    String DateTime = Date + " " + Time;

                    dbContext.Database.ExecuteSqlCommand("Insert into Appointment(StartTime, Duration, CounselorID, EmployeeID, RoomNumber, Notes) values (@StartTime, @Duration, @Counselor,@Employee, (Select RoomNumber from Room where RoomName =@RoomName), @Notes)",
                        new SqlParameter("StartTime", DateTime),
                        new SqlParameter("Duration", DurationCB.SelectionBoxItem),
                        new SqlParameter("Counselor", CLRID),
                        new SqlParameter("Employee", EmpID),
                        new SqlParameter("RoomName", RoomCB.SelectionBoxItem),
                        new SqlParameter("Notes", NotesTxt.Text));
                    dbContext.Database.ExecuteSqlCommand("Insert into ScheduledFor (AppointmentID, ClientID) values((Select max(AppointmentID) from appointment),  @Client)",
                        new SqlParameter("Client", CID));

                    //populates the datagrid with the Employee1 view information
                    var Appointment = dbContext.Appointments1.SqlQuery("select * from Appointments").ToList();
                    ApptDataGrid.ItemsSource = Appointment;
                    ApptDataGrid.SelectedIndex = 0;

                    MessageBox.Show("An Appointment was added for " + ClientCB.SelectionBoxItem.ToString());

                    //Resets the text fields
                    ApptDate.Text = null;
                    TimeCB.SelectedIndex = -1;
                    DurationCB.SelectedIndex = -1;
                    CounselorCB.SelectedIndex = -1;
                    ScheduledByCB.SelectedIndex = -1;
                    RoomCB.SelectedIndex = -1;
                    NotesTxt.Text = null;
                    ClientCB.SelectedIndex = -1;
                    AppointmentID = null;
                }
            }
            catch
            {
                MessageBox.Show("Please Enter All information");
            }
        }

        private void ApptUpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                jeffersonEntities dbContext = new jeffersonEntities();
                //concatenates date and time  so that you can get the right Starttime
                String Date = ApptDate.SelectedDate.Value.ToShortDateString();
                String Time = (String)TimeCB.SelectionBoxItem;


                String DateTime = Date + " " + Time;


                string Counselor = CounselorCB.SelectionBoxItem.ToString();
                //Ex: ger = Holly White

                var CounselorName = Counselor.Split(' ');
                //Ex: [1] = White   [0] = Holly
                string CounselorLastName = CounselorName[1].ToString();
                string CounselorFirstName = CounselorName[0].ToString();


                int query_Couns_ID = (from Person in dbContext.People
                                      where Person.FirstName == CounselorFirstName &&
                                         Person.LastName == CounselorLastName
                                      select Person).First<Person>().personID;
                int CLRID = Convert.ToInt32(query_Couns_ID);

                //Separate Employee First Name and Last Name
                string Employee = ScheduledByCB.SelectionBoxItem.ToString();
                //Ex: ger = Holly White

                var EmployeeName = Employee.Split(' ');
                //Ex: [1] = White   [0] = Holly
                string EmployeeLastName = EmployeeName[1].ToString();
                string EmployeeFirstName = EmployeeName[0].ToString();


                int query_Emp_ID = (from Person in dbContext.People
                                    where Person.FirstName == EmployeeFirstName &&
                                       Person.LastName == EmployeeLastName
                                    select Person).First<Person>().personID;
                int EmpID = Convert.ToInt32(query_Emp_ID);

                //Client
                string Client = ClientCB.SelectionBoxItem.ToString();
                //Ex: ger = Holly White

                var ClientName = Client.Split(' ');
                //Ex: [1] = White   [0] = Holly
                string ClientLastName = ClientName[1].ToString();
                string ClientFirstName = ClientName[0].ToString();


                int query_Client_ID = (from Person in dbContext.People
                                       where Person.FirstName == ClientFirstName &&
                                          Person.LastName == ClientLastName
                                       select Person).First<Person>().personID;
                int CID = Convert.ToInt32(query_Client_ID);

                //The update statement updates all fields. This enables the user to update any and all the information
                dbContext.Database.ExecuteSqlCommand("Update Appointment set StartTime = @StartTime, Duration = @Duration, CounselorID =@Counselor, EmployeeID = @Employee, RoomNumber = (Select RoomNumber from Room where RoomName =@RoomName), Notes = @Notes where AppointmentID = @AppointmentID",
                    //Assigns parameters values found in the textfields
                   new SqlParameter("StartTime", DateTime),
                    new SqlParameter("Duration", DurationCB.SelectionBoxItem),
                    new SqlParameter("Counselor", CLRID),
                    new SqlParameter("Employee", EmpID),
                    new SqlParameter("RoomName", RoomCB.SelectionBoxItem),
                    new SqlParameter("Notes", NotesTxt.Text),
                    new SqlParameter("AppointmentID", this.AppointmentID));
                //updates the Degree by using the ClientID so that we know we are updating the correct record
                dbContext.Database.ExecuteSqlCommand("UPDATE ScheduledFor SET ClientID = @NewClientID  WHERE AppointmentID = @AppointmentID AND ClientID = (Select ClientID from ScheduledFor where AppointmentID =@AppointmentID)",
                      new SqlParameter("AppointmentID", this.AppointmentID),
                      new SqlParameter("NewClientID", CID));
                Console.WriteLine(CID);
                Console.WriteLine(this.ClientID);

                var Appointment = dbContext.Appointments1.SqlQuery("select * from Appointments").ToList();
                ApptDataGrid.ItemsSource = Appointment;
                ApptDataGrid.SelectedIndex = Index;

                MessageBox.Show("Appointment #" + AppointmentID + " was Updated");
                //Resets the text fields
                ApptDate.Text = null;
                TimeCB.SelectedIndex = -1;
                DurationCB.SelectedIndex = -1;
                CounselorCB.SelectedIndex = -1;
                ScheduledByCB.SelectedIndex = -1;
                RoomCB.SelectedIndex = -1;
                NotesTxt.Text = null;
                ClientCB.SelectedIndex = -1;
                AppointmentID = null;

                ApptAddBtn.IsEnabled = true;
                ApptEditBtn.IsEnabled = true;

            }
            catch
            {
                MessageBox.Show("Please Select an Appointment from 'Appointments', and then Click 'Edit Appointment'");
            }
        }
        int Index;
        private void ApptEditBtn_Click(object sender, RoutedEventArgs e)
        {
            //Create an object called CurrentAppointment of type Appointment1
            Appointment1 CurrentAppointment = new Appointment1();
            //CurrentAppointment holds the values of the selectedItem by casting the AppointmentDataGrid as type Appointment1. This makes it possible to grab these values
            CurrentAppointment = (Appointment1)ApptDataGrid.SelectedItem;
            Index = ApptDataGrid.SelectedIndex;

            //ScheduledFor CurrentClient = new ScheduledFor();

            CounselorCB.SelectedValue = CurrentAppointment.Counselor;
 
            ClientCB.SelectedValue = CurrentAppointment.Client;
//this separates the first name from the last name so that I can use it to select the value in the combo box
    
            ScheduledByCB.SelectedValue = CurrentAppointment.ScheduledBy;

            string DateTime = CurrentAppointment.StartTime.ToString();
            //Ex: ger = Holly White

            //spits the time
            var DTime = DateTime.Split(' ');
            //Ex: [1] = White   [0] = Holly
            string Time = DTime[1].ToString();
            string AMPM = DTime[2].ToString();
            string Date = DTime[0].ToString();

            String FinalTime = Time + " " + AMPM;


            //Populate the text fields with their respective data from the datagrid
            ApptDate.Text =  Date;
            TimeCB.Text = FinalTime;
            RoomCB.Text = CurrentAppointment.RoomName;
            NotesTxt.Text = CurrentAppointment.Notes;
            DurationCB.Text = CurrentAppointment.Duration.ToString();

            //AppointmentID holds the AppointmentID of the record that is being updated
            this.AppointmentID = CurrentAppointment.AppointmentID.ToString();

            ApptEditBtn.IsEnabled = false;
            ApptAddBtn.IsEnabled = false;
            
        }

        private void ClientFirstBtn_Click(object sender, RoutedEventArgs e)
        {
            //sets the datagrid index to zero to select the first record
            ApptDataGrid.SelectedIndex = 0;
        }

        private void ClientPreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            //selects the previous record by checking if the current index is greater than zero. if it is the selected record is selected by 
            //subtracting one from the current selected index
            if (ApptDataGrid.SelectedIndex > 0)
                ApptDataGrid.SelectedIndex = ApptDataGrid.SelectedIndex - 1;
        }

        private void ClientNextBtn_Click(object sender, RoutedEventArgs e)
        {
            //Selects the next record in the list by verifying if the selected index is less than the total number of records. if it is
            // one is added to the current selected index
            if (ApptDataGrid.SelectedIndex < ApptDataGrid.Items.Count)
                ApptDataGrid.SelectedIndex = ApptDataGrid.SelectedIndex + 1;
        }

        private void ClientLastBtn_Click(object sender, RoutedEventArgs e)
        {
            //Selects the last record in the datagrid by counting all of the records and subtracting one
            ApptDataGrid.SelectedIndex = ApptDataGrid.Items.Count - 1;
        }

        private void ApptDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            //Create an object called CurrentAppt of type Appt1
            Appointment1 CurrentAppt = new Appointment1();
            //CurrentAppt holds the values of the selectedItem by casting the ApptDataGrid as type Appt1. This makes it possible to grab these values
            CurrentAppt = (Appointment1)ApptDataGrid.SelectedItem;
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show( "Delete Appointment for " + CurrentAppt.Client + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                
                AppointmentID = CurrentAppt.AppointmentID.ToString();

                //Execute the following query to delete the record from the Appt table using the ApptID
                dbContext.Database.ExecuteSqlCommand("Delete from Appointment where AppointmentID = @ApptID",
                    //assign parameters values
                    new SqlParameter("ApptID", this.AppointmentID));

                //Execute the following query to delete the record from the person table using the person ID
                // dbContext.Database.ExecuteSqlCommand("Delete from Person where PersonID = @PersonID",
                //new SqlParameter("PersonID", this.AppointmentID));
                var Appt = dbContext.Appointments1.SqlQuery("select * from Appointments").ToList();
                ApptDataGrid.ItemsSource = Appt;

                MessageBox.Show("An Appointment for " + CurrentAppt.Client.ToString() + " was deleted");

                //Populated the datagrid so that it no longer contains the deleted record
                
                ApptDataGrid.SelectedIndex = 0;

                //Resets the text fields
                ApptDate.Text = null;
                TimeCB.SelectedIndex = -1;
                DurationCB.SelectedIndex = -1;
                CounselorCB.SelectedIndex = -1;
                ScheduledByCB.SelectedIndex = -1;
                RoomCB.SelectedIndex = -1;
                NotesTxt.Text = null;
                ClientCB.SelectedIndex = -1;
                AppointmentID = null;

                ApptAddBtn.IsEnabled = true;
                ApptEditBtn.IsEnabled = true;
            }
            
        
        }

     
    }
}
