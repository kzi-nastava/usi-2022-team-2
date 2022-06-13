using HealthCare_System.Model;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using HealthCare_System.Database;
using HealthCare_System.Services.UserServices;
using HealthCare_System.Model.Dto;
using HealthCare_System.Services.IngredientServices;
using HealthCare_System.Services.MedicalRecordServices;

namespace HealthCare_System.gui
{
    public partial class AddPatientWindow : Window
    {
        bool isUpdate;
        HealthCareDatabase database;
        Patient patient;
        PatientService patientService;
        IngredientService ingredientService;
        MedicalRecordService medicalRecordService;

        public AddPatientWindow(PatientService patientService, MedicalRecordService medicalRecordService,  bool isUpdate, Patient patient, HealthCareDatabase database)
        {
            InitializeComponent();
            this.patientService = patientService;
            this.medicalRecordService = medicalRecordService;
            this.isUpdate = isUpdate;
            this.database = database;

            this.ingredientService = new(database.IngredientRepo, null);

            if(isUpdate)
            {
                this.patient = patient;
                InitializeWinForUpdate();
            }
            else
            {
                this.patient = new Patient();
                textBoxAlergies.Text = "example1, example2";
            }
        }

        private void InitializeWinForUpdate()
        {
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
                foreach (Ingredient ingredient in ingredientService.Ingredients())
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

        private void UpdatePatient()
        {
            patient.Jmbg = textBoxJmbg.Text;
            patient.FirstName = textBoxFirstName.Text;
            patient.LastName = textBoxLastName.Text;
            patient.Mail = textBoxEmail.Text;
            patient.BirthDate = birthDatePicker.SelectedDate.Value;
            patient.MedicalRecord.Height = Convert.ToDouble(textBoxHeight.Text);
            patient.MedicalRecord.Weight = Convert.ToDouble(textBoxWeight.Text);
            patient.MedicalRecord.DiseaseHistory = textBoxHistory.Text;

            patientService.UpdatePatient();

            MessageBox.Show("You succesefully updated patient.");
        }

        private void CreatePatient()
        {
            PersonDto personDto = new(textBoxJmbg.Text, textBoxFirstName.Text, textBoxLastName.Text, textBoxEmail.Text, birthDatePicker.SelectedDate.Value, passwordBox.Password);

            List<Ingredient> ingredients = GetIngredients();
            MedicalRecord medRecord = medicalRecordService.Add(Convert.ToDouble(textBoxHeight.Text), Convert.ToDouble(textBoxWeight.Text), textBoxHistory.Text, ingredients);

            patientService.AddPatient(personDto, medRecord);

            MessageBox.Show("You succesefully registred new patient.");
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!ValidateInput()) { return; }

            if (isUpdate)
            {

                UpdatePatient();
            }
            else
            {
                CreatePatient();
            }
            Close();
        }
    }
}
