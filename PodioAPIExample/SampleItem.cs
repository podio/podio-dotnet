using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PodioAPI;
using PodioAPI.Exceptions;
using PodioAPI.Models.Response;
using PodioAPI.Utils.ItemFields;


namespace PodioAPIExample
{
    public class SampleItem
    {
        public int? AddNewitem()
        {
            var podio = new Podio("ClientId", "ClientSecret");
            try
            {
                
                var auth = podio.AuthenicateWithPassword("user@example.com", "xxxxxx");
                Item item = new Item();

                var textfield = item.Field<TextItemField>("title");
                textfield.Value = "My newest value latest !!!";

                var dateField = item.Field<DateItemField>("date");
                dateField.Start = DateTime.Now;
                dateField.End = DateTime.Now.AddMonths(2);

                var categoryField = item.Field<CategoryItemField>("category-multiple");
                categoryField.OptionId = 1;

                var locationField = item.Field<LocationItemField>("google-maps");
                List<string> location = new List<string>() { 
                    "Copenhagen, denmark"
                };
                locationField.Locations = location;

                var questionField = item.Field<QuestionItemField>("question");
                questionField.OptionId = 1;

                var moneyField = item.Field<MoneyItemField>("money");
                moneyField.Currency = "EUR";
                moneyField.Value = 223;

                var durationField = item.Field<DurationItemField>("duration");
                durationField.Value = TimeSpan.Parse("1.00:00:00");

                var progressField = item.Field<ProgressItemField>("progress-slider");
                progressField.Value = 51;

                var contactField = item.Field<ContactItemField>("contact");
                contactField.ContactIds = new List<int> { 106996753 };

                var appField = item.Field<AppItemField>("app-reference-o");
                appField.ItemIds = new List<int> { 119925300 };

                var numericField = item.Field<NumericField>("number");
                numericField.Value = 45;

                var imageField = item.Field<ImageItemField>("image");
                var file = podio.FileService.UploadFile("E:\\Projects\\Podio API working\\Source\\Podio .NET Client\\PlayGround\\Images\\44.jpg", "44.jpg");
                imageField.FileIds = new List<int> {file.FileId};

                var embedField = item.Field<EmbedItemField>("link");
                var embedUrl = "https://podio.com";
                var embed = podio.EmbedService.AddAnEmbed(embedUrl);
                if(embed.Files.Count > 0)
                {
                    embedField.AddEmbed((int)embed.EmbedId, embed.Files.First().FileId);
                }
                else
                {
                    embedField.AddEmbed((int)embed.EmbedId);
                }

                return podio.ItemService.AddNewItem(6681120, item);
            }
            catch(PodioException podioException)
            {
                var error = podioException.Error;
                string errorMessage = @"HTTP Status Code: {0} <br/> Error Description: {1} <br/> Error Detail: {2}";
                HttpContext.Current.Response.Write(string.Format(errorMessage, podioException.Status, error.ErrorDescription, error.ErrorDetail));
                return null;
            }
        }
    }
}