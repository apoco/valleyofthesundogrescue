using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VOTSDR.Web.Models;
using VOTSDR.Data;
using VOTSDR.Utils;
using System.Net.Mail;
using System.Configuration;

namespace VOTSDR.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var entities = new DataEntities();

            var news =
                from article in entities
                    .NewsStories
                    .OrderByDescending(s => s.DateCreated)
                    .Take(10)
                    .ToList()
                select new NewsOrEventSummaryViewModel
                {
                    Id = article.NewsStoryId,
                    Date = article.Date,
                    Title = article.Title,
                    Summary = article.Text.Summarize(200),
                };

            var events =
                from evt in entities
                    .Events
                    .OrderByDescending(e => e.DateCreated)
                    .Take(10)
                    .ToList()
                select new NewsOrEventSummaryViewModel
                {
                    Id = evt.EventId,
                    Date = evt.Date,
                    Title = evt.Name,
                    Summary = evt.Description.Summarize(200),
                };

            var viewData = new HomeIndexViewModel
            {
                NewsAndEvents = news
                    .Concat(events)
                    .OrderByDescending(i => i.Date),
            };

            // Load the latest featured dog
            var featuredDog = entities
                .Dogs
                .OrderByDescending(d => d.DateFeatured)
                .Where(d => d.DateFeatured.HasValue)
                .FirstOrDefault();
            if (featuredDog != null)
            {
                viewData.FeaturedDogId = featuredDog.DogId;
                viewData.FeaturedDogThumbnailUrl = Url.Action(
                    "Thumbnail", "Dog",
                    new { id = featuredDog.DogId });
                viewData.FeaturedDogName = featuredDog.Name;
                viewData.FeaturedDogProfile = featuredDog.Profile;
            }

            var featuredNeed = entities
                .SpecialNeedsStories
                .Where(s => s.IsFeatured)
                .OrderByDescending(s => s.DateCreated)
                .FirstOrDefault();
            if (featuredNeed != null)
            {
                viewData.SpecialNeedThumbnailUrl = Url.Action(
                    "SpecialNeedsImage", "News",
                    new { id = featuredNeed.SpecialNeedsStoryId });
                viewData.SpecialNeedDescription = featuredNeed.Text;
            }
            else
            {
                viewData.SpecialNeedDescription = string.Empty;
            }

            return View(viewData);
        }

        public ActionResult HowToHelp()
        {
            return View();
        }

        public ActionResult Education()
        {
            var content = GetCurrentContent("education") ;
            var model = new EducationModel { Markup = content.Markup };
            return View(model);
        }

        public ActionResult SponsorsAndLinks()
        {
            var sponsorContent = GetCurrentContent("sponsors");
            var linksContent = GetCurrentContent("links");
            var model = new SponsorsAndLinksModel { SponsorMarkup = sponsorContent.Markup, LinkMarkup = linksContent.Markup };
            
            return View(model);
        }
        private static Content GetCurrentContent(string area)
        {
            var allSponsorContent = new DataEntities().Contents.Where(c => c.Area.ToLower() == area.ToLower()).OrderByDescending(c => c.PublishOn);
            var defaultSponsor = allSponsorContent.FirstOrDefault<Content>(c => c.IsDefault == true);
            Content content = defaultSponsor;
            foreach (var contentItem in allSponsorContent)
            {
                if (contentItem.PublishOn <= DateTime.Now)
                {
                    if (contentItem.PublishUntil.HasValue)
                    {
                        if (contentItem.PublishUntil.Value >= DateTime.Now)
                        {
                            // we are in the sweetspot
                            content = contentItem;
                            break;
                        }
                        else
                        {
                            // we are past the publish until date, keep looking
                        }
                    }
                    else
                    {
                        // there's a publish on without an end date, so use it
                        content = contentItem;
                        break; // take the first one
                    }
                }
            }
            return content ?? new Content { Markup = "****Coming Soon" };
        }
        public ActionResult Shop()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View(new ContactUsViewModel());
        }

        [HttpPost]
        public ActionResult ContactUs(ContactUsViewModel contactForm)
        {
            if (ModelState.IsValid)
            {
                using (var smtpClient = new SmtpClient())
                {
                    var mailMsg = new MailMessage {
                        Body = string.Format(@"
                            <html>
                              <head></head>
                              <body>
                                <p>
                                    Received the following from a visitor to 
                                    http://valleyofthesundogrescue.com/
                                </p>
                                <table>
                                  <tbody>
                                    <tr>
                                      <th>Name:</th>
                                      <td>{0}</td>
                                    </tr>
                                    <tr>
                                      <th>Phone Number:</th>
                                      <td>{1}</td>
                                    </tr>
                                    <tr>
                                      <th>Email Address:</th>
                                      <td>{2}</td>
                                    </tr>
                                    <tr>
                                      <th>Street Address:</th>
                                      <td>{3}</td>
                                    </tr>
                                    <tr>
                                      <th>Comments:</th>
                                      <td>{4}</td>
                                    </tr>
                                  </tbody>
                                </table>
                              </body>
                            </html>", 
                            HttpUtility.HtmlEncode(contactForm.Name), 
                            HttpUtility.HtmlEncode(contactForm.PhoneNumber),
                            HttpUtility.HtmlEncode(contactForm.EmailAddress), 
                            HttpUtility.HtmlEncode(contactForm.StreetAddress),
                            HttpUtility.HtmlEncode(contactForm.Comments)
                                .Replace("\r\n", "<br/>")
                                .Replace("\n", "<br/>")),
                        From = new MailAddress("noreply@valleyofthesundogrescue.com"),
                        IsBodyHtml = true,
                        Subject = "Contact message from " + contactForm.Name };
                    mailMsg.To.Add(
                        ConfigurationManager.AppSettings["ContactUsEmailAddress"]);
                    mailMsg.ReplyToList.Add(
                        contactForm.EmailAddress
                        ?? "noreply@valleyofthesundogrescue.com");
                    smtpClient.Send(mailMsg);
                    return Redirect("ContactUsSuccess");
                }
            }
            else
            {
                return View(contactForm);
            }
        }

        public ActionResult ContactUsSuccess()
        {
            return View();
        }
    }
}
