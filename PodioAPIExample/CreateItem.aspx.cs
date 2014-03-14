using PodioAPI;
using PodioAPI.Exceptions;
using PodioAPI.Models;
using PodioAPI.Utils.ItemFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PodioAPIExample
{
    public partial class CreateItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var auth = podio.AuthenicateWithApp(7292649, "121f0b6e396c46b397363e7047010b6f");
                var items = podio.ItemService.FilterItems(7292649);
                if (items.Total > 0)
                {
                    foreach (var item in items.Items)
                    {
                        ddlReference.Items.Add(new ListItem { Text = item.Title, Value = item.ItemId.ToString() });
                    }
                }
            }
        }
        Podio _podio = null;
        public Podio podio
        {
            get
            {
                if (_podio == null)
                    _podio = PodioConnection.GetCilent();

                return _podio;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
            try
            {
                var auth = podio.AuthenicateWithApp(7292649, "121f0b6e396c46b397363e7047010b6f");
                string savedFilename = "";
                Item item = new Item();
                var textfield = item.Field<TextItemField>("title");
                textfield.Value = txtTitle.Text;

                if (!string.IsNullOrEmpty(txtDate.Text))
                {
                    var dateField = item.Field<DateItemField>("date");
                    dateField.Start = DateTime.Parse(txtDate.Text);
                }
                
                if (!string.IsNullOrEmpty(txtLocation.Text))
                {
                    var locationField = item.Field<LocationItemField>("location");
                    List<string> location = new List<string>() { 
                    txtLocation.Text
                    };
                    locationField.Locations = location;
                }

                if(!string.IsNullOrEmpty(txtNumber.Text))
                {
                    var numericField = item.Field<NumericField>("number");
                    numericField.Value = double.Parse(txtNumber.Text);
                }
                
                var embedUrl = txtLink.Text;
                if(!string.IsNullOrEmpty(embedUrl))
                {
                    var embedField = item.Field<EmbedItemField>("link");
                    var embed = podio.EmbedService.AddAnEmbed(embedUrl);
                    if (embed.Files.Count > 0)
                    {
                        embedField.AddEmbed((int)embed.EmbedId, embed.Files.First().FileId);
                    }
                    else
                    {
                        embedField.AddEmbed((int)embed.EmbedId);
                    }
                }
                
                if (!string.IsNullOrEmpty(txtMoney.Text))
                {
                    var moneyField = item.Field<MoneyItemField>("money");
                    moneyField.Currency = "EUR";
                    moneyField.Value = decimal.Parse(txtMoney.Text);
                }
                
                if(ddlReference.SelectedValue != "-1")
                {
                    var appField = item.Field<AppItemField>("app-reference");
                    appField.ItemIds = new List<int> { int.Parse(ddlReference.SelectedValue) };
                }
                
                if (pnlImage.HasFile)
                {
                    savedFilename = SaveFile(pnlImage.PostedFile);
                    var imageField = item.Field<ImageItemField>("image");
                    var fullPath = Server.MapPath("\\images\\UploadedFiles\\" + savedFilename);
                    var file = podio.FileService.UploadFile(fullPath, savedFilename);
                    imageField.FileIds = new List<int> { file.FileId };
                }
                
                int createdAppId = podio.ItemService.AddNewItem(7292649, item);
                if(createdAppId > 0)
                {
                    lblSuccess.Text = "The item with id " + createdAppId.ToString() + " created successfully.";
                    ClearFields();
                }
                
            }
            catch (PodioException podioException)
            {
                var error = podioException.Error;
                string errorMessage = @"HTTP Status Code: {0} <br/> Error Description: {1} <br/> Error Detail: {2}";
                HttpContext.Current.Response.Write(string.Format(errorMessage, podioException.Status, error.ErrorDescription, error.ErrorDetail));
            }
        }

        string SaveFile(HttpPostedFile file)
        {
            // Specify the path to save the uploaded file to.
            string savePath = Server.MapPath("\\images\\UploadedFiles\\");

            // Get the name of the file to upload.
            string fileName = pnlImage.FileName;

            // Create the path and file name to check for duplicates.
            string pathToCheck = savePath + fileName;

            // Create a temporary file name to use for checking duplicates.
            string tempfileName = "";

            // Check to see if a file already exists with the
            // same name as the file to upload.        
            if (System.IO.File.Exists(pathToCheck))
            {
                int counter = 2;
                while (System.IO.File.Exists(pathToCheck))
                {
                    // if a file with this name already exists,
                    // prefix the filename with a number.
                    tempfileName = counter.ToString() + fileName;
                    pathToCheck = savePath + tempfileName;
                    counter++;
                }

                fileName = tempfileName;

            }
            // Append the name of the file to upload to the path.
            savePath += fileName;

            // Call the SaveAs method to save the uploaded
            // file to the specified directory.
            pnlImage.SaveAs(savePath);
            return fileName;
        }

        void ClearFields()
        {
            txtDate.Text = "";
            txtLink.Text = "";
            txtMoney.Text = "";
            txtNumber.Text = "";
            txtTitle.Text = "";
        }
    }
}