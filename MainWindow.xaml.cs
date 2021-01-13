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

namespace ContactApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool checkSelected = false;
        Contact Person = new Contact();
        public MainWindow()
        {
            InitializeComponent();
            DataShow();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }
        void Clear()
        {
            txtName.Text = txtLastName.Text = txtMail.Text = txtPhone.Text = "";
            btnSave.Content = "Save";
            btnDelete.IsEnabled = false;
            btnUpdate.IsEnabled = false;
            Person.Id = 0;
        }

        private void MWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Clear();
            DataShow();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Person.Name = txtName.Text.Trim();
            Person.LastName = txtLastName.Text.Trim();
            Person.Phone = txtPhone.Text.Trim();
            Person.Mail = txtMail.Text.Trim();
            using (DBmodel db = new DBmodel())
            {
                db.Contacts.Add(Person);
                db.SaveChanges();
            }
            Clear();
            MessageBox.Show("Person Added Succesfully.");
            DataShow();
        }
        void DataShow()
        {
            using(DBmodel db = new DBmodel())
            {
                grContact.ItemsSource = db.Contacts.ToList<Contact>();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //if (grContact.CurrentCell.Item == null)
            //{
            //    return;
            //}

            Contact person = grContact.SelectedCells[0].Item as Contact;
 

            using (DBmodel db = new DBmodel())
            {
                var entry = db.Entry(person);
                if (entry.State == System.Data.Entity.EntityState.Detached)
                {
                    db.Contacts.Attach(person);
                }
                db.Contacts.Remove(person);
                db.SaveChanges();
            }
            MessageBox.Show("Contact Deleted.");

            DataShow();
            Clear();

        }

        private void currentCell_changed(object sender, EventArgs e)
        {
            btnDelete.IsEnabled = true;
            btnUpdate.IsEnabled = true;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Contact person = grContact.SelectedCells[0].Item as Contact;
            using (DBmodel db = new DBmodel())
            {
                var entry = db.Entry(person);
                if (entry.State == System.Data.Entity.EntityState.Detached)
                {
                    db.Contacts.Attach(person);
                }
                person.Name = txtName.Text.Trim();
                person.LastName = txtLastName.Text.Trim();
                person.Phone = txtPhone.Text.Trim();
                person.Mail = txtMail.Text.Trim();
                db.SaveChanges();
            }
            MessageBox.Show("Contact Modified.");
            DataShow();
            Clear();
        }


    }
}
