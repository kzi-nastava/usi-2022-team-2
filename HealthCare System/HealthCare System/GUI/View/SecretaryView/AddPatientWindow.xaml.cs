﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using HealthCare_System.Core.Ingredients;
using HealthCare_System.Core.Ingredients.Model;
using HealthCare_System.Core.MedicalRecords;
using HealthCare_System.Core.MedicalRecords.Model;
using HealthCare_System.Core.Users;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.Database;
using HealthCare_System.GUI.Controller.Ingredients;
using HealthCare_System.GUI.Controller.MedicalRecords;
using HealthCare_System.GUI.Controller.Users;

namespace HealthCare_System.GUI.SecretaryView
{
    public partial class AddPatientWindow : Window
    {
        bool isUpdate;
        ServiceBuilder serviceBuilder;
        Patient patient;
        PatientController patientController;
        IngredientController ingredientController;
        MedicalRecordController medicalRecordController;

        public AddPatientWindow(PatientController patientController, MedicalRecordController medicalRecordController, IIngredientService ingredientService, bool isUpdate, Patient patient)
        {
            InitializeComponent();
            this.patientController = patientController;
            this.medicalRecordController = medicalRecordController;
            this.isUpdate = isUpdate;

            this.ingredientController = new(ingredientService);

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
                foreach (Ingredient ingredient in ingredientController.Ingredients())
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

            patientController.UpdatePatient();

            MessageBox.Show("You succesefully updated patient.");
        }

        private void CreatePatient()
        {
            PersonDto personDto = new(textBoxJmbg.Text, textBoxFirstName.Text, textBoxLastName.Text, textBoxEmail.Text, birthDatePicker.SelectedDate.Value, passwordBox.Password);

            List<Ingredient> ingredients = GetIngredients();
            MedicalRecord medRecord = medicalRecordController.Add(Convert.ToDouble(textBoxHeight.Text), Convert.ToDouble(textBoxWeight.Text), textBoxHistory.Text, ingredients);

            patientController.AddPatient(personDto, medRecord);

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
