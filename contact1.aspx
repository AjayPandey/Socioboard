<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/socialsuite.Master" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="SocialSuitePro.contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <hr />
 
 <div>
   <iframe width="100%" height="350" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="https://maps.google.co.in/maps?f=q&amp;source=s_q&amp;hl=en&amp;geocode=&amp;q=Globussoft,+Smriti+Nagar,+Bhilai+Nagar,+Chhattisgarh&amp;aq=0&amp;oq=globussoft&amp;sll=21.205521,81.487619&amp;sspn=0.360404,0.676346&amp;ie=UTF8&amp;hq=Globussoft,&amp;hnear=Smriti+Nagar,+Bhilai,+Durg,+Chhattisgarh&amp;t=m&amp;ll=21.219529,81.318624&amp;spn=0.006295,0.019671&amp;output=embed"></iframe><br /><small><a href="https://maps.google.co.in/maps?f=q&amp;source=embed&amp;hl=en&amp;geocode=&amp;q=Globussoft,+Smriti+Nagar,+Bhilai+Nagar,+Chhattisgarh&amp;aq=0&amp;oq=globussoft&amp;sll=21.205521,81.487619&amp;sspn=0.360404,0.676346&amp;ie=UTF8&amp;hq=Globussoft,&amp;hnear=Smriti+Nagar,+Bhilai,+Durg,+Chhattisgarh&amp;t=m&amp;ll=21.219529,81.318624&amp;spn=0.006295,0.019671" style="color:#0000FF;text-align:left">View Larger Map</a></small>
 
 </div>
 
	<div class="contact_form">
    <h2 style="text-align:left;">Contact</h2>
    <div id="signup-inner" style="width:700px; height:auto; float:left;">
          
            <form id="send" action="">
            	
                <p>
                <label for="name">First Name *</label>
                <input id="name" type="text" name="name" value="" />
                </p>
                
                <p>
                <label for="company">Last Name</label>
                <input id="company" type="text" name="company" value="" />
                </p>
                
                <p>

                <label for="email">Email *</label>
                <input id="email" type="text" name="email" value="" />
                </p>
                
                <p>
                <label for="website">Your Subject</label>
                <input id="Subject" type="text" name="Subject" value="" />
                </p>
                             
                <p>
                <label for="profile">Message *</label>
                <textarea name="profile" id="profile" cols="30" rows="10"></textarea>

                </p>
                
                <p>

                <button id="submit" type="submit">Submit</button>
                </p>
                
            </form>
            
          </div>
    <div style="width:300px; float:right; height:auto;">
      <h2 style=" text-align:left;">Our Offices</h2>
      <div style="width:300px; height:100px; border-top:1px solid #999; color:#787878; font-size:12px; font-family:nexa-light; padding-top:9px; font-size:14px;">
         CHPL DreamHomes Apartments, Apollo Hospital Road Junwani, Bhilai-490020. Chhattisgarh. <br />
         <img src="contents/img/support.png" alt="" /> <br />
         0788-4083007, 4083008, 4083009
      </div> 
        
    </div>      
    </div>

</asp:Content>
