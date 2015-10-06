using System;
using System.Linq;
using PodioAPI.Models;

namespace PodioAPI.Utils.ItemFields
{
    public class DateItemField : ItemField
    {
        public DateTime? Start
        {
            get
            {
                if (this.HasValue("start"))
                {
                    return DateTime.Parse((string) this.Values.First()["start"]);
                }
                else
                {
                    return null;
                }
            }
            set { setDateTime(value, "start"); }
        }

        public DateTime? End
        {
            get
            {
                if (this.HasValue("end"))
                {
                    return DateTime.Parse((string) this.Values.First()["end"]);
                }
                else
                {
                    return null;
                }
            }
            set { setDateTime(value, "end"); }
        }

        public DateTime? StartDate
        {
            get
            {
                if (this.HasValue("start_date"))
                {
                    return DateTime.Parse((string) this.Values.First()["start_date"]);
                }
                else
                {
                    return null;
                }
            }
        }

        public DateTime? EndDate
        {
            get
            {
                if (this.HasValue("end_date"))
                {
                    return DateTime.Parse((string) this.Values.First()["end_date"]);
                }
                else
                {
                    return null;
                }
            }
        }

        public string StartTime
        {
            get
            {
                if (this.HasValue("start_time"))
                {
                    return (string) this.Values.First()["start_time"];
                }
                else
                {
                    return null;
                }
            }
        }

        public string EndTime
        {
            get
            {
                if (this.HasValue("end_time"))
                {
                    return (string) this.Values.First()["end_time"];
                }
                else
                {
                    return null;
                }
            }
        }

        protected void setDateTime(DateTime? value, string attribute)
        {
            EnsureValuesInitialized(true);
            string stringValue = null;
            if (value != null)
            {
                stringValue = String.Format("{0:yyyy-MM-dd HH:mm:ss}", value);
            }
            this.Values.First()[attribute] = stringValue;
        }
    }
}