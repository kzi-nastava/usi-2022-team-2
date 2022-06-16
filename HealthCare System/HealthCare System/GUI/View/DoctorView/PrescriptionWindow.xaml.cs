using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HealthCare_System.Core.Drugs;
using HealthCare_System.Core.Drugs.Model;
using HealthCare_System.Core.Ingredients.Model;
using HealthCare_System.Core.MedicalRecords.Model;
using HealthCare_System.Core.Prescriptions;
using HealthCare_System.Core.Prescriptions.Model;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.GUI.Controller.Drugs;
using HealthCare_System.GUI.Controller.Prescriptions;

namespace HealthCare_System.GUI.DoctorView
{
    public partial class PrescriptionWindow : Window
    {
        Patient patient;
        Dictionary<string, Drug> drugsDisplay;
        PrescriptionController prescriptionController;
        DrugController drugController;

        public PrescriptionWindow(Patient patient, IPrescriptionService prescriptionService, IDrugService drugService)
        {
            this.patient = patient;

            InitializeComponent();

            prescriptionController = new(prescriptionService);
            drugController = new(drugService);

            InitializeDrugs();

            startDate.DisplayDateStart = DateTime.Now;
            endDate.DisplayDateStart = DateTime.Now;
        }

        private void InitializeDrugs()
        {
            drugCb.Items.Clear();
            drugsDisplay = new Dictionary<string, Drug>();
            List<Drug> drugs = drugController.FillterAccepted();
            List<Drug> sortedDrugs = drugs.OrderBy(x => x.Id).ToList();

            foreach (Drug drug in sortedDrugs)
            {
                drugCb.Items.Add(drug.Id + " - " + drug.Name);
                drugsDisplay.Add(drug.Id + " - " + drug.Name, drug);
            }

            drugCb.SelectedIndex = 0;
        }

        public static DateTime ValidateDate(DatePicker date, string message)
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

        private PrescriptionDto ValidatePrescription()
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

            int frequency = DoctorWindow.ValidateTextBox(frequencyTb, "Frequency");
            if (frequency == -1)
                return null;

            int id = prescriptionController.GenerateId();
            return new(id, medicalRecord, start, end, frequency, drug);
        }

        private void AddDrugBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrescriptionDto prescriptionDto = ValidatePrescription();
                if (prescriptionDto is null) return;

                prescriptionController.AddPrescrition(prescriptionDto);
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
            endDate.SelectedDate = startDate.SelectedDate.Value.AddDays(1);
        }
    }
}
