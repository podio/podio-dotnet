using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PodioAPI;
using PodioAPI.Models.Request;
using PodioAPI.Exceptions;
using PodioAPIExample.ViewModel;
using PodioAPI.Utils.ItemFields;


namespace PodioAPIExample
{
    public partial class Sample : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    PopulateItems(null);
                }
                catch (PodioException podioException)
                {
                    lblError.Text = "Error: " + podioException.Error.ErrorDescription;
                } 
            }
        }

        Podio _podio = null;
        public Podio podio 
        {
            get
            {
                if( _podio == null)
                    _podio = PodioConnection.GetCilent();

                return _podio;
            }
        }


        public void PopulateItems(object filter)
        {
            if (podio.IsAuthenticated())
            {
                var items = podio.ItemService.FilterItems(7292649, 30, 0, filter);
                List<ItemViewModel> itemList = new List<ItemViewModel>();
                if(items.Items.Count()> 0)
                {
                    foreach(var item in items.Items)
                    {
                        var titleField = item.Field<TextItemField>("title");
                        var locationField = item.Field<LocationItemField>("location");
                        var moneyField = item.Field<MoneyItemField>("money");
                        var linkField = item.Field<EmbedItemField>("link");
                        var embeds = linkField.Embeds;
                        var allLinks = "";
                        var allLocations = "";
                        var locations = locationField.Locations;
                        if (embeds.Any())
                        {
                            allLinks = string.Join(",", embeds.Select(x => x.OriginalUrl).ToArray()).TrimEnd(',');                           
                        }
                        if(locations.Any())
                        {
                            var temp = "";
                            foreach(var location in locations)
                            {
                                allLocations = location + temp;
                                temp = "," + allLocations;
                            }
                        }
                        var itemviewModel = new ItemViewModel
                        {
                            Title = titleField.Value,
                            Link = allLinks,
                            CreatedOn = item.CreatedOn,
                            Money = moneyField.Currency +" "+ moneyField.Value,
                            Location = allLocations
                        };
                        itemList.Add(itemviewModel);
                    }
                }
                rpItems.DataSource = itemList;
                rpItems.DataBind();
            }
        }

        protected void btnButtom_Click(object sender, EventArgs e)
        {
            DateTime FromDate = DateTime.Parse(txtDateFrom.Text);
            DateTime ToDate = DateTime.Parse(txtDateTo.Text);
            var filters = new
            {
                created_on = new
                {
                    from = FromDate,
                    to = ToDate
                }
            };
            PopulateItems(filters);
            //SampleItem test = new SampleItem();
            //test.AddNewitem();
        }
    }
}