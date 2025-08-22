<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SchedulingPage.aspx.cs" Inherits="FinalProject.WebForm.SchedulePage" Async="true" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h3 id="aspnetTitle">List Of Schedules</h3>
        </section>
        <br />
        <div class="row">
            <div class="col-md-7">
                <asp:Panel ID="pnlSearch" runat="server" CssClass="mb-3">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control d-inline-block w-auto" placeholder="Search by Guest ID, Dealer ID, or Program" />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary ml-2" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnClearSearch" runat="server" Text="Clear" CssClass="btn btn-secondary ml-2" OnClick="btnClearSearch_Click" />
                </asp:Panel>
                <asp:DataGrid ID="gvSchedules" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" AllowPaging="true" PageSize="5" OnPageIndexChanged="gvSchedules_PageIndexChanged">
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
            </div>
            <div class="col-md-5">
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
        </div>
    </main>
</asp:Content>
