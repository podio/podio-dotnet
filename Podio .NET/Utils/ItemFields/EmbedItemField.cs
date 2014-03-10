using PodioAPI.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodioAPI.Utils.ItemFields
{
    public class EmbedItemField : ItemField
    {
        private List<Embed> _embeds;

        public IEnumerable<Embed> Embeds
        {
            get
            {
                if (_embeds == null)
                {
                    _embeds = new List<Embed>();
                    if(this.Values != null)
                    {
                        foreach (var embedFilePair in this.Values)
                        {
                            var embed = this.valueAs<Embed>(embedFilePair, "embed");
                            if (embedFilePair.ContainsKey("file"))
                            {
                                var file = this.valueAs<FileAttachment>(embedFilePair, "file");
                                if (embed.Files == null)
                                {
                                    embed.Files = new List<FileAttachment>();
                                }
                                embed.Files.Add(file);
                            }
                            _embeds.Add(embed);
                        }
                    }      
                }
                return _embeds;
            }
        }

        public void AddEmbed(int embedId, int? fileId = null)
        {
            ensureValuesInitialized();
            var dict = new Dictionary<string, object>();
            dict["embed"] = embedId;
            if (fileId != null)
            {
                dict["file"] = fileId;
            }
            this.Values.Add(dict);
        }
    }
}
