﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/socialsuite.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SocialSuitePro.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--  <div class="outer-container">
    	<div class="header-inner-container">
        	<div class="logo-container"><a href="#"><img src="img/logo-txt.png" alt="" /></a></div>
            <div class="main-nav">
            	<nav class="clearfix">
                    <ul class="clearfix">
                        <li><a href="#">Featured</a></li>
                        <li><a href="#">Pricing</a></li>
                        <li><a href="#">About</a></li>
                        <li class="login"><a href="#">login</a></li>
                    </ul>
                    <a href="#" id="pull">Menu</a>
                </nav>
            </div>
            <div class="clear"></div>
        </div> 
    </div>    --%>
    <div class="banner-container">
        <div class="banner-inner">
            <div class="ban-main-txt">
                More Than Social<br />
                Media Management
            </div>
            <div class="ban-sub-text">
                <span>SOCIOBOARD brings proactive open source social media marketing</span><br />
                <span>to boost your brand preference</span>
            </div>
            <div class="trial-butn">
                <a href="../Pricing.aspx">
                    <img src="../Contents/img/ssp/trail-butn.png" alt="" /></a><br />
                No credit card required, no software to install
            </div>
            <div class="clear">
            </div>
        </div>
        <div class="arrow">
            <ul class="mainnav">
                <li><a href="#p2"></a></li>
            </ul>
        </div>
    </div>
    <div class="anim-banner-container" style="position: relative; overflow: hidden;"
        id="p1">
        <div class="anim-banner-inner">
            <h1>
                See What Our Social Media Marketing Suite Can
                <br />
                Do for Your Business</h1>
            <p>
                People are talking about your brand. Why not join them? Socioboard allows
                <br />
                you to listen to, analyze and engage your followers across social media accounts
                from one simple platform.</p>
            <div class="ban-static">
                <img src="../Contents/img/ssp/nammer-2.png" alt="" /></div>
            <div class="ban-img">
                <div class="mid-circle">
                    <div class="lang-txt">
                        <a href="#">Multi
                            <br />
                            Language</a></div>
                    <div class="mid-white">
                        <span>SocioBoard</span></div>
                    <div class="commun-txt">
                        <a href="#">Community /
                            <br />
                            Cultural hub</a></div>
                </div>
                <div class="left-circle">
                    <span class="utop"><a href="#">You</a></span> <span class="ubottom"><a href="#">You</a></span>
                </div>
                <div class="left-pin">
                    <span><a href="#">youR BUSINESS</a></span></div>
                <div class="right-circle">
                    <span class="email"><a href="#">email</a></span> <span class="youtube"><a href="#">youtube</a></span>
                    <span class="facebook"><a href="#">facebook</a></span> <span class="mob-apps"><a
                        href="#">Mobile<br />
                        apps</a></span> <span class="twitter"><a href="#">twitter</a></span> <span class="partner">
                            <a href="#">partner<br />
                                sites</a></span>
                </div>
                <div class="right-pin">
                    <span><a href="#">All Major
                        <br />
                        SociaL<br />
                        Networks,<br />
                        News sites
                        <br />
                        &amp; bLOGS</a></span></div>
            </div>
        </div>
    </div>
    <div class="content-sec-container" id="p2">
        <div class="arrow-2">
            <ul class="mainnav">
                <li><a href="#p3"></a></li>
            </ul>
        </div>
        <div class="content-sec-inner">
            <div class="content-left">
                <h1>
                    Engage your followers effectively
                    <br />
                    across all social channels</h1>
                <p>
                    Socioboard makes it easy to keep tabs on
                    <br />
                    social conversations and provides everything you need
                    <br />
                    to efficiently engage your audience.</p>
                <ul>
                    <li>Scheduling</li>
                    <li>Canned reponses</li>
                    <li>Drafts</li>
                    <li>Approval flows</li>
                    <li>Built-in Social CRM</li>
                    <li>Workflow Automation</li>
                </ul>
                <a href="Pricing.aspx">
                    <img src="../Contents/img/ssp/trail-butn.png" alt="" /></a>
            </div>
            <div class="content-right">
                <img src="../Contents/img/ssp/phone-banner.png" alt="" />
            </div>
            <div class="clear">
            </div>
        </div>
        <div class="arrow">
            <ul class="mainnav">
                <li><a href="#p4"></a></li>
            </ul>
        </div>
    </div>
    <div class="reason-outer" id="p3">
        <div class="reason-inner">
            <h2>
                some reasons
            </h2>
            <h1>
                why people like SocioBoard</h1>
            <div class="reason-left">
                <img src="../Contents/img/ssp/img-1.jpg" alt="" />
            </div>
            <div class="reason-right">
                <ul>
                    <li>Open Source</li>
                    <li>Unified Smart Inbox to streamline engagement</li>
                    <li>Social CRM tools including shared customer records</li>
                    <li>Advanced scheduling & publishing tools including ViralPost™</li>
                    <li>Sophisticated analytics & unlimited custom reports</li>
                    <li>Customer support features like tasks and Helpdesk integration</li>
                    <li>Team collaboration tools including live activity updates</li>
                </ul>
            </div>
            <div class="clear">
            </div>
            <div class="ban-large-txt">
                With Socioboard, engaging your followers is so eazy, fun and insightful now. Get started today!</div>
        </div>
    </div>
    <div class="anim-banner-container" id="p4">
        <div class="signup-inner">
            Try Socioboard Premium risk-free for 30 days! <span><a href="Pricing.aspx">
                <img src="../Contents/img/ssp/signup-butn.png" alt="" align="absmiddle" /></a></span>
        </div>
        <div class="arrow">
            <ul class="mainnav">
                <li><a href="#p5"></a></li>
            </ul>
        </div>
    </div>
</asp:Content>
