﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Model;
using System.Collections;
using SocioBoard.Helper;
using Facebook;
using GlobusInstagramLib.App.Core;
using System.Configuration;
using GlobusInstagramLib.Authentication;
using GlobusTwitterLib.Authentication;
using System.Text.RegularExpressions;
using GlobusLinkedinLib.App.Core;
using GlobusLinkedinLib.Authentication;
using log4net;
using Newtonsoft.Json.Linq;

namespace SocialSuitePro.Feeds
{
    public partial class AjaxFeeds : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(AjaxFeeds));

        public static int instagramcount = 0;
        public static int facebookwallcount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {


            SocioBoard.Domain.User use = (SocioBoard.Domain.User)Session["LoggedUser"];
            try
            {
                if (use == null)
                    Response.Redirect("/Default.aspx");

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            try
            {
                ProcessRequest();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

        }

        public void ProcessRequest()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["op"]))
            {
                SocioBoard.Domain.User user = (SocioBoard.Domain.User)Session["LoggedUser"];
                if (Request.QueryString["op"] == "networkprofiles")
                {
                    #region NetworkProfiles
                    string profiles = string.Empty;
                    if (Request.QueryString["network"] == "facebook")
                    {
                        ArrayList alstfacebook = null;
                        if (Session["facebooktotalprofiles"] == null)
                        {
                            FacebookAccountRepository faceaccrepo = new FacebookAccountRepository();
                            alstfacebook = faceaccrepo.getFacebookAccountsOfUser(user.Id);
                            Session["facebooktotalprofiles"] = alstfacebook;
                        }
                        else
                        {
                            alstfacebook = (ArrayList)Session["facebooktotalprofiles"];
                        }


                        if (alstfacebook.Count == 0)
                        {
                            profiles += "<li><a  href=\"#\" class=\"active\">No Records Found</a> </li>";
                        }
                        else
                        {
                            foreach (FacebookAccount item in alstfacebook)
                            {
                                profiles += "<li><a id=\"lifb_" + item.FbUserId + "\" href=\"#\" onclick=\"facebookdetails('" + item.FbUserId + "');\" class=\"active\">" + item.FbUserName + "</a> </li>";
                            }
                        }

                    }
                    else if (Request.QueryString["network"] == "twitter")
                    {
                        ArrayList alsttwitter = null;

                        if (Session["twittertotalprofiles"] == null)
                        {
                            TwitterAccountRepository twtaccrepo = new TwitterAccountRepository();
                            alsttwitter = twtaccrepo.getAllTwitterAccountsOfUser(user.Id);
                            Session["twittertotalprofiles"] = alsttwitter;
                        }
                        else
                        {
                            alsttwitter = (ArrayList)Session["twittertotalprofiles"];
                        }

                        if (alsttwitter.Count == 0)
                        {
                            profiles += "<li><a  href=\"#\" class=\"active\">No Records Found</a> </li>";
                        }
                        else
                        {

                            foreach (TwitterAccount item in alsttwitter)
                            {
                                profiles += "<li><a id=\"litwt_" + item.TwitterUserId + "\" href=\"#\" onclick=\"twitterdetails('" + item.TwitterUserId + "');\" class=\"active\">" + item.TwitterScreenName + "</a> </li>";
                            }
                        }
                    }
                    else if (Request.QueryString["network"] == "linkedin")
                    {
                        ArrayList alstlinklist = null;
                        if (Session["linkedintotalprofiles"] == null)
                        {
                            LinkedInAccountRepository linkaccrepo = new LinkedInAccountRepository();
                            alstlinklist = linkaccrepo.getAllLinkedinAccountsOfUser(user.Id);
                        }
                        else
                        {
                            alstlinklist = (ArrayList)Session["linkedintotalprofiles"];
                        }
                        if (alstlinklist.Count == 0)
                        {
                            profiles += "<li><a  href=\"#\" class=\"active\">No Records Found</a> </li>";
                        }
                        else
                        {
                            foreach (LinkedInAccount item in alstlinklist)
                            {
                                profiles += "<li><a id=\"lilin_" + item.LinkedinUserId + "\" href=\"#\" onclick=\"linkedindetails('" + item.LinkedinUserId + "');\" class=\"active\">" + item.LinkedinUserName + "</a> </li>";
                            }
                        }
                    }
                    else if (Request.QueryString["network"] == "instagram")
                    {
                        ArrayList alstinstagram = null;
                        if (Session["instagramtotalprofiles"] == null)
                        {
                            InstagramAccountRepository insaccrepo = new InstagramAccountRepository();
                            alstinstagram = insaccrepo.getAllInstagramAccountsOfUser(user.Id);
                            Session["instagramtotalprofiles"] = alstinstagram;
                        }
                        else
                        {
                            alstinstagram = (ArrayList)Session["instagramtotalprofiles"];
                        }
                        if (alstinstagram.Count == 0)
                        {
                            profiles += "<li><a  href=\"#\" class=\"active\">No Records Found</a> </li>";
                        }
                        else
                        {
                            foreach (InstagramAccount item in alstinstagram)
                            {
                                profiles += "<li><a id=\"liins_" + item.InstagramId + "\" href=\"#\" onclick=\"Instagramdetails('" + item.InstagramId + "');\" class=\"active\">" + item.InsUserName + "</a> </li>";
                            }
                        }

                    }
                    Response.Write(profiles);
                    #endregion
                }
                else if (Request.QueryString["op"] == "facebookwallposts")
                {
                    string messages = string.Empty;
                    string profileid = string.Empty;
                    string load = Request.QueryString["load"];
                    //Session[""] = profileid;
                    if (load == "first")
                    {
                        profileid = Request.QueryString["profileid"];
                        Session["FacebookProfileIdForFeeds"] = profileid;
                        facebookwallcount = 0;
                    }
                    else
                    {
                        profileid = (string)Session["FacebookProfileIdForFeeds"];
                        facebookwallcount = facebookwallcount + 10;
                    }


                    FacebookMessageRepository fbmsgrepo = new FacebookMessageRepository();
                    FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
                 
                    List<FacebookMessage> lsgfbmsgs = fbmsgrepo.getAllWallpostsOfProfile(profileid, facebookwallcount);

                    UrlExtractor urlext = new UrlExtractor();
                    foreach (FacebookMessage item in lsgfbmsgs)
                    {
                        try
                        {

                            string[] str = urlext.splitUrlFromString(item.Message);
                            messages += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"" + item.FromProfileUrl + "\" onclick=\"getFacebookProfiles('" + item.FromId + "');\">" +
                                                         "</div><div class=\"pull-left feedcontent\">" +
                                                            "<a href=\"#\" class=\"feednm\" onclick=\"getFacebookProfiles('" + item.FromId + "');\">" + item.FromName + "</a> <span>" + item.MessageDate +
                                                                " </span>" +
                                                             "<p>";

                            if (!string.IsNullOrEmpty(item.Picture))
                            {
                                //string largeimage = item.Picture.Replace("_s.jpg","_n.jpg");

                                messages += "<img src=\"" + item.Picture + "\" alt=\"\" onclick=\"fbimage('" + item.Picture + "');\" /><br/>";
                            }

                            foreach (string substritem in str)
                            {
                                if (!string.IsNullOrEmpty(substritem))
                                {
                                    if (substritem.Contains("http"))
                                    {
                                        messages += "<a target=\"_blank\" href=\"" + substritem + "\">" + substritem + "</a>";
                                    }
                                    else
                                    {
                                        messages += substritem;
                                    }
                                }
                            }

                            messages += "</p>" +
                                        "<a class=\"retweets\" href=\"#\">" +
                                        "</a><p><span onclick=\"facebookLike('" + item.FbLike + "','" + profileid + "','" + item.MessageId + "')\" id=\"likefb_" + item.MessageId + "\" class=\"like\">Like</span><span id=\"commentfb_" + item.MessageId + "\" onclick=\"commentText('"+item.MessageId+"');\" class=\"comment\">Comment</span></p>" +
                                        "<p class=\"commeent_box\"><input id=\"textfb_"+item.MessageId+"\" type=\"text\" class=\"put_comments\"></p>"+
                                      "<p><span onclick=\"commentFB('"+item.MessageId+"','"+profileid+"')\" id=\"okfb_"+item.MessageId+"\" class=\"ok\">ok</span><span id=\"cancelfb_"+item.MessageId+"\" onclick=\"cancelFB('"+item.MessageId+"');\" class=\"cancel\"> cancel</span></p>"+
                                        "</div>" +
                                        "</li>";
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }
                    }
                    Response.Write(messages);
                }
                else if (Request.QueryString["op"] == "fblike")
                {
                    try
                    {
                        //System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                        //string line = "";
                        //line = sr.ReadToEnd();
                        //JObject jo = JObject.Parse(line);
                        //string accesstoken = Server.UrlDecode((string)jo["access"]);
                        //string id = Server.UrlDecode((string)jo["fbid"]);
                        string profileid = Request.QueryString["profileid"];
                        FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
                        FacebookAccount fbAccount = fbAccRepo.getFacebookAccountDetailsById(profileid, user.Id);
                        string id = Request.QueryString["fbid"];
                        FacebookClient fbClient = new FacebookClient(fbAccount.AccessToken);
                        var s = fbClient.Post(id + "/likes",null);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                }
                else if (Request.QueryString["op"] == "fbcomment")
                {
                    string profileid = Request.QueryString["profileid"];
                    string message = Request.QueryString["message"];
                    FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
                    FacebookAccount fbAccount = fbAccRepo.getFacebookAccountDetailsById(profileid, user.Id);
                    string id = Request.QueryString["fbid"];
                    FacebookClient fbClient = new FacebookClient(fbAccount.AccessToken);
                    var args = new Dictionary<string, object>();
                    args["message"] = message;
                    var s = fbClient.Post(id+"/comments",args);
                
                }
                else if (Request.QueryString["op"] == "twitternetworkdetails")
                {
                    string messages = string.Empty;
                    string profileid = Request.QueryString["profileid"];
                    TwitterFeedRepository fbmsgrepo = new TwitterFeedRepository();
                    List<TwitterFeed> lsgfbmsgs = fbmsgrepo.getTwitterFeedOfProfile(profileid);
                    UrlExtractor urlext = new UrlExtractor();
                    foreach (TwitterFeed item in lsgfbmsgs)
                    {
                        try
                        {
                            messages += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"" + item.FromProfileUrl + "\" onclick=\"detailsprofile('" + item.FromId + "');\">" +
                                                         "</div><div class=\"pull-left feedcontent\">" +
                                                            "<a href=\"#\" class=\"feednm\" onclick=\"detailsprofile('" + item.FromId + "');\">" + item.FromName + "</a> <span>" + item.FeedDate +
                                                                " </span>" +
                                                             "<p>";

                            string[] str = urlext.splitUrlFromString(item.Feed);

                            foreach (string substritem in str)
                            {
                                if (!string.IsNullOrEmpty(substritem))
                                {
                                    if (substritem.Contains("http"))
                                    {
                                        messages += "<a target=\"_blank\" href=\"" + substritem + "\">" + substritem + "</a>";
                                    }
                                    else
                                    {
                                        messages += substritem;
                                    }
                                }
                            }

                            messages += "</p>" +
                                   "<a class=\"retweets\" href=\"#\">" +
                                /*"<img alt=\"\" src=\"../contents/img/admin/arrow.png\">*/"</a><span></span>" +
                               "</div>" +
                           "</li>";
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }
                    }
                    Response.Write(messages);

                }
                else if (Request.QueryString["op"] == "scheduler")
                {
                    #region Schduler
                    string message = string.Empty;
                    string profileid = Request.QueryString["profileid"];
                    string network = Request.QueryString["network"];

                    if (network == "facebook")
                    {
                        ScheduledMessageRepository schmsgrepo = new ScheduledMessageRepository();
                        List<ScheduledMessage> lstschmsg = schmsgrepo.getAllMessagesOfUser(user.Id, profileid);

                        if (lstschmsg.Count != 0)
                        {
                            foreach (ScheduledMessage item in lstschmsg)
                            {
                                FacebookAccountRepository faceaccrepo = new FacebookAccountRepository();
                                FacebookAccount faceacc = faceaccrepo.getFacebookAccountDetailsById(profileid, user.Id);
                                try
                                {
                                    message += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"../Contents/img/blank_img.png\">" +
                                                                 "</div><div class=\"pull-left feedcontent\">" +
                                                                    "<a href=\"#\" class=\"feednm\">" + faceacc.FbUserName + "</a> <span>" + item.ScheduleTime +
                                                                        " </span>" +
                                                                     "<p>" + item.ShareMessage + "</p>" +
                                                                     "<a class=\"retweets\" href=\"#\">" +
                                                                      "</a><span></span>" +
                                                                 "</div>" +
                                                             "</li>";
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }

                            }
                        }
                        else
                        {
                            message = "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"../Contents/img/blank_img.png\">" +
                                                                    "</div><div class=\"pull-left feedcontent\">" +
                                                                       "<a href=\"#\" class=\"feednm\"></a> <span>" +
                                                                           " </span>" +
                                                                        "<p>No Scheduled Messages</p>" +
                                                                        "<a class=\"retweets\" href=\"#\">" +
                                                                         "</a><span></span>" +
                                                                    "</div>" +
                                                                "</li>";
                        }



                    }
                    else if (network == "twitter")
                    {
                        ScheduledMessageRepository schmsgrepo = new ScheduledMessageRepository();
                        List<ScheduledMessage> lstschmsg = schmsgrepo.getAllMessagesOfUser(user.Id, profileid);

                        if (lstschmsg.Count != 0)
                        {
                            foreach (ScheduledMessage item in lstschmsg)
                            {
                                TwitterAccountRepository twtaccrepo = new TwitterAccountRepository();
                                TwitterAccount twtacc = twtaccrepo.getUserInformation(user.Id, profileid);
                                message += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"../Contents/img/blank_img.png\">" +
                                                                "</div><div class=\"pull-left feedcontent\">" +
                                                                   "<a href=\"#\" class=\"feednm\">" + twtacc.TwitterScreenName + "</a> <span>" + item.ScheduleTime +
                                                                       " </span>" +
                                                                    "<p>" + item.ShareMessage + "</p>" +
                                                                    "<a class=\"retweets\" href=\"#\">" +
                                                                     "</a><span></span>" +
                                                                "</div>" +
                                                            "</li>";
                            }
                        }
                        else
                        {
                            message = "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"../Contents/img/blank_img.png\">" +
                                                                  "</div><div class=\"pull-left feedcontent\">" +
                                                                     "<a href=\"#\" class=\"feednm\"></a> <span>" +
                                                                         " </span>" +
                                                                      "<p>No Scheduled Messages</p>" +
                                                                      "<a class=\"retweets\" href=\"#\">" +
                                                                       "</a><span></span>" +
                                                                  "</div>" +
                                                              "</li>";
                        }
                    }
                    else if (network == "linkedin")
                    {
                        ScheduledMessageRepository schmsgrepo = new ScheduledMessageRepository();
                        List<ScheduledMessage> lstschmsg = schmsgrepo.getAllMessagesOfUser(user.Id, profileid);

                        if (lstschmsg.Count != 0)
                        {
                            foreach (ScheduledMessage item in lstschmsg)
                            {
                                LinkedInAccountRepository linkedinrepo = new LinkedInAccountRepository();
                                LinkedInAccount linkedacc = linkedinrepo.getUserInformation(user.Id, profileid);
                                message += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"../Contents/img/blank_img.png\">" +
                                                                    "</div><div class=\"pull-left feedcontent\">" +
                                                                       "<a href=\"#\" class=\"feednm\">" + linkedacc.LinkedinUserName + "</a> <span>" + item.ScheduleTime +
                                                                           " </span>" +
                                                                        "<p>" + item.ShareMessage + "</p>" +
                                                                        "<a class=\"retweets\" href=\"#\">" +
                                                                         "</a><span></span>" +
                                                                    "</div>" +
                                                                "</li>";
                            }
                        }
                        else
                        {
                            message = "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"../Contents/img/blank_img.png\">" +
                                                                 "</div><div class=\"pull-left feedcontent\">" +
                                                                    "<a href=\"#\" class=\"feednm\"></a> <span>" +
                                                                        " </span>" +
                                                                     "<p>No Scheduled Messages</p>" +
                                                                     "<a class=\"retweets\" href=\"#\">" +
                                                                      "</a><span></span>" +
                                                                 "</div>" +
                                                             "</li>";

                        }

                    }




                    Response.Write(message);
                    #endregion
                }
                else if (Request.QueryString["op"] == "facebookfeeds")
                {
                    string message = string.Empty;
                    string profileid = Request.QueryString["profileid"];
                    FacebookAccountRepository faceaccrepo = new FacebookAccountRepository();
                    FacebookAccount faceaac = faceaccrepo.getFacebookAccountDetailsById(profileid, user.Id);
                    FacebookFeedRepository facefeedrepo = new FacebookFeedRepository();
                    List<FacebookFeed> lstfbfeed = facefeedrepo.getAllFacebookUserFeeds(profileid);
                    UrlExtractor urlext = new UrlExtractor();
                    foreach (FacebookFeed item in lstfbfeed)
                    {
                        message += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"https://www.facebook.com/" + item.ProfileId + "/picture?type=small\" onclick=\"getFacebookProfiles('" + item.FromId + "');\">" +
                                                                 "</div><div class=\"pull-left feedcontent\">" +
                                                                    "<a href=\"#\" class=\"feednm\" onclick=\"getFacebookProfiles('" + item.FromId + "');\">" + faceaac.FbUserName + "</a> <span>" + item.FeedDate +
                                                                        " </span>" +
                                                                     "<p>";

                        string[] str = urlext.splitUrlFromString(item.FeedDescription);

                        foreach (string substritem in str)
                        {
                            if (!string.IsNullOrEmpty(substritem))
                            {
                                if (substritem.Contains("http"))
                                {
                                    message += "<a target=\"_blank\" href=\"" + substritem + "\">" + substritem + "</a>";
                                }
                                else
                                {
                                    message += substritem;
                                }
                            }
                        }


                        message += "</p>" +
                                     "<a class=\"retweets\" href=\"#\">" +
                                      "</a><span></span>" +
                                 "</div>" +
                             "</li>";
                    }
                    Response.Write(message);
                }
                else if (Request.QueryString["op"] == "twitterfeeds")
                {
                    string message = string.Empty;
                    string profileid = Request.QueryString["profileid"];
                    TwitterMessageRepository twtmsgreop = new TwitterMessageRepository();
                    List<TwitterMessage> lstmsg = twtmsgreop.getAllTwitterMessagesOfProfile(profileid);
                    //TwitterFeedRepository twtmsgrepo = new TwitterFeedRepository();
                    //List<TwitterFeed>  lstfeed =  twtmsgrepo.getTwitterFeedOfProfile(profileid);
                    UrlExtractor urlext = new UrlExtractor();
                    foreach (TwitterMessage item in lstmsg)
                    {
                        try
                        {
                            message += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"" + item.FromProfileUrl + "\" onclick=\"detailsprofile('" + item.FromId + "');\">" +
                                                         "</div><div class=\"pull-left feedcontent\">" +
                                                            "<a href=\"#\" class=\"feednm\" onclick=\"detailsprofile('" + item.FromId + "');\">" + item.FromName + "</a> <span>" + item.MessageDate +
                                                                " </span>" +
                                                             "<p>";

                            string[] str = urlext.splitUrlFromString(item.TwitterMsg);

                            foreach (string substritem in str)
                            {
                                if (!string.IsNullOrEmpty(substritem))
                                {
                                    if (substritem.Contains("http"))
                                    {
                                        message += "<a target=\"_blank\" href=\"" + substritem + "\">" + substritem + "</a>";
                                    }
                                    else
                                    {
                                        message += substritem;
                                    }
                                }
                            }
                            message += "</p>" +
                                                              "<a class=\"retweets\" href=\"#\">" +
                                /*"<img alt=\"\" src=\"../Contents/img/admin/arrow.png\">*/"</a><span></span>" +
                                                          "</div>" +
                                                      "</li>";
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            Console.WriteLine(ex.Message);
                        }
                    }
                    Response.Write(message);
                }
                else if (Request.QueryString["op"] == "linkedinwallposts")
                {
                    string message = string.Empty;
                    string profileid = Request.QueryString["profileid"];

                    LinkedInFeedRepository linkedinfeedrepo = new LinkedInFeedRepository();
                    List<LinkedInFeed> lstfeed = linkedinfeedrepo.getAllLinkedInFeedsOfProfile(profileid);


                    if (lstfeed != null)
                    {
                        if (lstfeed.Count != 0)
                        {
                            foreach (LinkedInFeed item in lstfeed)
                            {
                                message += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"" + item.FromPicUrl + "\">" +
                                                               "</div><div class=\"pull-left feedcontent\">" +
                                                                  "<a href=\"#\" class=\"feednm\">" + item.FromName + "</a> <span>" + item.FeedsDate +
                                                                      " </span>" +
                                                                   "<p>" + item.Feeds + "</p>" +
                                                                   "<a class=\"retweets\" href=\"#\">" +
                                                                   "</a><span></span>" +
                                                               "</div>" +
                                                           "</li>";
                            }
                        }
                        else
                        {
                            message = "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"../Contents/img/blank_img.png\">" +
                                                                   "</div><div class=\"pull-left feedcontent\">" +
                                                                      "<a href=\"#\" class=\"feednm\"></a> <span>" +
                                                                          " </span>" +
                                                                       "<p>No Messages Found</p>" +
                                                                       "<a class=\"retweets\" href=\"#\">" +
                                                                        "</a><span></span>" +
                                                                   "</div>" +
                                                               "</li>";
                        }
                    }
                    Response.Write(message);

                }
                else if (Request.QueryString["op"] == "linkedinfeeds")
                {
                    string profileid = Request.QueryString["profileid"];
                    LinkedInAccountRepository linkedinAccRepo = new LinkedInAccountRepository();
                    LinkedInAccount linkacc = linkedinAccRepo.getUserInformation(user.Id, profileid);
                    oAuthLinkedIn oauthlin = new oAuthLinkedIn();
                    oauthlin.ConsumerKey = ConfigurationManager.AppSettings["LiApiKey"];
                    oauthlin.ConsumerSecret = ConfigurationManager.AppSettings["LiSecretKey"];
                    oauthlin.FirstName = linkacc.LinkedinUserName;
                    oauthlin.Id = linkacc.LinkedinUserId;
                    oauthlin.Token = linkacc.OAuthToken;
                    oauthlin.TokenSecret = linkacc.OAuthSecret;
                    oauthlin.Verifier = linkacc.OAuthVerifier;


                    LinkedInUser l = new LinkedInUser();
                    List<LinkedInUser.User_Updates> lst = l.GetUserUpdates(oauthlin, linkacc.LinkedinUserId, 10);
                    string message = string.Empty;
                    if (lst.Count != 0)
                    {
                        foreach (LinkedInUser.User_Updates item in lst)
                        {
                            try
                            {
                                string picurl = string.Empty;
                                if (string.IsNullOrEmpty(item.PictureUrl))
                                {
                                    picurl = "../Contents/img/blank_img.png";
                                }
                                else
                                {
                                    picurl = item.PictureUrl;
                                }
                                message += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"" + picurl + "\">" +
                                                                       "</div><div class=\"pull-left feedcontent\">" +
                                                                          "<a href=\"#\" class=\"feednm\">" + item.PersonFirstName + " " + item.PersonLastName + "</a> <span>" + item.DateTime +
                                                                              " </span>" +
                                                                           "<p>" + item.Message + "</p>" +
                                                                           "<a class=\"retweets\" href=\"#\">" +
                                                                           "</a><span></span>" +
                                                                       "</div>" +
                                                                   "</li>";
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                        }
                    }
                    else
                    {
                        message += "<li><div class=\"feedim pull-left\"><img alt=\"\" width=\"31\" height=\"31\" src=\"../Contents/img/blank_img.png\">" +
                                                                                             "</div><div class=\"pull-left feedcontent\">" +
                                                                                                "<a href=\"#\" class=\"feednm\"></a> <span>" +
                                                                                                    " </span>" +
                                                                                                 "<p>No Messages Found</p>" +
                                                                                                 "<a class=\"retweets\" href=\"#\">" +
                                                                                                  "</a><span></span>" +
                                                                                             "</div>" +
                                                                                         "</li>";
                    }
                    Response.Write(message);
                }
                else if (Request.QueryString["op"] == "facebookapi")
                {
                    try
                    {
                        string profileid = Request.QueryString["profileid"];
                        FacebookAccountRepository facerepo = new FacebookAccountRepository();
                        FacebookAccount faceaccount = facerepo.getFacebookAccountDetailsById(profileid, user.Id);
                        FacebookHelper fbhelper = new FacebookHelper();
                        FacebookClient fbclient = new FacebookClient(faceaccount.AccessToken);
                        dynamic profile = fbclient.Get("me");
                        var feeds = fbclient.Get("/me/feed");
                        var home = fbclient.Get("me/home");
                        fbhelper.getFacebookUserFeeds(feeds, profile);
                        fbhelper.getFacebookUserHome(home, profile);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (Request.QueryString["op"] == "twitterapi")
                {
                    string profileid = Request.QueryString["profileid"];
                    TwitterAccountRepository twtAccountRepo = new TwitterAccountRepository();
                    TwitterAccount twtAccount = twtAccountRepo.getUserInformation(user.Id, profileid);
                    oAuthTwitter oAuth = new oAuthTwitter();
                    TwitterHelper twthelper = new TwitterHelper();
                    oAuth.AccessToken = twtAccount.OAuthToken;
                    oAuth.AccessTokenSecret = twtAccount.OAuthSecret;
                    twthelper.SetCofigDetailsForTwitter(oAuth);
                    oAuth.TwitterScreenName = twtAccount.TwitterScreenName;
                    oAuth.TwitterUserId = twtAccount.TwitterUserId;
                    twthelper.getUserTweets(oAuth, twtAccount, user.Id);
                    twthelper.getUserFeed(oAuth, twtAccount, user.Id);
                    twthelper.getSentDirectMessages(oAuth, twtAccount, user.Id);
                    twthelper.getReTweetsOfUser(oAuth, twtAccount, user.Id);

                }
                else if (Request.QueryString["op"] == "instagramlike")
                {
                    string mediaid = Request.QueryString["mediaid"];
                    bool b = this.likefunction(mediaid, Request.QueryString["userid"], Request.QueryString["access"]);
                }
                else if (Request.QueryString["op"] == "instagramunlike")
                {
                    string mediaid = Request.QueryString["mediaid"];
                    bool b = this.unlikefunction(mediaid, Request.QueryString["userid"], Request.QueryString["access"]);

                }
                else if (Request.QueryString["op"] == "instagramimages")
                {
                    if (Request.QueryString["loadtime"] != "first")
                    {
                        instagramcount = instagramcount + 10;
                    }
                    else
                    {
                        instagramcount = 0;
                    }



                    InstagramAccountRepository objInsAccRepo = new InstagramAccountRepository();
                    InstagramFeedRepository objInsFeedRepo = new InstagramFeedRepository();
                    InstagramFeed objInsFeed = new InstagramFeed();
                    InstagramCommentRepository objInsCmtRepo = new InstagramCommentRepository();
                    List<SocioBoard.Domain.InstagramComment> lstInsCmt = new List<SocioBoard.Domain.InstagramComment>();

                    string strInsImage = string.Empty;

                    try
                    {
                        string profileid = Request.QueryString["profileid"];
                        InstagramAccount insaccount = objInsAccRepo.getInstagramAccountDetailsById(profileid, user.Id);
                        List<InstagramFeed> lstInsFeed = objInsFeedRepo.getAllInstagramFeedsOfUser(user.Id, profileid, instagramcount);
                        if (lstInsFeed.Count != 0)
                        {
                            strInsImage += "<div class=\"feedcontainer\">";
                            foreach (InstagramFeed feed in lstInsFeed)
                            {

                                try
                                {
                                    lstInsCmt = objInsCmtRepo.getAllInstagramCommentsOfUser(user.Id, profileid, feed.FeedId);
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex.Message);
                                    Console.WriteLine(ex.Message);
                                }
                                try
                                {
                                    strInsImage += "<div class=\"span3\" class=\"row-fluid\"><div class=\"span12 box whitebg feedwrap\"><div class=\"topicon\"><div class=\"pull-left\">" +

                                                                    "</div><div class=\"pull-right\" id=\"like\"><a title=\"\" href=\"#\" onClick=\"insUser('" + feed.FeedId + "','" + insaccount.AccessToken + "')\" ><img id=\"heartEmpty_" + feed.FeedId + "\" width=\"14\" alt=\"\" src=\"../Contents/img/admin/heart-empty.png\"  style=\"margin-top: 9px;\"></a><a title=\"\" href=\"#\"><img width=\"14\" alt=\"\" src=\"../Contents/img/admin/speech-bubble-left.png\"  style=\"margin-top: 9px;\"></a>" +
                                                                    "</div></div><div class=\"pic\"><img alt=\"\" src=\"" + feed.FeedImageUrl + "\"></div><div class=\"desc\"><p></p><span class=\"pull-left span3\">" +
                                                                    "<img width=\"12\" alt=\"\" src=\"../Contents/img/admin/heart-empty.png\"> " + feed.LikeCount + "</span><span class=\"pull-left span3\"><img width=\"12\" alt=\"\" src=\"../Contents/img/admin/speech-bubble-left.png\"> "+ lstInsCmt.Count +"</span><div class=\"clearfix\"></div>";

                                    foreach (InstagramComment insCmt in lstInsCmt)
                                    {
                                        try
                                        {
                                            strInsImage += "<div class=\"userprof\"><div class=\"pull-left\"><a href=\"#\">" +
                                              "<img width=\"36\" alt=\"\" src=\"" + insCmt.FromProfilePic + "\"></a></div><div class=\"pull-left descr\"><p>" + insCmt.Comment + "</p>" +
                                               "<span class=\"usert\">" + DateExtension.ToDateTime(DateTime.Now, (long)Convert.ToDouble(insCmt.CommentDate)) + "</span></div></div>";

                                        }
                                        catch (Exception ex)
                                        {
                                            logger.Error(ex.Message);
                                            Console.WriteLine(ex.Message);
                                        }

                                    }

                                    strInsImage += "</div></div></div>";
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex.Message);
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            strInsImage += "</div>";
                        }
                        else
                        {
                            if (instagramcount == 0)
                            {
                                strInsImage = "<div class=\"grid\"><div class=\"box whitebg feedwrap\">" +
                                             "<div class=\"topicon\"><div class=\"pull-left\"></div><div class=\"pull-right\">" +
                                 "<a href=\"#\" title=\"\"></a><a href=\"#\" title=\"\"></a></div></div><div class=\"pic\">" +
                                 "<img src=\"../Contents/img/no_image_found.png\" alt=\"\"></div><div class=\"desc\"><p></p></div></div></div>";
                            }
                        }


                        Response.Write(strInsImage);


                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }

                }
                else if (Request.QueryString["op"] == "instagramApi")
                {
                    try
                    {
                        InstagramManager insManager = new InstagramManager();
                        string profileid = Request.QueryString["profileid"];
                        InstagramAccountRepository insAccRepo = new InstagramAccountRepository();
                        InstagramAccount instagramAccount = insAccRepo.getInstagramAccountDetailsById(profileid, user.Id);
                        insManager.getIntagramImages(instagramAccount);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        Console.WriteLine(ex.Message);
                    }
                }

            }
        }

        public bool likefunction(string id, string userid, string accesstoken)
        {
            oAuthInstagram _api;
            GlobusInstagramLib.Authentication.ConfigurationIns configi = new GlobusInstagramLib.Authentication.ConfigurationIns("https://api.instagram.com/oauth/authorize/", ConfigurationManager.AppSettings["InstagramClientKey"].ToString(), ConfigurationManager.AppSettings["InstagramClientSec"].ToString(), ConfigurationManager.AppSettings["InstagramCallBackURL"].ToString(), "http://api.instagram.com/oauth/access_token", "https://api.instagram.com/v1/", "");
            _api = oAuthInstagram.GetInstance(configi);
            LikesController objlikes = new LikesController();
            bool dd = objlikes.PostUserLike(id, userid, accesstoken);
            return dd;
        }
        public bool unlikefunction(string id, string userid, string accesstoken)
        {
            LikesController objlikes = new LikesController();
            bool dd = objlikes.DeleteLike(id, userid, accesstoken);
            return dd;

        }
    }
}