using HealthCare_System.entities;
using HealthCare_System.factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HealthCare_System.gui
{
    public partial class PrescriptionWindow : Window
    {
        HealthCareFactory factory;
        Patient patient;
        Dictionary<string, Drug> drugsDisplay;

        public PrescriptionWindow(Patient patient, HealthCareFactory factory)
        {
            this.factory = factory;
            this.patient = patient;

            InitializeComponent();

            InitializeDrugs();

            startDate.DisplayDateStart = DateTime.Now;
            endDate.DisplayDateStart = DateTime.Now;
        }

        private void InitializeDrugs()
        {
            drugCb.Items.Clear();
            drugsDisplay = new Dictionary<string, Drug>();
            List<Drug> drugs = factory.DrugController.Drugs;
            List<Drug> sortedDrugs = drugs.OrderBy(x => x.Id).ToList();

            foreach (Drug drug in sortedDrugs)
            {
                drugCb.Items.Add(drug.Id + " - " + drug.Name);
                drugsDisplay.Add(drug.Id + " - " + drug.Name, drug);
            }

            drugCb.SelectedIndex = 0;
        }

        private DateTime ValidateDate(DatePicker date, string message)
        {
            try
            {
                return date.SelectedDate.Value;
            }
            catch
            {
                MessageBox.Show("You haven't picked " + message + " date!");
            }
            return default(DateTime);
        }

        private Drug ValidateDrug()
        {
            Drug drug = drugsDisplay[drugCb.SelectedItem.ToString()];

            foreach (Ingredient allergen in patient.MedicalRecord.Allergens)
                if (drug.Ingredients.Contains(allergen))
                {
                    MessageBox.Show("Patient is allergic to " + allergen.Name + " in the chosen drug!");
                    return null;
                }

            return drug;
        }

        private int ValidateFrequency()
        {
            try
            {
                int frequency = Convert.ToInt32(frequencyTb.Text);

                if (frequency == 0)
                {
                    MessageBox.Show("Frequency must be at least 1!");
                    return -1;
                }

                return frequency;
            }
            catch
            {
                MessageBox.Show("Frequency must be an integer!");
                return -1;
            }
        }

        private Prescription ValidatePrescription()
        {
            MedicalRecord medicalRecord = patient.MedicalRecord;

            Drug drug = ValidateDrug();
            if (drug is null)
                return null;

            DateTime start = ValidateDate(startDate, "start");
            if (start == default(DateTime))
                return null;

            DateTime end = ValidateDate(endDate, "end");
            if (end == default(DateTime))
                return null;

            int frequency = ValidateFrequency();
            if (frequency == -1)
                return null;

            int id = factory.PrescriptionController.GenerateId();
            return new(id, medicalRecord, start, end, frequency, drug);
        }

        private void AddAllergensBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Prescription prescription = ValidatePrescription();
                if (prescription is null) return;

                factory.AddPrescrition(prescription);
                MessageBox.Show("Drug prescribed");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrequencyTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void StartDate_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            endDate.DisplayDateStart = startDate.SelectedDate.Value.AddDays(1);
        }
    }
}
