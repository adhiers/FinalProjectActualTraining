using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using FinalProject.WebForm.Models;
using FinalProject.WebForm.Services;

namespace FinalProject.WebForm
{
    public partial class ConsultationPage : System.Web.UI.Page
    {
        private ConsultationsServices _consultationService = new ConsultationsServices();

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadConsultations();
            }
        }

        private async Task LoadConsultations()
        {
            gvConsultations.DataSource = await _consultationService.GetConsultations();
            gvConsultations.DataBind();
        }

        protected void btnShowInsertForm_Click(object sender, EventArgs e)
        {
            pnlInsertConsultation.Visible = true;
            btnInsertConsultation.Visible = true;
            btnUpdateConsultation.Visible = false;
            // Optionally clear form fields here
        }

        protected void btnCancelInsert_Click(object sender, EventArgs e)
        {
            pnlInsertConsultation.Visible = false;
        }

        protected async void btnInsertConsultation_Click(object sender, EventArgs e)
        {
            try
            {
                var insertModel = new ConsultationInsert
                {
                    ConsultId = txtConsultId.Text,
                    ScheduleId = int.TryParse(txtScheduleId.Text, out var sid) ? sid : (int?)null,
                    Spid = txtSpid.Text,
                    CustomerBudget = int.TryParse(txtCustomerBudget.Text, out var budget) ? budget : 0,
                    ConsultDate = DateTime.TryParse(txtConsultDate.Text, out var date) ? date : DateTime.Now,
                    Note = txtNote.Text
                };
                await _consultationService.AddConsultation(insertModel);
                pnlInsertConsultation.Visible = false;
                await LoadConsultations();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., show an error message)
                //lblError.Text = $"An error occurred while inserting the consultation: {ex.Message}";
                //lblError.Visible = true;
                throw new Exception($"An error occurred while inserting the consultation: {ex.Message}", ex);
            }
        }

        protected async void btnUpdateConsultation_Click(object sender, EventArgs e)
        {
            try
            {
                var consultId = btnUpdateConsultation.CommandArgument;
                var updateModel = new ConsultationUpdate
                {
                    ConsultId = txtConsultId.Text,
                    ScheduleId = int.TryParse(txtScheduleId.Text, out var sid) ? sid : (int?)null,
                    Spid = txtSpid.Text,
                    CustomerBudget = int.TryParse(txtCustomerBudget.Text, out var budget) ? budget : 0,
                    ConsultDate = DateTime.TryParse(txtConsultDate.Text, out var date) ? date : DateTime.Now,
                    Note = txtNote.Text
                };
                await _consultationService.UpdateConsultation(updateModel);
                btnInsertConsultation.Visible = true; 
                btnUpdateConsultation.Visible = false; 
                btnUpdateConsultation.CommandArgument = null;
                pnlInsertConsultation.Visible = false;
                await LoadConsultations();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the consultation: {ex.Message}", ex);
            }
        }

        protected async void btnDeleteConsultation_Click(object sender, EventArgs e)
        {
            try
            {
                var consultId = txtConsultId.Text;
                await _consultationService.DeleteConsultation(consultId);
                await LoadConsultations();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the consultation: {ex.Message}", ex);
            }
        }

        protected async void gvConsultations_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteConsultation")
            {
                string consultId = e.CommandArgument.ToString();
                await _consultationService.DeleteConsultation(consultId);
                await LoadConsultations();
            }
            else if (e.CommandName == "UpdateConsultation")
            {
                string consultId = e.CommandArgument.ToString();
                var consultation = await _consultationService.GetConsultationById(consultId);
                txtConsultId.Text = consultation.ConsultId;
                txtScheduleId.Text = consultation.ScheduleId?.ToString();
                txtSpid.Text = consultation.Spid;
                txtCustomerBudget.Text = consultation.CustomerBudget.ToString();
                txtConsultDate.Text = consultation.ConsultDate.ToString("yyyy-MM-dd");
                txtNote.Text = consultation.Note;
                pnlInsertConsultation.Visible = true;
                btnInsertConsultation.Visible = false;
                btnUpdateConsultation.Visible = true; 
                btnUpdateConsultation.CommandArgument = consultId; // Store id for update
            }
        }
    }
}