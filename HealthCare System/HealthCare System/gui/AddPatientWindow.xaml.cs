using HealthCare_System.Model;
using HealthCare_System.factory;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using HealthCare_System.Database;

namespace HealthCare_System.gui
{
    public partial class AddPatientWindow : Window
    {
        bool isUpdate;
        HealthCareFactory factory;
        HealthCareDatabase database;

        Patient patient;

        public AddPatientWindow(HealthCareFactory factory, bool isUpdate, Patient patient, HealthCareDatabase database)
        {
            InitializeComponent();
            this.factory = factory;
            this.isUpdate = isUpdate;
            this.database = database;

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

        private List<Ingredient> GetIngredients()
        {
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

            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (string name in ingredientNames)
            {
                foreach (Ingredient ingredient in factory.IngredientController.Ingredients)
                {
                    if (ingredient.Name.ToLower() == name.ToLower())
                    {
                        ingredients.Add(ingredient);
                        continue;
                    }
                }
            }
            return ingredients;
        }

        private bool ValidateInput()
        {
            if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$") || textBoxEmail.Text == "")
            {
                errormessage.Text = "Enter a valid email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
                return false;
            }
            else if (passwordBox.Password.Length == 0)
            {
                errormessage.Text = "Enter password.";
                passwordBox.Focus();
                return false;
            }

            try
            {
                double height = Convert.ToDouble(textBoxHeight.Text);
                double weight = Convert.ToDouble(textBoxWeight.Text);
                DateTime date = birthDatePicker.SelectedDate.Value;
            }
            catch (FormatException)
            {
                errormessage.Text = "Enter a valid height or weight.";
                textBoxHeight.Focus();
                textBoxWeight.Focus();
                return false;
            }
            catch
            {
                errormessage.Text = "Pick a date.";
                birthDatePicker.Focus();
                return false;
            }
            return true;
        }

        private void UpdatePatient(double height, double weight, string history)
        {
            patient.MedicalRecord.Height = height;
            patient.MedicalRecord.Weight = weight;
            patient.MedicalRecord.DiseaseHistory = history;

            factory.UpdatePatient();

            MessageBox.Show("You succesefully updated patient.");
        }

        private void CreatePatient(double height, double weight, string history)
        {
            patient.Password = passwordBox.Password;

            List<Ingredient> ingredients = GetIngredients();
            MedicalRecord medRecord = factory.MedicalRecordController.Add(height, weight, history, ingredients);

            factory.AddPatient(patient, medRecord);

            MessageBox.Show("You succesefully registred new patient.");
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!ValidateInput()) { return; }

            patient.Jmbg = textBoxJmbg.Text;
            patient.FirstName = textBoxFirstName.Text;
            patient.LastName = textBoxLastName.Text;
            patient.Mail = textBoxEmail.Text;
            patient.BirthDate = birthDatePicker.SelectedDate.Value;
            double height = Convert.ToDouble(textBoxHeight.Text);
            double weight = Convert.ToDouble(textBoxWeight.Text);
            string history = textBoxHistory.Text;

            if (isUpdate)
            {

                UpdatePatient(height, weight, history);
            }
            else
            {
                CreatePatient(height, weight, history);
            }
            Close();
        }
    }
}
