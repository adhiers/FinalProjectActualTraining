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
    public partial class GuestPage : System.Web.UI.Page
    {
        private GuestsServices _guestService = new GuestsServices();
        protected async void Page_Load(object sender, EventArgs e)
        {
            await FillGrid();
        }

        private async Task FillGrid()
        {
            gvGuests.DataSource = await _guestService.GetGuests();
            gvGuests.DataBind();
        }
    }
}