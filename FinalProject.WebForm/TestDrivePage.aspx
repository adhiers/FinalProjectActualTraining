<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestDrivePage.aspx.cs" Inherits="FinalProject.WebForm.TestDrivePage" Async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h3>Test Drive Table</h3>
        <asp:Button ID="btnShowInsertForm" runat="server" Text="Add Test Drive" CssClass="btn btn-success mb-3" OnClick="btnShowInsertForm_Click" />
        <asp:Panel ID="pnlInsertTestDrive" runat="server" Visible="false" CssClass="mb-4">
            <h4>Insert Test Drive</h4>
            <asp:TextBox ID="txtTdid" runat="server" placeholder="Test Drive ID" CssClass="form-control mb-2" />
            <asp:TextBox ID="txtConsultId" runat="server" placeholder="Consultation ID" CssClass="form-control mb-2" />
            <asp:TextBox ID="txtScheduleId" runat="server" placeholder="Schedule ID" CssClass="form-control mb-2" />
            <asp:TextBox ID="txtDealerCarId" runat="server" placeholder="Dealer Car ID" CssClass="form-control mb-2" />
            <asp:TextBox ID="txtSpid" runat="server" placeholder="Salesperson ID" CssClass="form-control mb-2" />
            <asp:TextBox ID="txtTddate" runat="server" placeholder="Test Drive Date (yyyy-MM-dd)" CssClass="form-control mb-2" />
            <asp:TextBox ID="txtNote" runat="server" placeholder="Note" CssClass="form-control mb-2" TextMode="MultiLine" Rows="3" />
            <asp:Button ID="btnInsertTestDrive" runat="server" Text="Insert" CssClass="btn btn-primary" OnClick="btnInsertTestDrive_Click" />
            <asp:Button ID="btnUpdateTestDrive" runat="server" Text="Update" CssClass="btn btn-warning" OnClick="btnUpdateTestDrive_Click" Visible="false" />
            <asp:Button ID="btnCancelInsert" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancelInsert_Click" />
        </asp:Panel>
        <asp:GridView ID="gvTestDrives" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" OnRowCommand="gvTestDrives_RowCommand">
            <Columns>
                <asp:BoundField DataField="Tdid" HeaderText="Test Drive ID" />
                <asp:BoundField DataField="ConsultId" HeaderText="Consultation ID" />
                <asp:BoundField DataField="ScheduleId" HeaderText="Schedule ID" />
                <asp:BoundField DataField="DealerCarId" HeaderText="Dealer Car ID" />
                <asp:BoundField DataField="Spid" HeaderText="Salesperson ID" />
                <asp:BoundField DataField="Tddate" HeaderText="Test Drive Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="Note" HeaderText="Note" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="UpdateTestDrive" CommandArgument='<%# Eval("Tdid") %>' CssClass="btn btn-warning btn-sm mr-2" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteTestDrive" CommandArgument='<%# Eval("Tdid") %>' CssClass="btn btn-danger btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </main>
</asp:Content>
