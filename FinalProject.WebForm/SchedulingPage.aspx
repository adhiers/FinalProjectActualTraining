<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SchedulingPage.aspx.cs" Inherits="FinalProject.WebForm.SchedulePage" Async="true" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h3 id="aspnetTitle">List Of Schedules</h3>
        </section>
        <br />
        <div class="col-md-6">
            <asp:DataGrid ID="gvSchedules" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundColumn DataField="ScheduleId" HeaderText="Schedule ID" />
                    <asp:BoundColumn DataField="GuestId" HeaderText="Guest ID" />
                    <asp:BoundColumn DataField="DealerId" HeaderText="Dealer ID" />
                    <asp:BoundColumn DataField="Program" HeaderText="Program"/>
                    <asp:BoundColumn DataField="AvailableStart" HeaderText="Available From"/>
                    <asp:BoundColumn DataField="AvailableEnd" HeaderText="To"/>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:LinkButton 
                                ID="btnDetail" 
                                runat="server" 
                                CssClass="btn btn-primary btn-sm"
                                CommandName="ShowDetail"
                                CommandArgument='<%# Eval("GuestId") %>'
                                OnCommand="btnDetail_Command">
                                Detail
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle CssClass="pagination justify-content-center" />
            </asp:DataGrid>
            <asp:Panel ID="pnlGuestDetail" runat="server" Visible="false" CssClass="mt-4">
                <h4>Guest Information</h4>
                <div>
                    <b>ID:</b> <asp:Label ID="lblGuestId" runat="server" /><br />
                    <b>Name:</b> <asp:Label ID="lblGuestName" runat="server" /><br />
                    <b>Email:</b> <asp:Label ID="lblGuestEmail" runat="server" /><br />
                    <b>Phone:</b> <asp:Label ID="lblGuestPhone" runat="server" /><br />
                </div>
        </asp:Panel>
        </div>
    </main>
</asp:Content>
