using System.Windows;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace HealthCare_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    class Osoba
    {
        
    

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        private string mail;

        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private string birthDate;

        public string BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }








    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Osoba> persons = JsonConvert.DeserializeObject<List<Osoba>>(File.ReadAllText("../../../data/usersLogin.json"));
            for(int i = 0; i < persons.Count; i++)
            {
                lb.Items.Add(persons[i].Mail);
            }

        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string mail = mailTb.Text;
            string password = passTb.Password;
            
            

        }
    }
}
