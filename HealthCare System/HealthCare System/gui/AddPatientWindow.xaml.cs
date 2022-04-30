using HealthCare_System.entities;
using HealthCare_System.factory;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace HealthCare_System.gui
{
    public partial class AddPatientWindow : Window
    {
        bool isUpdate;
        HealthCareFactory factory;
        Patient patient;

        public AddPatientWindow(HealthCareFactory factory, bool isUpdate, Patient patient)
        {
            InitializeComponent();
            this.factory = factory;
            this.isUpdate = isUpdate;

            if(isUpdate)
            {
                this.patient = patient;
                passwordBox.Password = patient.Password;
                passwordBox.IsEnabled = false;
                textBoxJmbg.Text = patient.Jmbg;
                textBoxLastName.Text = patient.LastName;
                textBoxFirstName.Text = patient.FirstName;
                textBoxEmail.Text = patient.Mail;
                birthDatePicker.SelectedDate = patient.BirthDate;
                textBoxHeight.Text = patient.MedicalRecord.Height.ToString();
                textBoxWeight.Text = patient.MedicalRecord.Weight.ToString();
                textBoxHistory.Text = patient.MedicalRecord.DiseaseHistory.ToString();

                string allergies = "";
                foreach (Ingredient ingredient in patient.MedicalRecord.Allergens)
                {
                    allergies += ", " + ingredient.Name;
                }
                textBoxAlergies.Text = allergies;
                textBoxAlergies.IsEnabled = false;

            }
            else
            {
                this.patient = new Patient();
                textBoxAlergies.Text = "example1, example2";
            }
        }

        private void CancleBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(textBoxEmail.Text, 
                @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$") ||
                textBoxEmail.Text == "")
            {
                errormessage.Text = "Enter a valid email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
                return;
            }else if (passwordBox.Password.Length == 0)
            {
                errormessage.Text = "Enter password.";
                passwordBox.Focus();
                return;
            }
            string jmbg = textBoxJmbg.Text;
            string firstName = textBoxFirstName.Text;
            string lastName = textBoxLastName.Text;
            string mail = textBoxEmail.Text;
            double height;
            double weight;
            try
            {
                height = Convert.ToDouble(textBoxHeight.Text);
                weight = Convert.ToDouble(textBoxWeight.Text);
            }
            catch
            {
                height = 0;
                weight = 0;
            }
            string history = textBoxHistory.Text;
            string password = passwordBox.Password;
            DateTime birthDate = birthDatePicker.SelectedDate.Value;

            string[] ingredientNames;
            try
            {
                ingredientNames = textBoxAlergies.Text.Split(",");
            }
            catch
            {
                if (textBoxAlergies.Text != "")
                {
                    ingredientNames = new string[] { textBoxAlergies.Text };
                }
                else
                {
                    ingredientNames = new string[0];
                }
            }


            if (isUpdate)
            {
                patient.Jmbg = jmbg;
                patient.FirstName = firstName;
                patient.LastName = lastName;
                patient.Mail = mail;
                patient.BirthDate = birthDate;
                patient.MedicalRecord.Height = height;
                patient.MedicalRecord.Weight = weight;
                patient.MedicalRecord.DiseaseHistory = history;

                factory.UpdatePatient();

                Close();
                MessageBox.Show("You succesefully updated patient.");
            }
            else
            {
                List<Ingredient> ingredients = new List<Ingredient>();
                foreach (string name in ingredientNames)
                {
                    foreach (Ingredient ingredient in factory.IngredientController.Ingredients)
                    {
                        if (ingredient.Name.ToLower() == name)
                        {
                            ingredients.Add(ingredient);
                            continue;
                        }
                    }
                }
                MedicalRecord medRecord = factory.MedicalRecordController.Add(height, weight, history, ingredients);

                patient.Jmbg = jmbg;
                patient.FirstName = firstName;
                patient.LastName = lastName;
                patient.Mail = mail;
                patient.BirthDate = birthDate;
                patient.Password = password;

                factory.AddPatient(patient, medRecord);

                Close();
                MessageBox.Show("You succesefully registred new patient.");
            }

        }
    }
}
