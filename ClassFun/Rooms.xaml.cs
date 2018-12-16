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
    //allows user to create, read, update, and delete rooms
    /// <summary>
    /// Interaction logic for Room.xaml
    /// </summary>
    public partial class Rooms : Window
    {
        jeffersonEntities dbContext = new jeffersonEntities();

        public Rooms()
        {
            InitializeComponent();
        }

    
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
       
        private void Room_Loaded(object sender, RoutedEventArgs e)
        {
            var Room = dbContext.Rooms1.SqlQuery("select * from Rooms").ToList();
            RoomDataGrid.ItemsSource = Room;
            RoomDataGrid.SelectedIndex = 0;            
            
        }

        private void RoomFirstBtn_Click(object sender, RoutedEventArgs e)
        {
            RoomDataGrid.SelectedIndex = 0;
        }

        private void RoomLastBtn_Click(object sender, RoutedEventArgs e)
        {
            RoomDataGrid.SelectedIndex = RoomDataGrid.Items.Count - 1;
        }

        private void RoomPreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            if (RoomDataGrid.SelectedIndex > 0)
                RoomDataGrid.SelectedIndex = RoomDataGrid.SelectedIndex - 1;
        }

        private void RoomNextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (RoomDataGrid.SelectedIndex < RoomDataGrid.Items.Count)
                RoomDataGrid.SelectedIndex = RoomDataGrid.SelectedIndex + 1;
        }

        private void RoomAddBtn_Click(object sender, RoutedEventArgs e)
        {
            //The if statement makes sure that you cannot add a room without a name
            if (RoomNameTxt1.Text == "")
            {
                MessageBox.Show("Please Enter Room Name");
            }
            else
            {
                //iMatch holds the value to know if the room name matches any name in current room
                int iMatch=0;
                //the for loop goes through all the records to check if there are any matches
                for (int iCount = 0; iCount < RoomDataGrid.Items.Count - 1; iCount++)
                {

                    Room1 CurrentRoom = new Room1();
                    RoomDataGrid.SelectedIndex = iCount;// set the index to that of the iCount 
                    CurrentRoom = (Room1)RoomDataGrid.SelectedItem;

                    //if the new name matches a record, one is added to iMatch
                    if (RoomNameTxt1.Text.ToLowerInvariant() ==CurrentRoom.RoomName.ToLowerInvariant())
                    {
                        iMatch++;
                    }
                    
                }
                //if iMatch is greater than zero it means that a room already has the new name
                if (iMatch > 0)
                {
                    //asks the user if they want to add the name even tho it already exists
                         MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Room already exists with this name, add anyway?", "Add Confirmation", System.Windows.MessageBoxButton.YesNo);
                         if (messageBoxResult == MessageBoxResult.Yes)
                         {
                             Console.WriteLine(iMatch);
                             String RoomName = RoomNameTxt1.Text + iMatch;
                             Console.WriteLine(RoomName);
                             dbContext.Database.ExecuteSqlCommand("Insert into Room (RoomName, RoomNumber) values (@RoomName, @RoomNumber)",
                                                      new SqlParameter("RoomName", RoomName),
                                                      new SqlParameter("RoomNumber", RoomDataGrid.Items.Count + 1));

                             var Room = dbContext.Rooms1.SqlQuery("select * from Rooms").ToList();
                             RoomDataGrid.ItemsSource = Room;
                             RoomDataGrid.SelectedIndex = 0;

                             //confirmation that the room is added
                             MessageBox.Show(RoomNameTxt1.Text + " added to Current Rooms");
                             //resets the textbox
                             RoomNameTxt1.Text = null;
                         }
                         else
                         {//resets the textbox
                             RoomNameTxt1.Text = null;
                         }
                }
                else
                    {
                            dbContext.Database.ExecuteSqlCommand("Insert into Room (RoomName, RoomNumber) values (@RoomName, @RoomNumber)",
                                                     new SqlParameter("RoomName", RoomNameTxt1.Text),
                                                     new SqlParameter("RoomNumber", RoomDataGrid.Items.Count + 1));

                            var Room = dbContext.Rooms1.SqlQuery("select * from Rooms").ToList();
                            RoomDataGrid.ItemsSource = Room;
                            RoomDataGrid.SelectedIndex = 0;

                            MessageBox.Show(RoomNameTxt1.Text + " added to Current Rooms");
                            //resets the textbox
                            RoomNameTxt1.Text = null;
                            
                        
                    }
                
            }
        }
        //RNum makes it possible to only update the record that was selected when the edit button was clicked
        String RNum;
        private void RoomDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            //Create varible of type Room1
            Room1 CurrentRoom = new Room1();
            CurrentRoom = (Room1)RoomDataGrid.SelectedItem;

            //asks the user if they really want to delete the record
             MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Delete " + CurrentRoom.RoomName + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
             if (messageBoxResult == MessageBoxResult.Yes)
             {
                 //executes the query to delete the record
                 dbContext.Database.ExecuteSqlCommand("Delete from Room where RoomNumber = @RoomNumber",
                     new SqlParameter("RoomNumber", CurrentRoom.RoomNumber));
                 //Sets RName to the value of the current room name that is selected so that it is displayed when the room is deleted
                 String RName = CurrentRoom.RoomName.ToString();
                 //Repopulates the Datagrid with the updated rooms
                 var Room = dbContext.Rooms1.SqlQuery("Select * from Rooms").ToList();
                 RoomDataGrid.ItemsSource = Room;
                 RoomDataGrid.SelectedIndex = 0;

                 RoomNameTxt1.Text = null;
                 RoomNumberTxt.Text = null;
                 RNum = null;

                 RoomAddBtn.IsEnabled = true;
                 RoomEditBtn.IsEnabled = true;
                 //confirmation that the room was deleted
                 MessageBox.Show(RName + " deleted from Current Rooms");

                 RoomNumberTxt.Visibility = System.Windows.Visibility.Collapsed;
                 RoomNumLbl.Visibility = System.Windows.Visibility.Collapsed;
             }
        }
        //Index makes it possible to selected the room that was updated
        int Index;
        private void RoomEditBtn_Click(object sender, RoutedEventArgs e)
        {
            //Create varible of type Room1
            Room1 CurrentRoom = new Room1();
            CurrentRoom = (Room1)RoomDataGrid.SelectedItem;
            //populates the texts fields by using the object CurrentRoom to grab the values
            RoomNameTxt1.Text = CurrentRoom.RoomName;
            RoomNumberTxt.Text = CurrentRoom.RoomNumber.ToString();
            
            //sets the index and RNum that we use in the update
            Index = RoomDataGrid.SelectedIndex;
            RNum = RoomNumberTxt.Text;

            //makes the number field visisble so that user knows which record is being updated
            RoomNumberTxt.Visibility = System.Windows.Visibility.Visible;
            RoomNumLbl.Visibility = System.Windows.Visibility.Visible;

            RoomAddBtn.IsEnabled = false;
            RoomEditBtn.IsEnabled = false;
        }

        private void RoomUpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            jeffersonEntities dbContext = new jeffersonEntities();
            
            try
            {
                //executes the query to update rooms
                dbContext.Database.ExecuteSqlCommand("Update Room set RoomName = @RoomName where RoomNumber = @RNum",
                    new SqlParameter("RoomName", RoomNameTxt1.Text),
                    new SqlParameter("RNum", RNum));


                var Room = dbContext.Rooms1.SqlQuery("select * from Rooms").ToList();
                RoomDataGrid.ItemsSource = Room;
                RoomDataGrid.SelectedIndex = Index;//selected the room that was updated
                MessageBox.Show(RoomNameTxt1.Text + " was Updated");//confirmation message that the room was updated

                //resets the values 
                RoomNameTxt1.Text = null;
                RoomNumberTxt.Text = null;
                RNum = null;
                Index = 0;

                //Makes the RoomNumber collapsed so that the user doesn't see it
                RoomNumberTxt.Visibility = System.Windows.Visibility.Collapsed;
                RoomNumLbl.Visibility = System.Windows.Visibility.Collapsed;

                RoomEditBtn.IsEnabled = true;
                RoomAddBtn.IsEnabled = true;
            }
            catch
            {
                //if the user hasn't selected a room from the datagrid and click the edit button it creates an error and this message is displayed
                MessageBox.Show("Please select a Room from Current Rooms, and click the 'Edit Room' button");
            }

            
        }
    }
}
