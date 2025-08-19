using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using FinalProject.WebForm.Models;
using FinalProject.WebForm.Services;

namespace FinalProject.WebForm
{
    public partial class TestDrivePage : System.Web.UI.Page
    {
        private TestDrivesServices _testDriveService = new TestDrivesServices();

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadTestDrives();
            }
        }

        private async Task LoadTestDrives()
        {
            gvTestDrives.DataSource = await _testDriveService.GetTestDrives();
            gvTestDrives.DataBind();
        }

        protected void btnShowInsertForm_Click(object sender, EventArgs e)
        {
            pnlInsertTestDrive.Visible = true;
            btnInsertTestDrive.Visible = true;
            btnUpdateTestDrive.Visible = false;
            // Optionally clear form fields here
        }

        protected void btnCancelInsert_Click(object sender, EventArgs e)
        {
            pnlInsertTestDrive.Visible = false;
        }

        protected async void btnInsertTestDrive_Click(object sender, EventArgs e)
        {
            try
            {
                var insertModel = new TestDriveInsert
                {
                    Tdid = txtTdid.Text,
                    ConsultId = string.IsNullOrWhiteSpace(txtConsultId.Text) ? null : txtConsultId.Text,
                    ScheduleId = int.TryParse(txtScheduleId.Text, out var sid) ? sid : 0, // Use 0 or another default value
                    DealerCarId = txtDealerCarId.Text,
                    Spid = txtSpid.Text,
                    Tddate = DateTime.TryParse(txtTddate.Text, out var date) ? date : DateTime.Now,
                    Note = txtNote.Text
                };
                await _testDriveService.AddTestDrive(insertModel);
                pnlInsertTestDrive.Visible = false;
                await LoadTestDrives();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while inserting the test drive: {ex.Message}", ex);
            }
        }

        protected async void btnUpdateTestDrive_Click(object sender, EventArgs e)
        {
            try
            {
                var tdid = btnUpdateTestDrive.CommandArgument;
                var updateModel = new TestDriveUpdate
                {
                    Tdid = txtTdid.Text,
                    ConsultId = string.IsNullOrWhiteSpace(txtConsultId.Text) ? null : txtConsultId.Text,
                    ScheduleId = int.TryParse(txtScheduleId.Text, out var sid) ? sid : (int?)null,
                    DealerCarId = txtDealerCarId.Text,
                    Spid = txtSpid.Text,
                    Tddate = DateTime.TryParse(txtTddate.Text, out var date) ? date : DateTime.Now,
                    Note = txtNote.Text
                };
                await _testDriveService.UpdateTestDrive(updateModel);
                btnInsertTestDrive.Visible = true;
                btnUpdateTestDrive.Visible = false;
                btnUpdateTestDrive.CommandArgument = null;
                pnlInsertTestDrive.Visible = false;
                await LoadTestDrives();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the test drive: {ex.Message}", ex);
            }
        }

        protected async void gvTestDrives_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteTestDrive")
            {
                string tdid = e.CommandArgument.ToString();
                await _testDriveService.DeleteTestDrive(tdid);
                await LoadTestDrives();
            }
            else if (e.CommandName == "UpdateTestDrive")
            {
                string tdid = e.CommandArgument.ToString();
                var testDrive = await _testDriveService.GetTestDriveById(tdid);
                txtTdid.Text = testDrive.Tdid;
                txtConsultId.Text = testDrive.ConsultId;
                txtScheduleId.Text = testDrive.ScheduleId.ToString();
                txtDealerCarId.Text = testDrive.DealerCarId;
                txtSpid.Text = testDrive.Spid;
                txtTddate.Text = testDrive.Tddate.ToString("yyyy-MM-dd");
                txtNote.Text = testDrive.Note;
                pnlInsertTestDrive.Visible = true;
                btnInsertTestDrive.Visible = false;
                btnUpdateTestDrive.Visible = true;
                btnUpdateTestDrive.CommandArgument = tdid;
            }
        }
    }
}