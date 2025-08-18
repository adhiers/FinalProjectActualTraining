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
        }

        protected void btnCancelInsert_Click(object sender, EventArgs e)
        {
            pnlInsertConsultation.Visible = false;
        }

        protected async void btnInsertConsultation_Click(object sender, EventArgs e)
        {
            var insertModel = new ConsultationInsert
            {
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
                // For simplicity, you can show the insert panel pre-filled for update, or implement a separate update panel
                string consultId = e.CommandArgument.ToString();
                var consultation = await _consultationService.GetConsultationById(consultId);
                txtScheduleId.Text = consultation.ScheduleId?.ToString();
                txtSpid.Text = consultation.Spid;
                txtCustomerBudget.Text = consultation.CustomerBudget.ToString();
                txtConsultDate.Text = consultation.ConsultDate.ToString("yyyy-MM-dd");
                txtNote.Text = consultation.Note;
                pnlInsertConsultation.Visible = true;
                btnInsertConsultation.Text = "Update";
                btnInsertConsultation.CommandArgument = consultId;
            }
        }

        // Optionally, handle update logic if btnInsertConsultation is in update mode
        protected async void btnInsertConsultation_Command(object sender, CommandEventArgs e)
        {
            if (btnInsertConsultation.Text == "Update")
            {
                var updateModel = new ConsultationUpdate
                {
                    ConsultId = btnInsertConsultation.CommandArgument,
                    ScheduleId = int.TryParse(txtScheduleId.Text, out var sid) ? sid : (int?)null,
                    Spid = txtSpid.Text,
                    CustomerBudget = int.TryParse(txtCustomerBudget.Text, out var budget) ? budget : 0,
                    ConsultDate = DateTime.TryParse(txtConsultDate.Text, out var date) ? date : DateTime.Now,
                    Note = txtNote.Text
                };
                await _consultationService.UpdateConsultation(updateModel);
                btnInsertConsultation.Text = "Insert";
                btnInsertConsultation.CommandArgument = null;
                pnlInsertConsultation.Visible = false;
                await LoadConsultations();
            }
        }
    }
}