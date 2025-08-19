using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FinalProject.WebForm.Models;
using FinalProject.WebForm.Services;

namespace FinalProject.WebForm
{
    public partial class SchedulePage : System.Web.UI.Page
    {
        private SchedulingsServices _schedulingsServices = new SchedulingsServices();
        private GuestsServices _guestService = new GuestsServices();
        protected async void Page_Load(object sender, EventArgs e)
        {
            await FillGrid();
        }

        private async Task FillGrid()
        {
            gvSchedules.DataSource = await _schedulingsServices.GetSchedules();
            gvSchedules.DataBind();
        }

        protected async void btnDetail_Command(object sender, CommandEventArgs e)
        {
            int guestId;
            if (int.TryParse(e.CommandArgument.ToString(), out guestId))
            {
                var guest = await _guestService.GetGuestById(guestId);
                if (guest != null)
                {
                    lblGuestId.Text = guest.GuestId.ToString();
                    lblGuestName.Text = guest.GuestName;
                    lblGuestEmail.Text = guest.Email;
                    lblGuestPhone.Text = guest.PhoneNumber;
                    pnlGuestDetail.Visible = true;
                }
                else
                {
                    pnlGuestDetail.Visible = false;
                }
            }
        }

        protected async void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            var schedules = await _schedulingsServices.GetSchedulesBySearch(keyword);
            gvSchedules.DataSource = schedules;
            gvSchedules.DataBind();
        }

        protected async void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            var schedules = await _schedulingsServices.GetSchedules();
            gvSchedules.DataSource = schedules;
            gvSchedules.DataBind();
        }
    }
}