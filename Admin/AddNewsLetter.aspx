﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/adminSite.Master" AutoEventWireup="true" CodeBehind="AddNewsLetter.aspx.cs" Inherits="SocialSuitePro.Admin.AddNewsLetter" %>
<%@ Register TagPrefix="cc" Namespace="Winthusiasm.HtmlEditor" Assembly="Winthusiasm.HtmlEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script src="../Contents/js/jquery.min.js" type="text/javascript"></script>
 <script type="text/javascript" language="javascript">
     $(function () {
         $("#<%= txtSendDate.ClientID %>").datepicker();
     });
</script>
<div id="content">
	       <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
    <div class="innerLR" style="margin-top: 10px; margin-left:-15px;">
	    <!-- Widget -->
	    <div class="widget widget-tabs border-bottom-none">	
		    <!-- Widget heading -->
		    <div class="widget-head">
			    <ul>
				    <li class="active"><a href="#account-details">User Detail</a></li>
				    <%--<li class=""><a data-toggle="tab" href="#account-settings" class="glyphicons settings"><i></i>Account settings</a></li>--%>
			    </ul>
         
		    </div>
		    <!-- // Widget heading END -->
		
		    <div class="widget-body">
			    <form style="margin: 0;" class="form-horizontal">
				    <div style="padding: 0;" class="tab-content">
                    <div class="tab-pane active" id="account-details">
					
						<!-- Row -->
						<div class="row-fluid">
						
							<!-- Column -->
							<div class="span6">
								<!-- Group -->
								<div class="control-group">
									<label class="control-label">Subject</label>
									<div class="controls">
                                          <asp:TextBox ID="txtSubject" runat="server" Visible="true"></asp:TextBox>                                  
										<span style="margin: 0;" class="btn-action single glyphicons circle_question_mark" data-toggle="tooltip" data-placement="top" data-original-title="First name is mandatory"><i></i></span>
									</div>
								</div>
								<!-- // Group END -->
								<!-- Group -->
								<div class="control-group">
									<label class="control-label">News</label>
									<div class="controls">
                                         <cc:HtmlEditor ID="Editor" runat="server" Height="400px" Width="600px" />                                       
										<span style="margin: 0;" class="btn-action single glyphicons circle_question_mark" data-toggle="tooltip" data-placement="top" data-original-title="First name is mandatory"><i></i></span>
									</div>
								</div>
								<!-- // Group END -->
                                	<!-- Group -->
								<div class="control-group">
									<label class="control-label">Send Date</label>
									<div class="controls">
										<div class="input-append">
                                      <%--  <input type="text" value="13/06/1988" id="datepicker" >--%>
                                            <asp:TextBox ID="txtSendDate" runat="server" Visible="true"></asp:TextBox>
											   
											<span class="add-on glyphicons calendar"><i></i></span>
										</div>
									</div>
								</div>
								<!-- // Group END -->
                                
						</div>
						<!-- // Row END -->
						<!-- Group -->
								<div class="control-group">
									<label class="control-label">Status</label>
									<div class="controls scroll" style="width: 100%; overflow-x: hidden; overflow-y: auto; height: 355px; ">
										
                                            <asp:CheckBoxList ID="chkUser" runat="server">
                                            </asp:CheckBoxList>
                                        
									</div>
								</div>
								<!-- // Group END -->
						<div class="separator line bottom"></div>
						
						<!-- Group -->
						<div class="control-group row-fluid">
							<label class="control-label"></label>
							<div class="controls">
								
							</div>
						</div>
						<!-- // Group END -->
						
						<!-- Form actions -->
						<div class="form-actions" style="margin: 0;">
                            <asp:Button ID="btnSave" runat="server" Text="Save Changes" 
                                class="btn btn-icon btn-primary circle_ok" onclick="btnSave_Click"/>
                            <%--	<button type="submit" class="btn btn-icon btn-primary glyphicons circle_ok"><i></i>Save changes</button>
							<button type="button" class="btn btn-icon btn-default glyphicons circle_remove"><i></i>Cancel</button>--%>
						</div>
						<!-- // Form actions END -->
						
					</div>
                    </div>
			    </form>
		    </div>
	    </div>
	    <!-- // Widget END -->
	
    </div>		
</div>

</asp:Content>
