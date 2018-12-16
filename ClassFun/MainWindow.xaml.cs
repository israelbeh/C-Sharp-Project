using System;
using System.Collections.Generic;
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

//Author: Israel Beh
/*Description: A program to schedule appointments. This program allows you to Create, Read, Update, and Delete 
 Appointments, Clients, Counselors, Employees, and Rooms. You can navigate to each page either by the menu or by the 
 clicking the buttons for each window.
 * 
 * Date last Modified: 11/15/2014 10:10 AM
*/

namespace ClassFun
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
       
// Create navigation objects and click events
        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Counselor_Click(object sender, RoutedEventArgs e)
        {
            //Opens Counselor window
            Counselors frmCounselor=new Counselors();

            frmCounselor.ShowDialog();
            this.Show();
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            //Opens client window
            Clients frmClient = new Clients();

            frmClient.ShowDialog();
            this.Show();
        }

        private void Room_Click(object sender, RoutedEventArgs e)
        {
            //opens room window
            Rooms frmRoom = new Rooms();

            frmRoom.ShowDialog();
            this.Show();
        }
        private void Employee_Click(object sender, RoutedEventArgs e)
        {

            //opens the employee window
            Employees frmEmployee = new Employees();

            frmEmployee.ShowDialog();
            this.Show();
        }
        private void Appointment_Click(object sender, RoutedEventArgs e)
        {
            //opens the appointment window
           Appointments frmAppointment = new Appointments();

            frmAppointment.ShowDialog();
            this.Show();
        }

        private void CounselorBtn_Click(object sender, RoutedEventArgs e)
        {
            //opens the counselor window when counselor button is clicked
            Counselors frmCounselor = new Counselors();

            frmCounselor.ShowDialog();
            this.Show();
        }

        private void ClientBtn_Click(object sender, RoutedEventArgs e)
        {
            //opens the client window when client button is clicked
            Clients frmClient = new Clients();

            frmClient.ShowDialog();
            this.Show();
        }

        private void EmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            //opens the employee window when employee button is clicked
            Employees frmEmployee = new Employees();

            frmEmployee.ShowDialog();
            this.Show();
        }

        private void RoomBtn_Click(object sender, RoutedEventArgs e)
        {
            //opens the Room window when counselor button is clicked
            Rooms frmRoom = new Rooms();

            frmRoom.ShowDialog();
            this.Show();
        }

        private void ApptBtn_Click(object sender, RoutedEventArgs e)
        {
            //opens the cappointment window when appointment button is clicked
            Appointments frmAppointment = new Appointments();

            frmAppointment.ShowDialog();
            this.Show();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {//closes the window when clicked
            this.Close();
        }
    }
}
